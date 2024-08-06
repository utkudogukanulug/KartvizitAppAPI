using Microsoft.AspNetCore.Mvc;
using RestApiProject.Model.MyDatabaseContext;



using global::RestApiProject.DTO;
using global::RestApiProject.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace RestApiProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            var user = await _context.Users
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return Ok("Giriş yapılıyor...");
            }
            else
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı");
            }

        }






        }
}
