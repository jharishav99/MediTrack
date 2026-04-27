using AutoMapper;
using MediTrack.API.Data;
using MediTrack.API.DTOs.Doctor;
using MediTrack.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;



namespace MediTrack.API.Services
{
    public class DoctorService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DoctorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DoctorResponseDto>> GetAllSync()
        {
            var doctor = await _context.Doctors.ToListAsync();
            return _mapper.Map<IEnumerable<DoctorResponseDto>>(doctor);
        }
       public async Task<DoctorResponseDto?> GetByIdAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        return doctor == null ? null : _mapper.Map<DoctorResponseDto>(doctor);
    }
       public async Task<DoctorResponseDto> CreateIdAsync(CreateDoctorDto dto)
        {
            var doctor = _mapper.Map<Doctor>(dto);
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return _mapper.Map<DoctorResponseDto>(doctor);
        }
       public async Task<bool> DeleteIdsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
