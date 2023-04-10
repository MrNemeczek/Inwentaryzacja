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
    public class SprzetController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public SprzetController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// glowna metoda GET
        /// </summary>
        /// <returns> zwraca wlasciwosci z tabeli sprzety i dodatkowo nazwy kluczow obcych (np. nazwa faktury) </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sprzety = from s in _context.Sprzet
                          select new
                          {
                              s.IdSprzet,
                              s.NazwaSprzet,
                              s.IdFaktura,
                              s.IdTypSprzet,
                              s.IdStan,
                              s.IdOddzialy,
                              s.NumerSeryjny,
                              s.IdSpolka,
                              s.IdNrInwentaryzacyjny,
                              s.IdNrInwentaryzacyjnyNew,
                              s.Comment,
                              s.Model,
                              s.Marka,
                              s.IdFakturaNavigation.NazwaFaktura,
                              s.IdNrInwentaryzacyjnyNavigation.Numer,
                              s.IdTypSprzetNavigation.Typ,
                              s.IdSpolkaNavigation.NazwaSpolka,
                              s.IdStanNavigation.Stan,
                              s.IdOddzialyNavigation.OddzialNazwa,
                              s.IdNrInwentaryzacyjnyNewNavigation.NumerNew
                          };

            return Ok(sprzety);
        }

        /// <summary>
        /// metoda GET zwraca IdSprzetu i idNrInwentaryzacyjny pomijajac rekordy z idNrInwentaryzacyjny == null
        /// </summary>
        /// <returns> json z idSprzet i idNrInwentaryzacyjny </returns>
        [HttpGet("nrinw")]
        public async Task<IActionResult> GetSprzetWithNrIwn()
        {
            var sprzety = from s in _context.Sprzet
                          where s.IdNrInwentaryzacyjny != null
                          select new
                          {
                              s.IdSprzet,
                              s.IdNrInwentaryzacyjny,
                          };

            return Ok(sprzety);
        }

        /// <summary>
        /// metoda GET zwraca IdSprzetu i idNrInwentaryzacyjny pomijajac rekordy z idNrInwentaryzacyjny == null
        /// </summary>
        /// <returns> json z idSprzet i idNrInwentaryzacyjny </returns>
        [HttpGet("nrinwnew")]
        public async Task<IActionResult> GetSprzetWithNrIwnNew()
        {
            var sprzety = from s in _context.Sprzet
                          where s.IdNrInwentaryzacyjnyNew != null
                          select new
                          {
                              s.IdSprzet,
                              s.IdNrInwentaryzacyjnyNew,
                          };

            return Ok(sprzety);
        }

        /// <summary>
        /// metoda GET ktora zwraca liste sprzetow ktore maja faktue o ID <paramref name="idfaktura"/>
        /// </summary>
        /// <param name="idfaktura"> id faktury wedlug ktorej przeszukuje sie sprzety</param>
        /// <returns> liste sprzetow  ktore maja faktue o ID <paramref name="idfaktura"/> </returns>
        [HttpGet("faktura/{idfaktura}")]
        public async Task<IActionResult> GetByFaktura(int idfaktura)
        {
            var sprzety = await _context.Sprzet.Where(s => s.IdFaktura == idfaktura).ToListAsync();

            return Ok(sprzety);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sprzet = await _context.Sprzet.FirstOrDefaultAsync(s => s.IdSprzet == id);
            return Ok(sprzet);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Sprzet sprzet)
        {
            _context.Add(sprzet);
            await _context.SaveChangesAsync();
            return Ok(sprzet.IdSprzet);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Sprzet sprzet)
        {
            _context.Entry(sprzet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// usuwa <paramref name="faktura"/> ze sprzetu ktory ja posiadaja
        /// </summary>
        /// <param name="faktura"> faktura ktora ma byc usunieta ze sprzetu </param>
        /// <returns> NoContent </returns>
        [HttpPut("usunfakture")]
        public async Task<IActionResult> Put(Faktury faktura)
        {
            var sprzety = from s in _context.Sprzet where s.IdFaktura == faktura.IdFaktura select s;

            foreach (var sprzet in sprzety)
            {
                sprzet.IdFaktura = null;
                _context.Entry(sprzet).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// przypisuje do kazdego sprzetu z listy ID <paramref name="sprzetyID"/> fakture <paramref name="FakturaID"/>
        /// </summary>
        /// <param name="sprzetyID"> lista id sprzetow</param>
        /// <param name="FakturaID"> id faktury przypisywanej do sprzetu</param>
        [HttpPut("{FakturaID}")]
        public async Task<IActionResult> Put(List<int> sprzetyID, int FakturaID)
        {
            foreach (int sprzetID in sprzetyID)
            {
                Sprzet sprzet = await _context.Sprzet.FirstOrDefaultAsync(s => s.IdSprzet == sprzetID);
                sprzet.IdFaktura = FakturaID;

                _context.Entry(sprzet).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sprzet = new Sprzet { IdSprzet = id };
            _context.Remove(sprzet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
