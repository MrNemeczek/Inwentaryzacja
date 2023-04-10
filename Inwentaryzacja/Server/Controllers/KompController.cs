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
    [Route ("api/[controller]")]
    [ApiController]
    public class KompController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public KompController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// glowna metoda GET
        /// </summary>
        /// <returns>
        /// zwraca wlasciwosci z tabeli komp i dodatkowo nazwe sprzetu z tabeli sprzety i nazwe oddzialu z tabeli oddzialy
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var komps = from k in _context.Komps
                        select new
                        {
                            k.IdKomp,
                            k.KompNazwaDomena,
                            k.ZrzutCzas,
                            k.Ip,
                            k.IdSprzet,
                            k.IdUser,
                            k.IdOddzial,
                            k.IdOddzialNavigation.OddzialNazwa,
                            k.IdSprzetNavigation.NazwaSprzet
                        };

            return Ok(komps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var komp = await _context.Komps.FirstOrDefaultAsync(l => l.IdKomp == id);
            return Ok(komp);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Komp komp)
        {
            _context.Add(komp);
            await _context.SaveChangesAsync();
            
            return Ok(komp.IdKomp);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Komp komp)
        {
            _context.Entry(komp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var komp = new Komp { IdKomp = id };
            _context.Remove(komp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
