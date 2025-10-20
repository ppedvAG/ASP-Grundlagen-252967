namespace M002_Einfuehrung;

public class Startup
{
	public static WebApplication ConfigureServices(WebApplicationBuilder builder)
	{
		builder.Services.AddControllersWithViews();

		//Hier können neue Services registriert werden
		builder.Services.AddSingleton<DITest>();
		//Im Konstruktor vom Controller kann dieses Objekt nun empfangen werden

		return builder.Build();
	}

	public static void ConfigureMiddleware(WebApplication app)
	{
		//Middleware
		//Konfiguriert HTTP-Requests/Responses
		//WICHTIG: Reihenfolge beachten

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapStaticAssets();
		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}")
			.WithStaticAssets();

		app.Run();
	}
}
