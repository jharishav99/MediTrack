using System.ComponentModel.DataAnnotations;
namespace MediTrack.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string HashPassword { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Role { get; set; } = string.Empty; // admin or user   
    }
}
