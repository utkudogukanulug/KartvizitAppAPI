using Microsoft.AspNetCore.Mvc;
using RestApiProject.DTO;
using RestApiProject.Model.Entities;
using RestApiProject.Model.MyDatabaseContext;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RestApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardVisitController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CardVisitController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("KartvizitGetir")]
        public ActionResult<CardVisit> GetList()
        {
            var datas = _context.CardVisits.ToList();

            var newData = new List<CardVisitDto>();

            foreach (var item in datas)
            {
                var newDto = new CardVisitDto
                {
                    Title = item.Title,
                    Name = item.Name,

                };

                newData.Add(newDto);
            }

            return Ok(newData);
        }

        [HttpPost("KartvizitEkle")]
        public async Task<IActionResult> PostCardVisit(string Title, string Name, string Address, string Phone,string Email)
        {
            var model = new CardVisit
            {
                Title = Title,
                Address = Address,
                Email=Email,
                Name = Name,
                Phone = Phone
            };

            var response = "Başarıyla kayıt oluştu";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CardVisits.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetList), new { Result = response });
        }


        [HttpPut("KartvizitDüzenle/{id}")]
        public async Task<IActionResult> UpdateCardVisit(int id, [FromBody] CardVisitDto cardVisitDto)
        {
            var existingCardVisit = await _context.CardVisits.FindAsync(id);
            if (existingCardVisit == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingCardVisit.Title = cardVisitDto.Title;
            existingCardVisit.Name = cardVisitDto.Name;
            existingCardVisit.Email = cardVisitDto.Email;
            existingCardVisit.Phone = cardVisitDto.Phone;
            existingCardVisit.Address = cardVisitDto.Address;


            _context.CardVisits.Update(existingCardVisit);
            await _context.SaveChangesAsync();

            return Ok(existingCardVisit);
        }

        [HttpDelete("KartvizitSil/{id:int}")]
        public async Task<IActionResult> DeleteCardVisit(int id)
        {
            var cardVisit = await _context.CardVisits.FindAsync(id);
            if (cardVisit == null)
            {
                return NotFound();
            }

            _context.CardVisits.Remove(cardVisit);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }







    }
}