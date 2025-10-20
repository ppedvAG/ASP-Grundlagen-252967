using M001.Models;
using Microsoft.AspNetCore.Mvc;

namespace M001.Controllers;

public class CalculatorController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Berechne(string z1, string z2, string op)
	{
		if (!int.TryParse(z1, out int zahl1) || !int.TryParse(z2, out int zahl2) || !Enum.TryParse(op, out Rechenoperation rechenoperation))
		{
			return BadRequest();
		}

		double ergebnis = 0;
		switch (rechenoperation)
		{
			case Rechenoperation.Addition:
				ergebnis = zahl1 + zahl2;
				break;
			case Rechenoperation.Subtraktion:
				ergebnis = zahl1 - zahl2;
				break;
			case Rechenoperation.Multiplikation:
				ergebnis = zahl1 * zahl2;
				break;
			case Rechenoperation.Division:
				ergebnis = zahl1 / zahl2;
				break;
		}

		return View("Ergebnis", ergebnis);
	}
}
