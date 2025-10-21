using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Rechner;

public class RechnerModel : PageModel
{
	public int Anzahl { get; set; } = 2;

	public void OnGet(string anz)
	{
		if (int.TryParse(anz, out int a))
			if (a > 1)
				Anzahl = a;
	}

	public IActionResult OnPostAnzahlEingabe(string anz)
	{
		if (!int.TryParse(anz, out int a))
			return BadRequest();
		return RedirectToPage("Rechner", new { anz = a });
	}

	public IActionResult OnPostBerechne(string[] zahlen, string op)
	{
		List<int> z = [];
		foreach (string str in zahlen)
		{
			if (!int.TryParse(str, out int x))
				return BadRequest();
			z.Add(x);
		}

		if (!Enum.TryParse(op, out Rechenoperation rechenoperation) || rechenoperation == Rechenoperation.Keine)
			return BadRequest();

		double ergebnis = z[0];
		foreach (int x in z)
		{
			switch (rechenoperation)
			{
				case Rechenoperation.Addition:
					ergebnis += x;
					break;
				case Rechenoperation.Subtraktion:
					ergebnis -= x;
					break;
				case Rechenoperation.Multiplikation:
					ergebnis *= x;
					break;
				case Rechenoperation.Division:
					ergebnis /= 0;
					break;
			}
		}

		return RedirectToPage("Ergebnis", new { e = ergebnis });
	}
}