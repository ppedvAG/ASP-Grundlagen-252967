using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Rechner;

public class ErgebnisModel : PageModel
{
	public int Ergebnis { get; set; }

	public void OnGet(int e)
	{
		Ergebnis = e;
	}
}
