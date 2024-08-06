using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RestApiProject.Model.Entities
{
    public class CardVisit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }
        
        public string? Name { get; set; }
        
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}
