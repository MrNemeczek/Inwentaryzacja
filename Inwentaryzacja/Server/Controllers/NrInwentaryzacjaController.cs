using Inwentaryzacja.Server.Models;
using Inwentaryzacja.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inwentaryzacja.Server.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class NrInwentaryzacjaController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public NrInwentaryzacjaController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var numeryInwentaryzacyjne = await _context.NumeryInwentaryzacyjne.ToListAsync();

            return Ok(numeryInwentaryzacyjne);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var numerInwentaryzacyjny = await _context.NumeryInwentaryzacyjne.FirstOrDefaultAsync(ni => ni.IdNrInwentaryzacyjny == id);
            return Ok(numerInwentaryzacyjny);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NumeryInwentaryzacyjne numerInwentaryzacyjny)
        {
            if (_context.NumeryInwentaryzacyjne.Select(nr => nr.Numer).Contains(numerInwentaryzacyjny.Numer))
            {
                return StatusCode(422);
            }

            _context.Add(numerInwentaryzacyjny);
            await _context.SaveChangesAsync();

            return Ok(numerInwentaryzacyjny.IdNrInwentaryzacyjny);
        }

        [HttpPut]
        public async Task<IActionResult> Put(NumeryInwentaryzacyjne numerInwentaryzacyjny)
        {
            if (_context.NumeryInwentaryzacyjne.Select(nr => nr.Numer).Contains(numerInwentaryzacyjny.Numer))
            {
                return StatusCode(422);
            }

            _context.Entry(numerInwentaryzacyjny).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var numerInwentaryzacyjny = new NumeryInwentaryzacyjne { IdNrInwentaryzacyjny = id };
            _context.Remove(numerInwentaryzacyjny);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
