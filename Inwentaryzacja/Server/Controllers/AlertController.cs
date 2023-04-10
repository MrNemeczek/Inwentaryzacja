using Inwentaryzacja.Server.Models;
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
    public class AlertController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public AlertController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// glowna metoda GET do AppKomps ktore sa na czernej liscie
        /// </summary>
        /// <returns> AppKomps ktore sa na czarnej liscie </returns>
        [HttpGet]
        public async Task<IActionResult> GetAppInBlackList()
        {
            var appkomps = from ak in _context.Appkomps
                           where ak.IdAppNavigation.Blacklist == 1
                           select new
                           {
                               ak.IdAppkomp,
                               ak.IdApp,
                               ak.IdKomp,
                               ak.IdKompNavigation.KompNazwaDomena,
                               ak.IdKompNavigation.Ip,
                               ak.IdKompNavigation.IdOddzialNavigation.OddzialNazwa,
                               ak.IdAppNavigation.NazwaApp
                           };

            return Ok(appkomps);

        }

        /// <summary>
        /// metoda GET ktora zwraca AppKomps ktorych IDAppKomp nie wystepuje w <paramref name="ids"/> czyli cookies
        /// </summary>
        /// <returns> 
        /// AppKomps ktorych IdAppKomp nie wystepuje w <paramref name="ids"/>
        /// </returns>
        [HttpGet("appkomp/{ids}")]
        public async Task<IActionResult> GetAppsWithCookies(string ids)
        {
            var appkomps = from ak in _context.Appkomps
                           where ak.IdAppNavigation.Blacklist == 1
                           select new
                           {
                               ak.IdAppkomp,
                               ak.IdApp,
                               ak.IdKomp,
                               ak.IdKompNavigation.KompNazwaDomena,
                               ak.IdKompNavigation.Ip,
                               ak.IdKompNavigation.IdOddzialNavigation.OddzialNazwa,
                               ak.IdAppNavigation.NazwaApp
                           };

            if(ids != null && ids != "")
            {
                List<int> IDsToDelete = new List<int>();

                string[] closedAppKomps = ids.Split(",");

                foreach (var appkompID in closedAppKomps)
                {
                    IDsToDelete.Add(Int32.Parse(appkompID));
                }

                appkomps = appkomps.Where(ak => !IDsToDelete.Contains(ak.IdAppkomp));
            }

            return Ok(appkomps);

        }
    }
}
