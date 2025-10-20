using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

/// <summary>
/// Backend Klasse zur Page
/// Enthält genau wie bei MVC auch hier Dependency Injection (über Konstruktor)
/// 
/// Anstatt Methoden mit return View() werden hier void Methoden eingesetzt
/// Kann stattdessen auch IActionResult zurückgeben
/// </summary>
public class IndexModel(ILogger<IndexModel> logger, List<User> users) : PageModel
{
	public int Counter { get; set; }

	public User? CurrentUser { get; set; }

	public void OnGet(int x)
	{
		Counter = x;
	}

	public void OnGetIndexUser(User user)
	{
		CurrentUser = user;
	}
}
