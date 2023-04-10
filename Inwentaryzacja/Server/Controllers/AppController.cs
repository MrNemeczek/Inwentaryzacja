using Inwentaryzacja.Client;
using Inwentaryzacja.Server.Models;
using Inwentaryzacja.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App = Inwentaryzacja.Shared.Models.App;

namespace Inwentaryzacja.Server.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public AppController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        // GET: App
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var apps = await _context.Apps.ToListAsync();
            return Ok(apps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dev = await _context.Apps.FirstOrDefaultAsync(a => a.IdApp == id);
            return Ok(dev);
        }

        [HttpPost]
        public async Task<IActionResult> Post(App app)
        {
            _context.Add(app);
            await _context.SaveChangesAsync();
            return Ok(app.IdApp);
        }

        [HttpPut]
        public async Task<IActionResult> Put(App app)
        {
            _context.Entry(app).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dev = new App { IdApp = id };
            _context.Remove(dev);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
