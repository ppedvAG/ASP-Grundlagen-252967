using M002_Einfuehrung.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M002_Einfuehrung.Controllers;

/// <summary>
/// Dependency Injection
/// 
/// Hier werden Objekte aus der Program.cs empfangen
/// Logger ist standardmäßig immer verfügbar
/// 
/// Im Konstruktor müssen nicht alle registrierten Services verwendet werden
/// </summary>
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	private readonly DITest pageCounter;

	public HomeController(ILogger<HomeController> logger, DITest t)
	{
		_logger = logger;
		pageCounter = t;
	}

	public IActionResult Index()
	{
		pageCounter.Counter++;
		return View();
	}

	public IActionResult Privacy()
	{
		pageCounter.Counter++;
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
