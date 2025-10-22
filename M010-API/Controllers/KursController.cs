using M000_DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M010_API.Controllers;

[ApiController]
[Route("api/Kurse")] //Basisroute für alle API-Methoden festlegen
[Produces("application/json")]
public class KursController(KursDBContext db) : ControllerBase
{
	[HttpGet]
	[Route("AlleKurse")]
	//[Produces("application/json", "application/xml")]
	public IEnumerable<Kurse> GetAllKurse()
	{
		return db.Kurse;
	}

	[HttpGet]
	[Route("InhalteById/{id}")]
	public IEnumerable<KursInhalte> GetInhalteById([FromRoute] int id)
	{
		return db.KursInhalte.Where(e => e.KursId == id);
	}

	[HttpPost]
	[Route("NeuerInhalt/{id}")]
	public IActionResult NeuerInhalt(int id, [FromQuery] string inhaltTitel)
	{
		KursInhalte ki = new KursInhalte();
		ki.KursId = id;
		ki.InhaltTitel = inhaltTitel;

		try
		{
			db.KursInhalte.Add(ki);
			db.SaveChanges();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}

		return Ok();
	}

	// GET: api/Kurs
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Kurse>>> GetKurse()
	{
		return await db.Kurse.ToListAsync();
	}

	// GET: api/Kurs/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Kurse>> GetKurse(int id)
	{
		var kurse = await db.Kurse.FindAsync(id);

		if (kurse == null)
		{
			return NotFound();
		}

		return kurse;
	}

	// PUT: api/Kurs/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public async Task<IActionResult> PutKurse(int id, Kurse kurse)
	{
		kurse.Id = id;
		if (id != kurse.Id)
		{
			return BadRequest();
		}

		db.Entry(kurse).State = EntityState.Modified;

		try
		{
			await db.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!KurseExists(id))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return NoContent();
	}

	// POST: api/Kurs
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<Kurse>> PostKurse(Kurse kurse)
	{
		db.Kurse.Add(kurse);
		await db.SaveChangesAsync();

		return CreatedAtAction("GetKurse", new { id = kurse.Id }, kurse);
	}

	// DELETE: api/Kurs/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteKurse(int id)
	{
		var kurse = await db.Kurse.FindAsync(id);
		if (kurse == null)
		{
			return NotFound();
		}

		db.Kurse.Remove(kurse);
		await db.SaveChangesAsync();

		return NoContent();
	}

	private bool KurseExists(int id)
	{
		return db.Kurse.Any(e => e.Id == id);
	}
}
