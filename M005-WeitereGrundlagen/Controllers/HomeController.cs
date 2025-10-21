using M005_WeitereGrundlagen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M005_WeitereGrundlagen.Controllers;

public class HomeController(ILogger<HomeController> logger, List<User> users) : Controller
{
	public IActionResult Index()
	{
		User? u = null;
		string? login = HttpContext.Request.Cookies["Login"];
		if (login != null)
		{
			string[] up = login.Split('&');
			if (up.Length > 0)
			{
				string username = up[0].Split("=")[1];
				string passwort = up[1].Split("=")[1];

				u = users.First(e => e.Username == username && e.Password == passwort);
			}
		}
		return View(u);
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[HttpPost]
	public IActionResult FileUpload(IFormFile f)
	{
		using (StreamWriter sw = new StreamWriter(f.FileName))
		{
			f.CopyTo(sw.BaseStream);
		}

		return View("Privacy");
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
