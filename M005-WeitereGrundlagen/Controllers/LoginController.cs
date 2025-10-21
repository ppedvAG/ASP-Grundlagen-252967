using M005_WeitereGrundlagen.Models;
using Microsoft.AspNetCore.Mvc;

namespace M005_WeitereGrundlagen.Controllers;

/// <summary>
/// Login Portal
/// 
/// - Registrieren
/// - Einloggen
/// 
/// - Benötigt eine Liste, die per DI eingefügt wird
/// - Views für Registrieren, Einloggen
/// - Controller (hier)
/// - Buttons in _Layout.cshtml
/// </summary>
public class LoginController(ILogger<LoginController> logger, List<User> users) : Controller
{
	/// <summary>
	/// IActionResult
	/// Rückgabewert, welcher einen beliebigen HTTP-Statuscode zurückgeben kann
	/// z.B.: BadRequest(), Forbid(), StatusCode([Zahl]), ...
	/// </summary>
	public IActionResult Index() => View();

	/// <summary>
	/// Rechtsklick -> Add View
	/// </summary>
	public IActionResult Registrieren() => View();

	public IActionResult Einloggen() => View();

	[HttpPost]
	public IActionResult UserAnlegen(string username, string passwort)
	{
		if (users.Any(e => e.Username == username))
		{
			logger.Log(LogLevel.Error, $"Username existiert bereits: {username}");
			return BadRequest();
		}

		User newUser = new User() { Username = username, Password = passwort };
		users.Add(newUser);

		return View("Einloggen");
	}

	[HttpPost]
	public IActionResult UserAnmelden(string username, string passwort, string ab)
	{
		User? foundUser = users.FirstOrDefault(e => e.Username == username);

		//Der User existiert nicht
		if (foundUser == null)
		{
			logger.Log(LogLevel.Error, $"Username existiert nicht: {username}");
			return BadRequest();
		}

		//Das Passwort stimmt nicht überein
		if (foundUser.Password != passwort)
		{
			logger.Log(LogLevel.Error, $"Passwort inkorrekt für: {username}");
			return BadRequest();
		}

		//Eingeloggt bleiben
		if (ab == "on")
		{
			//Hier wird ein Cookie gesetzt, der einen Logintoken speichert
			Response.Cookies.Append("Login", $"U={username}&P={passwort}", new CookieOptions() { Expires = DateTime.MaxValue });
		}

		logger.Log(LogLevel.Information, $"User angemeldet: {username}");
		return RedirectToAction("IndexUser", "Home", foundUser);
	}
}
