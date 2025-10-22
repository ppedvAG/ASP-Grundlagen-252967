using M008_Lokalisierung.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace M008_Lokalisierung.Controllers;

public class HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> loc) : Controller
{
	public IActionResult Index()
	{
		//CultureInfo
		//System für die Lokalisierung von Inhalten
		//u.a. Tausendertrennzeichen/Dezimalzeichen, verschiedene Sprachen, ...

		//CultureInfo.CurrentCulture = new CultureInfo("en");
		//CultureInfo.CurrentUICulture = new CultureInfo("en");

		LocalizedString str = loc.GetString("Test");

		return View("Index", str.Value);
	}

	public IActionResult Privacy()
	{
		return View();
	}

	public IActionResult SetLanguage(string language, string returnUrl)
	{
		HttpContext.Response.Cookies.Append("Language", language, new CookieOptions() { Expires = DateTime.MaxValue });

		return View("Index");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
