using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using M000_DataAccess;

namespace M007_ModelBinding.Controllers;

public class KursInhalteController(KursDBContext context) : Controller
{
	// GET: KursInhalte
	public async Task<IActionResult> Index()
	{
		var kursDBContext = context.KursInhalte.Include(k => k.Kurs);
		return View(await kursDBContext.ToListAsync());
	}

	// GET: KursInhalte/Details/5
	public async Task<IActionResult> Details(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var kursInhalte = await context.KursInhalte
			.Include(k => k.Kurs)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (kursInhalte == null)
		{
			return NotFound();
		}

		return View(kursInhalte);
	}

	// GET: KursInhalte/Create
	public IActionResult Create()
	{
		ViewData["KursId"] = new SelectList(context.Kurse, "Id", "Id");
		return View();
	}

	// POST: KursInhalte/Create
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,KursId,InhaltTitel")] KursInhalte kursInhalte)
	{
		if (ModelState.IsValid)
		{
			context.Add(kursInhalte);
			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		ViewData["KursId"] = new SelectList(context.Kurse, "Id", "Id", kursInhalte.KursId);
		return View(kursInhalte);
	}

	// GET: KursInhalte/Edit/5
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var kursInhalte = await context.KursInhalte.FindAsync(id);
		if (kursInhalte == null)
		{
			return NotFound();
		}
		ViewData["KursId"] = new SelectList(context.Kurse, "Id", "Id", kursInhalte.KursId);
		return View(kursInhalte);
	}

	// POST: KursInhalte/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [Bind("Id,KursId,InhaltTitel")] KursInhalte kursInhalte)
	{
		if (id != kursInhalte.Id)
		{
			return NotFound();
		}

		//Prüft, ob die Werte die in dem Objekt, welches per Bind generiert wurde, den DataAnnotations entsprechen
		if (ModelState.IsValid)
		{
			try
			{
				context.Update(kursInhalte);
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!KursInhalteExists(kursInhalte.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return RedirectToAction(nameof(Index));
		}
		ViewData["KursId"] = new SelectList(context.Kurse, "Id", "Id", kursInhalte.KursId);
		return View(kursInhalte);
	}

	// GET: KursInhalte/Delete/5
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var kursInhalte = await context.KursInhalte
			.Include(k => k.Kurs)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (kursInhalte == null)
		{
			return NotFound();
		}

		return View(kursInhalte);
	}

	// POST: KursInhalte/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var kursInhalte = await context.KursInhalte.FindAsync(id);
		if (kursInhalte != null)
		{
			context.KursInhalte.Remove(kursInhalte);
		}

		await context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool KursInhalteExists(int id)
	{
		return context.KursInhalte.Any(e => e.Id == id);
	}
}
