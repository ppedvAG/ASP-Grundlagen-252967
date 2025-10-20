using System.Diagnostics;
using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace M003_MVC.Controllers;

/// <summary>
/// Primary Constructor
/// 
/// Einfache Schreibweise für Konstruktoren
/// Legt im Hintergrund eine Variable an, und weist den Wert aus dem Konstruktorparameter auf diese Variable zu
/// </summary>
public class HomeController(ILogger<HomeController> logger) : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	public IActionResult IndexUser(User user)
	{
		return View("Index", user);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
