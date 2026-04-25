using System.ComponentModel.DataAnnotations;

namespace MediTrack.API.Models
{
    public class Appointment
    {

        [Key]
        public int Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        [MaxLength(200)]
        public string Notes { get; set; } = string.Empty;


        public int PatientId { get; set; }
        public Patient patient { get; set; } = null!; // Navigation property to Patient
         public int DoctorId { get; set; }
        public Doctor doctor { get; set; } = null!; // Navigation property to Doctor



    }
}
