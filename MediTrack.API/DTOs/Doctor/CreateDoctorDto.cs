using System.ComponentModel.DataAnnotations;

namespace MediTrack.API.DTOs.Doctor
{
    public class CreateDoctorDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Specialization{ get; set; } = string.Empty;
    }
}
