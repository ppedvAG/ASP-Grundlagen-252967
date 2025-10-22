using M008_Lokalisierung.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

////////////////////////////////////////////////////////////////////

builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(o =>
{
	string[] cultures = ["de", "en"]; //Cultures können auch aus einer Config-Datei geladen werden
	o.SetDefaultCulture(cultures[0]);
	o.AddSupportedCultures(cultures);
	o.AddSupportedUICultures(cultures);
});

////////////////////////////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

//Middleware
//Pipeline, durch den sich Requests von Clients sequentiell bewegen
//Es kann auch eigene Middleware definiert werden
app.UseRequestLocalization();
app.UseMiddleware<LocalizationMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
