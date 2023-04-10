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
    public class OddzialyController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public OddzialyController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var oddzialy = await _context.Oddzialy.ToListAsync();
            return Ok(oddzialy);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var oddzial = await _context.Oddzialy.FirstOrDefaultAsync(o => o.IdOddzialy == id);
            return Ok(oddzial);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Oddzialy oddzial)
        {
            _context.Add(oddzial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Oddzialy oddzial)
        {
            _context.Entry(oddzial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var oddzial = new Oddzialy { IdOddzialy = id };
            _context.Remove(oddzial);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
