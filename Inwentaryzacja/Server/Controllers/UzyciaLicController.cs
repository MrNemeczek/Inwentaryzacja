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
    [Route("api/uzycialic")]
    [ApiController]
    public class UzyciaLicController : Controller
    {
        private readonly inwentaryzacjaContext _context;
        public UzyciaLicController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var uzyciaLicencji = await _context.UzyciaLicencji.ToListAsync();

            return Ok(uzyciaLicencji);
        }

        /// <summary>
        /// metoda GET ktora zwraca liste uzyc licencji o ID <paramref name="idlic"/>
        /// </summary>
        /// <param name="idlic"> id licencji do wyszukania </param>
        /// <returns> zwraca liste uzyc licencji o ID <paramref name="idlic"/> oraz nazwe komputerow przypisanych do licencji</returns>
        [HttpGet("licencja/{idlic}")]
        public async Task<IActionResult> GetByLicencja(int idlic)
        {
            var uzyciaLicencji = from u in _context.UzyciaLicencji
                                 where u.IdLic == idlic
                                 select new
                                {
                                     u.IdUzyciaLicencji,
                                     u.IdLic,
                                     u.IdKomp,
                                     u.IdKompNavigation.KompNazwaDomena
                                };

            if (uzyciaLicencji == null)
            {
                return NoContent();
            }

            return Ok(uzyciaLicencji);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UzyciaLicencji uzycieLic)
        {
            _context.Add(uzycieLic);
            await _context.SaveChangesAsync();

            return Ok(uzycieLic.IdLic);
        }

        /// <summary>
        /// dodaje do wszystkich komputerow ktore sa w <paramref name="kompsID"/> licencje o id <paramref name="LicID"/> 
        /// </summary>
        /// <param name="kompsID"> lista id komputerow</param>
        /// <param name="LicID"> id licencji do ktorej trzeba dodac komputery</param>
        [HttpPost("{LicID}")]
        public async Task<IActionResult> Post(List<int> kompsID, int LicID)
        {
            foreach (int kompID in kompsID)
            {
                UzyciaLicencji uzycieLic = new UzyciaLicencji() { IdKomp = kompID, IdLic = LicID };

                _context.Add(uzycieLic);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var uzycieLic = new UzyciaLicencji { IdUzyciaLicencji = id };
            _context.Remove(uzycieLic);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
