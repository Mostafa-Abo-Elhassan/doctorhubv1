
using Application.Features.Patients.Queries.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.PatientsMap
{
    public class PatientsProfile : Profile
    {

        //CreateMap<Appointment, AppointmentDto>()
        //        .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Personal.FullName));
        //    CreateMap<Prescription, PrescriptionDto>()
        //        .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Personal.FullName));
        //    CreateMap<LabResult, LabResultDto>()
        //        .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.UploadedAt));
        //    CreateMap<Prescription, MedicalRecordDto>()
        //        .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Prescription"))
        //        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Items.FirstOrDefault().MedicineName))
        //        .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.IssuedAt));
        //    CreateMap<LabResult, MedicalRecordDto>()
        //        .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
        //        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LapName))
        //        .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.UploadedAt));
        

    }

}
