using M006_EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M006_EntityFramework.Controllers;

public class HomeController(ILogger<HomeController> logger, KursDBContext db) : Controller
{
	public IActionResult Index()
	{
		//IQueryable
		//Nur eine Anleitung zum Laden der Daten
		IQueryable<Kurse> k = db.Kurse.Where(e => e.Kursname == null); //Bereitet das SQL-Statement vor: SELECT * FROM Kurse WHERE Kursname IS NULL

		//Mit ToList() werden die Daten von der Datenbank heruntergezogen
		List<Kurse> kurse = db.Kurse.OrderBy(e => e.Kursname).ToList(); //Immer gut nachdenken, wo ToList() verwendet werden soll

		return View(kurse);
	}

	public IActionResult Privacy()
	{
		//db.Database.ExecuteSqlRaw("DELETE FROM Kurse WHERE Aktiv IS NULL");
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
