using Inwentaryzacja.Server.Models;
using Inwentaryzacja.Shared.Models;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inwentaryzacja.Server.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class FakturyController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public FakturyController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// glowna metoda GET
        /// </summary>
        /// <returns> zwraca wlasciwosci z tabeli faktury(oprocz plik_nazwa i plik_faktura) i dodatkowo nazwe typu faktury z tabeli 
        /// typ_faktury oraz nazwe spolki z tabeli spolki
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var faktury = from f in _context.Faktury
                          select new
                          {
                              f.IdFaktura,
                              f.NazwaFaktura, 
                              f.DataFaktura, 
                              f.KwotaFaktura,
                              f.IdSpolka,
                              f.IdTypFaktura,
                              f.IdSpolkaNavigation.NazwaSpolka,
                              f.IdTypFakturaNavigation.NazwaTypFaktury
                          };

            return Ok(faktury);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var faktura = await _context.Faktury.FirstOrDefaultAsync(f => f.IdFaktura == id);
            return Ok(faktura);
        }

        /// <summary>
        /// metoda GET ktora zwraca liste faktur ktore maja dany typ na podstawie ID <paramref name="idtyp"/>
        /// </summary>
        /// <param name="idtyp"> ID typu faktury do wyszukania </param>
        /// <returns> zwraca liste faktur ktore maja dany typ na podstawie ID <paramref name="idtyp"/> </returns>
        [HttpGet("typfaktury/{idtyp}")]
        public async Task<IActionResult> GetByTypFaktury(int idtyp)
        {
            var faktury = await _context.Faktury.Where(f => f.IdTypFaktura == idtyp).ToListAsync();

            if(faktury == null)
            {
                return NoContent();
            }

            return Ok(faktury);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Faktury faktura)
        {
            _context.Add(faktura);
            await _context.SaveChangesAsync();

            return Ok(faktura.IdFaktura);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Faktury faktura)
        {
            _context.Entry(faktura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var faktura = new Faktury { IdFaktura = id };
            _context.Remove(faktura);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
