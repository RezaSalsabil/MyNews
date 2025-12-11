using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNews.Models;
using MyNews.Models.Dto.User;

namespace MyNews.Controllers
{
    public class UserController : Controller
    {
        private readonly MyNewsContext _context;
        public UserController(MyNewsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            var user = await _context.TblUser.Include(i =>i.Role).FirstOrDefaultAsync( i => i.UserName == dto.UserName);
            if (user == null)
            {
                return BadRequest("UserName or Password is Wrong!");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
                var user = dto.toUser();
            if (await _context.TblUser.AnyAsync(i => i.UserName == dto.UserName))
            {
                return BadRequest("This UserName is Taken");
            }
            await _context.TblUser.AddAsync(user);
            await _context.SaveChangesAsync();
            return Created();
        }

        public async Task<IActionResult> notvalid()
        {
            return View();
        }



    }
}
