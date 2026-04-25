namespace MediTrack.API.DTOs.Patient
{
    public class PatientResponseDto
    {

        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Age { get; set; }
        public int AppointmentCount { get; set; }
    }
}
