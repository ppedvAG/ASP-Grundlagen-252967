namespace M002_Einfuehrung;

public class Program
{
	public static void Main(string[] args)
	{
		//Dependency Injection
		//Bereitstellung von beliebigen C#-Objekten an die Controller/Pages
		//Werden hier registriert, und über den Konstruktor (Controller) oder über @inject empfangen
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		//Hier können neue Services registriert werden
		builder.Services.AddSingleton<DITest>();
		//Im Konstruktor vom Controller kann dieses Objekt nun empfangen werden

		var app = builder.Build();

		/////////////////////////////////////////////////////////////////////////////

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

	//public static void Main(string[] args)
	//{
	//	WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

	//	WebApplication app = Startup.ConfigureServices(builder);

	//	Startup.ConfigureMiddleware(app);
	//}
}