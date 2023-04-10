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
    public class TypFakturyController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public TypFakturyController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        // GET: App
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var typFaktury = await _context.TypFaktury.ToListAsync();

            return Ok(typFaktury);
        }
    }
}
