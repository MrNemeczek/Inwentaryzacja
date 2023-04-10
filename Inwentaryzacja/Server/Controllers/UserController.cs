using Inwentaryzacja.Server.Models;
using Inwentaryzacja.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inwentaryzacja.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly inwentaryzacjaContext _context;

        public UserController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.User.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{mail}")]
        public async Task<IActionResult> GetByMail(string mail)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Mail == mail);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user.IdUsers);
        }
    }
}
