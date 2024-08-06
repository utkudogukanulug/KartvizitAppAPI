using Microsoft.AspNetCore.Mvc;
using RestApiProject.Model.MyDatabaseContext;


using global::RestApiProject.DTO;
using global::RestApiProject.Model.Entities;


namespace RestApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RegistrationController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("KullanıcıGetir")]
        public ActionResult<User> GetList()
        {
            var datas = _context.Users.ToList();

            var newData = new List<UserDTO>();

            foreach (var item in datas)
            {
                var newDto = new UserDTO
                {
                    Id = item.Id,
                    Email=item.Email,
                    Password=item.Password,
                    Name = item.Name,

                };

                newData.Add(newDto);
            }

            return Ok(newData);
        }

        [HttpPost("KullanıcıEkle")]
        public async Task<IActionResult> PostKullanıcıKayıt( string Name, string Email,string Password)
        {
            var model = new User
            {
                
                Email = Email,
                Password = Password,
                Name = Name,
            };

            var response = "Başarıyla kayıt oluştu";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetList), new { Result = response });
        }


        [HttpPut("KullanıcıBilgiGüncelle/{id}")]
        public async Task<IActionResult> UpdateKullanıcıKY(int id, [FromBody] UserDTO userDTO)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            existingUser.Name = userDTO.Name;
            existingUser.Email = userDTO.Email;
            existingUser.Password = userDTO.Password;
            


            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }

        [HttpDelete("KullanıcıKayıtSil/{id:int}")]
        public async Task<IActionResult> DeleteKullanıcıKayıt(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }







    }
}
