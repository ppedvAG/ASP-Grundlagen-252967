using M012_Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Principal;

namespace M012_Authentication.Controllers;

public class HomeController
(
	ILogger<HomeController> logger,
	SignInManager<IdentityUser> sim,
	UserManager<IdentityUser> um,
	RoleManager<IdentityRole> rm  //ACHTUNG: IdentityRole statt IdentityUser
) : Controller
{
	public async Task<IActionResult> Index()
	{
		//Admin Rolle anlegen
		if (!await rm.RoleExistsAsync("Admin"))
		{
			IdentityRole role = new() {	Name = "Admin" };
			Task<IdentityResult> adminRole = rm.CreateAsync(role);
			IdentityResult result = await adminRole;
			
			if (!result.Succeeded)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		//Jetzt eingeloggten User finden, und die neue Rolle zuweisen
		IIdentity? loggedInUser = HttpContext.User.Identity;
		if (loggedInUser != null && loggedInUser.Name != null)
		{
			IdentityUser? actualUser = await um.FindByNameAsync(loggedInUser.Name);
			if (actualUser != null) 
			{
				await um.AddToRoleAsync(actualUser, "Admin");
			}
		}

		return View();
	}

	public IActionResult Privacy()
	{
		//Aufgabe: Privacy Page nur für Admin User zugänglich machen
		if (!HttpContext.User.IsInRole("Admin"))
		{
			return Forbid();
		}

		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
