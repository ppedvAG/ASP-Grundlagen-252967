//////////////////////////////////////////////////////////////////////////
//EntityFramework
//Object Relational Mapper
//Datenbank mit Anwendung verbinden
//Daten aus der DB laden, und in der Anwendung als C#-Objekte darstellen
//////////////////////////////////////////////////////////////////////////
//EF Core Power Tools
//GUI für Konsolenbefehle vom Entity Framework
//Rechtsklick auf Projekt -> EF Core Power Tools -> Reverse Engineer
//Connection hinzufügen/auswählen
//Tabellen auswählen
//Use DataAnnotations, Include Connection String, Install EF Core Package
//////////////////////////////////////////////////////////////////////////
//NuGet-Pakete
//- Microsoft.EntityFrameworkCore
//- Microsoft.EntityFrameworkCore.SqlServer (für jedes beliebige Datenbanksystem gibt es ein Paket)
//- Microsoft.EntityFrameworkCore.Tools
//- Microsoft.EntityFrameworkCore.Design


using M006_EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Hinzufügen vom DBContext per Dependency Injection + appsettings.json
string? conn = builder.Configuration.GetConnectionString("KursDB");
if (conn != null)
	builder.Services.AddDbContext<KursDBContext>(o => o.UseSqlServer(conn));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
