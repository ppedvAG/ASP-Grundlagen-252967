using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Login;

public class RegistrierenModel(ILogger<RegistrierenModel> logger, List<User> users) : PageModel
{
	public IActionResult OnPostUserAnlegen(string username, string passwort)
	{
		if (users.Any(e => e.Username == username))
		{
			logger.Log(LogLevel.Error, $"Username existiert bereits: {username}");
			return BadRequest();
		}

		User newUser = new User() { Username = username, Password = passwort };
		users.Add(newUser);

		return RedirectToPage("Einloggen");
	}
}