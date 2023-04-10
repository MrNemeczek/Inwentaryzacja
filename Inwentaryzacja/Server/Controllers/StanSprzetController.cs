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
    public class StanSprzetController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public StanSprzetController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stanySprzetow = await _context.StanSprzet.ToListAsync();
            return Ok(stanySprzetow);
        }
    }
}
