using System.ComponentModel.DataAnnotations;
namespace MediTrack.API.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Specialization { get; set; } = string.Empty;
    
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
