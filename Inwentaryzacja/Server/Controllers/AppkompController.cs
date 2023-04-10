using Inwentaryzacja.Client;
using Inwentaryzacja.Server.Models;
using Inwentaryzacja.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inwentaryzacja.Server.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class AppkompController : Controller
    {
        // GET: AppkompController
        private readonly inwentaryzacjaContext _context;

        public AppkompController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        [HttpGet("{appid}/{kompid}")]
        public async Task<IActionResult> Get(int appid, int kompid)
        {
            
            //znajdywanie rekordow w ktorych idapp == appid
            if(appid != 0)
            {
                var appkomps = from ak in _context.Appkomps
                           where ak.IdApp == appid
                           select new
                           {
                               ak.IdAppkomp,
                               ak.IdApp,
                               ak.IdKomp,
                               ak.IdKompNavigation.KompNazwaDomena,
                               ak.IdAppNavigation.NazwaApp,
                               ak.IdAppNavigation.Blacklist
                           };

                return Ok(appkomps);
            }
            //znajdywanie rekordow w ktorych idkomp == kompid
            else
            {
                var appkomps = from ak in _context.Appkomps
                               where ak.IdKomp == kompid
                               select new
                               {
                                   ak.IdAppkomp,
                                   ak.IdApp,
                                   ak.IdKomp,
                                   ak.IdKompNavigation.KompNazwaDomena,
                                   ak.IdAppNavigation.NazwaApp,
                                   ak.IdAppNavigation.Blacklist
                               };

                return Ok(appkomps);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Appkomp appkomp)
        {
            _context.Add(appkomp);
            await _context.SaveChangesAsync();
            return Ok(appkomp.IdAppkomp);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Appkomp appkomp)
        {
            _context.Entry(appkomp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var appkomp = new Appkomp { IdAppkomp = id };
            _context.Remove(appkomp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
