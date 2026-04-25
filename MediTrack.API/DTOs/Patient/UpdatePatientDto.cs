using System.ComponentModel.DataAnnotations;

namespace MediTrack.API.DTOs.Patient
{
    public class UpdatePatientDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(15)]
        public string? Phone { get; set; }
        [Range(0,150)]
        public int? Age { get; set; }
    }
}
