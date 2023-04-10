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
    public class LicencjeController : ControllerBase
    {
        private readonly inwentaryzacjaContext _context;

        public LicencjeController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// glowna metoda GET
        /// </summary>
        /// <returns> zwraca wlasciwosci z tabeli licencje i dodatkowo nazwe faktury z tabeli faktury </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var licencjes = from l in _context.Licencje 
                            select new 
                            { 
                                l.IdLic, 
                                l.Nazwa, 
                                l.Klucz,
                                l.DataWygLic,
                                l.LiczbaLicencji, 
                                l.IdFaktura,
                                l.IdFakturaNavigation.NazwaFaktura 
                            };

            return Ok(licencjes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var licencja = await _context.Licencje.FirstOrDefaultAsync(l => l.IdLic == id);
            return Ok(licencja);
        }

        /// <summary>
        /// metoda GET ktora zwraca liste licencji ktora ma przypisana fakture o podanym id <paramref name="idFaktura"/>
        /// </summary>
        /// <param name="id"> id faktury do wyszukania</param>
        /// <returns> liste licencji ktore maja przypisana fakture o podanym id <paramref name="idFaktura"/>
        /// jezeli nie ma takich zwraca NoContent</returns>
        [HttpGet("faktura/{idfaktura}")]
        public async Task<IActionResult> GetByFaktura(int idFaktura)
        {
            var licencje = await _context.Licencje.Where(l => l.IdFaktura == idFaktura).ToListAsync();
            if(licencje == null)
            {
                return NoContent();
            }
            return Ok(licencje);
        }

        /// <summary>
        /// metoda GET ktora zwraca liste licencji ktore wygasaja ponizej <paramref name="iloscdni"/>
        /// </summary>
        /// <param name="iloscdni"> ilosc dni do wygasniecia licencji ponizej ktorej licencje beda wybierane </param>
        /// <returns> liste licencji ktore wygasaja ponizej <paramref name="iloscdni"/> </returns>
        [HttpGet("wygasajace/{iloscdni}")]
        public async Task<IActionResult> GetExpiring(int iloscdni)
        {
            DateTime expirationDate = DateTime.Today.AddDays(iloscdni);

            var licencje = from l in _context.Licencje where l.DataWygLic < expirationDate select l;

            if (licencje == null)
            {
                return NoContent();
            }
            return Ok(licencje);
        }

        /// <summary>
        /// metoda GET ktora zwraca liste licencji ktore wygasaja ponizej <paramref name="iloscdni"/> i nie wystepuje w <paramref name="ids"/> czyli cookies
        /// </summary>
        /// <param name="iloscdni"> ilosc dni do wygasniecia licencji ponizej ktorej licencje beda wybierane </param>
        /// <param name="ids"> id licencji ktore wystepuje w cookies </param>
        /// <returns> liste licencji ktore wygasaja ponizej <paramref name="iloscdni"/> i nie wystepuje w <paramref name="ids"/> </returns>
        [HttpGet("wygasajace/{iloscdni}/{ids}")]
        public async Task<IActionResult> GetExpiringAndClosed(int iloscdni, string ids)
        {
            DateTime expirationDate = DateTime.Today.AddDays(iloscdni);

            var licencje = from l in _context.Licencje where l.DataWygLic < expirationDate select l;

            if (licencje == null)
            {
                return NoContent();
            }

            if (ids != null && ids != "")
            {
                List<int> IDsToDelete = new List<int>();

                string[] closedLics = ids.Split(",");

                foreach (var licID in closedLics)
                {
                    IDsToDelete.Add(Int32.Parse(licID));
                }

                licencje = licencje.Where(l => !IDsToDelete.Contains(l.IdLic));
            }

            return Ok(licencje);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Licencje licencja)
        {
            _context.Add(licencja);
            await _context.SaveChangesAsync();

            return Ok(licencja.IdLic);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Licencje licencja)
        {
            _context.Entry(licencja).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// usuwa <paramref name="faktura"/> z licencji ktore ja posiadaja
        /// </summary>
        /// <param name="faktura"> faktura ktora ma byc usunieta z licencji </param>
        /// <returns> NoContent </returns>
        [HttpPut("usunfakture")]
        public async Task<IActionResult> Put(Faktury faktura)
        {
            var licencje = from l in _context.Licencje where l.IdFaktura == faktura.IdFaktura select l;

            foreach (var licencja in licencje)
            {
                licencja.IdFaktura = null;
                _context.Entry(licencja).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// przypisuje do kazdej licencji z listy ID <paramref name="licencjeID"/> fakture <paramref name="FakturaID"/>
        /// </summary>
        /// <param name="licencjeID"> lista ID licencji</param>
        /// <param name="FakturaID"> id faktury przypisywanej do sprzetu </param>       
        [HttpPut("{FakturaID}")]
        public async Task<IActionResult> Put(List<int> licencjeID, int FakturaID)
        {
            foreach (int licencjaID in licencjeID)
            {
                Licencje licencja = await _context.Licencje.FirstOrDefaultAsync(l => l.IdLic == licencjaID);
                licencja.IdFaktura = FakturaID;

                _context.Entry(licencja).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var licencja = new Licencje { IdLic = id };
            _context.Remove(licencja);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
