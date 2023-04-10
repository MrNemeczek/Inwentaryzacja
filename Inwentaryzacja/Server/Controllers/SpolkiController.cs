using Inwentaryzacja.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Inwentaryzacja.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Inwentaryzacja.Server.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/spoleczki")]
    [ApiController]
    public class SpolkiController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public SpolkiController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var spolki = await _context.Spolki.ToListAsync();

            return Ok(spolki);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var spolka = await _context.Spolki.FirstOrDefaultAsync(s => s.IdSpolka == id);
            return Ok(spolka);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Spolki spolka)
        {
            _context.Add(spolka);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Spolki spolka)
        {
            _context.Entry(spolka).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Faktury.Any(f => f.IdSpolka == id))
            {
                return BadRequest("Spółka jest używana w fakturze!");
            }
            var spolka = new Spolki { IdSpolka = id };
            _context.Remove(spolka);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
