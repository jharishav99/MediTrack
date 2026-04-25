using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace MediTrack.API.Models
{
    public class Patient
    {
        [Key]  // primary key bhyo tei bhyera
        public int Id { get; set; }
        [Required] // required bhyesi compulsory field banaucha
        [MaxLength(100)] // 100 samma limit huncha
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)] // max 15 characters ko phone number
        public string Phone { get; set; } = string.Empty;

        public int Age { get; set; }

        // euta patient sanga dherai appointments huncha, so one-to-many relationship
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
