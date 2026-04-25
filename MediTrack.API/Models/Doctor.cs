using System.ComponentModel.DataAnnotations;
namespace MediTrack.API.Models
{
    public class Doctor
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Specialization { get; set; } = string.Empty;
    
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
