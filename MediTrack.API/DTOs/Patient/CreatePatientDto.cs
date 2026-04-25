using System.ComponentModel.DataAnnotations;
namespace MediTrack.API.DTOs.Patient
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage="Name is Required")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;

        [Range(0,150)]
        public int Age { get; set; }
    }
}
