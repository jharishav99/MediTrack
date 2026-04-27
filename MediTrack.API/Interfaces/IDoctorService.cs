using MediTrack.API.DTOs.Doctor;


namespace MediTrack.API.Interfaces;

    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDto>> GetAllSync();
        Task<DoctorResponseDto> GetByIdAsync(int id);
        Task<DoctorResponseDto> CreateIdAsync(CreateDoctorDto dto);
        Task<bool> DeleteIdAsync(int id);
}
