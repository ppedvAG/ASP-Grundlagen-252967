using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Login;

public class EinloggenModel(ILogger<EinloggenModel> logger, List<User> users) : PageModel
{
	public void OnGet() { }

	/// <summary>
	/// Bei mehreren Get/Post Methoden muss hier die folgende Namenskonvention verwendet werden
	/// OnGet/OnPost + [Name]
	/// </summary>
	public IActionResult OnPostUserAnmelden(string username, string passwort)
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

		logger.Log(LogLevel.Information, $"User angemeldet: {username}");
		return RedirectToPage("/Index", "IndexUser", foundUser);
	}
}