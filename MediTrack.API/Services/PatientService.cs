using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediTrack.API.Data;
using MediTrack.API.DTOs.Patient;
using MediTrack.API.Interfaces;
using MediTrack.API.Models;

namespace MediTrack.API.Services;

public class PatientService: IPatientService
    {

        private readonly AppDbContext _context;
    private readonly IMapper _mapper;    
        public PatientService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientResponseDto>> GetAllAsync() 
    {
        var patients = await _context.Patients
            .Include(p=> p.Appointments)
            .ToListAsync();
        return _mapper.Map<IEnumerable<PatientResponseDto>>(patients);
    }

    public async Task<PatientResponseDto?> GetByIdAsync(int id) 
    {

        var patient = await _context.Patients
            .Include(p => p.Appointments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (patient == null) return null;
        return _mapper.Map<PatientResponseDto>(patient);
    
    }
    public async Task<PatientResponseDto>CreateAsync(CreatePatientDto dto) 
    
    { 
        var patient = _mapper.Map<Patient>(dto);
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return _mapper.Map<PatientResponseDto>(patient);

    }
    public async Task<PatientResponseDto?> UpdateAsync(int id, UpdatePatientDto dto) 
    
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return null;

        _mapper.Map(dto, patient);
        await _context.SaveChangesAsync();
        return _mapper.Map<PatientResponseDto>(patient);


    }
    public async Task<bool> DeleteAsync(int id)
    
    {
    var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return false;
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return true;


    }
}
