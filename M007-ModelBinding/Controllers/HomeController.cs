using M007_ModelBinding.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M007_ModelBinding.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
	[FromQuery]
	public string LanguageCode { get; set; } //Allgemeiner Parameter, welcher bei jeder Controller Methode gefüllt werden kann

	/// <summary>
	/// [FromQuery]: ?test=123
	/// [FromRoute]: /Home/Index/123
	/// [FromBody]: Im HttpBody
	/// </summary>
	public IActionResult Index(string test)
	{
		return View("Index", test);
	}

	public IActionResult Privacy([Bind] [FromQuery] User u) //Wenn in der Adressleiste zwei Parameter namens Username und Password gegeben sind, wird hier ein Objekt erzeugt
	{
		return View(u);
	}

	/// <summary>
	/// Bind
	/// 
	/// Schreibt Werte aus Query/Route/Form/... direkt in ein Objekt
	/// WICHTIG: Die Namen der Parameter aus Query/Route/Form/... müssen mit den Properties aus der Klasse übereinstimmen
	/// 
	/// Bei zwei Feldern nicht so relevant, bei 10 Feldern eher relevant
	/// </summary>
	[HttpPost]
	public IActionResult UserAnmelden([Bind] [FromForm] User u) //FromForm optional
	{
		return View("Index");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
