using AutoMapper;
using MediTrack.API.DTOs.Patient;
using MediTrack.API.DTOs.Doctor;
using MediTrack.API.Models;


namespace MediTrack.API.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // patient mappings
            // src = source, dest = destination, opt = options
            CreateMap<Patient, PatientResponseDto>()
                .ForMember(dest => dest.AppointmentCount,// PatientResponseDto ko AppointmentCount property lai map garna, jun Patient model ko Appointments collection ko count ho
                opt => opt.MapFrom(src => src.Appointments.Count)); // Patient model ko Appointments collection ko count lai PatientResponseDto ko AppointmentCount property ma map garna
            // Patient mappings
            CreateMap<CreatePatientDto, Patient>();// CreatePatientDto ko properties lai Patient model ma map garna
            CreateMap<UpdatePatientDto, Patient>()// UpdatePatientDto ko properties lai Patient model ma map garna
                .ForAllMembers(opt=>opt.Condition(// ForAllMembers le sabai members ma condition apply garna, jun condition chai srcMember != null ho, yani UpdatePatientDto ko kunai pani property null bhaye, tyo property lai update nagarne
                    (src,dest,srcMember) => srcMember != null)); // Condition le check garna, jun condition chai srcMember != null ho, yani UpdatePatientDto ko kunai pani property null bhaye, tyo property lai update nagarne
                                                                 // UpdatePatientDto ko kunai pani property null bhaye, tyo property lai update nagarne 

            // Doctor mappings
            CreateMap<Doctor, DoctorResponseDto>();
            CreateMap<CreateDoctorDto, Doctor>();


        }

    }
}
