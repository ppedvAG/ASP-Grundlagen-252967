using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace M008_Lokalisierung.Middleware;

/// <summary>
/// Grundaufbau
/// 
/// - Delegate Variable für die nächste Methode in der Middleware-Kette
/// - Konstruktor, welcher die nächste Methode empfängt
/// - Invoke: Führt den Code der Middleware aus, und startet die nächste Methode der Middleware-Kette
/// </summary>
public class LocalizationMiddleware
{
	private readonly RequestDelegate _next; //Funktionszeiger

	public LocalizationMiddleware(RequestDelegate _next)
	{
		this._next = _next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		//2 Aufgaben
		//Cookies inspizieren (Niedrigere Priorität)
		//lang=... achten (Höhere Priorität)

		string? languageCookie = context.Request.Cookies["Language"];
		if (languageCookie != null)
		{
			SetCulture(languageCookie);
		}

		StringValues v = context.Request.Query["lang"]; //Derselbe Key kann beim QueryString mehrmals angegeben werden
		if (v != StringValues.Empty)
		{
			string lang = v.First()!;
			SetCulture(lang);
		}

		await _next(context);
	}

	private void SetCulture(string c)
	{
		CultureInfo culture = new CultureInfo(c);
		CultureInfo.CurrentCulture = culture;
		CultureInfo.CurrentUICulture = culture;
	}
}