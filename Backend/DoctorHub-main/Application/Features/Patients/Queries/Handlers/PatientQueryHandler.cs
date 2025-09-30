using Application.Bases;
using Application.Features.Patients.Queries.Models;
using Application.Features.Patients.Queries.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.Handlers
{
    public class PatientQueryHandler : ResponseHandler,
         IRequestHandler<GetPatientDashboardQuery, Response<PatientDashboardDto>>,
         IRequestHandler<GetPatientAppointmentsQuery, Response<List<AppointmentDto>>>,
         IRequestHandler<GetPatientMedicalRecordsQuery, Response<List<MedicalRecordDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientQueryHandler> _logger;

        public PatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PatientQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<PatientDashboardDto>> Handle(GetPatientDashboardQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching dashboard for patient with National ID: {NationalId}", request.NationalId);

            var patient = await _unitOfWork.PatientRepository.GetWithCareria(
      p => p.Id == request.NationalId,
      new[] { "Prescriptions.Doctor", "LabResults", "Appointments.Doctor", "Notifications", "Children", "ConsentRecords" } );


            if (patient == null)
            {
                _logger.LogWarning("Patient not found for National ID: {NationalId}", request.NationalId);
                return NotFound<PatientDashboardDto>("Patient not found");
            }

            // Verify user identity from JWT
            var userId = await _unitOfWork.UserRepository.GetByIdAsync(request.NationalId);
            if (userId ==null)
            {
                _logger.LogWarning("Unauthorized access attempt for National ID: {NationalId}", request.NationalId);
                return NotFound<PatientDashboardDto>("Unauthorized access");
            }

            var patientLastPrescription = patient.Prescriptions.OrderByDescending(p => p.IssuedAt).FirstOrDefault();
            var lastLabResult = patient.labResults
     .OrderByDescending(l => l.UploadedAt)
     .FirstOrDefault();

            LabResultDto? labResultDto = null;

            if (lastLabResult != null)
            {
                labResultDto = new LabResultDto
                {
                    Id = lastLabResult.Id,
                    Type = lastLabResult.Type,
                    Date = lastLabResult.UploadedAt
                };
            }

            var upcomingAppointment = patient.Appointments
                .Where(a => a.ScheduledAt > DateTime.UtcNow)
                .OrderBy(a => a.ScheduledAt)
                .FirstOrDefault();

            AppointmentDto? appointmentDto = null;

            if (upcomingAppointment != null)
            {
                appointmentDto = new AppointmentDto
                {
                    Id = upcomingAppointment.Id,
                    DoctorName = upcomingAppointment.Doctor != null
                        ? upcomingAppointment.Doctor.Personal.FullName
                        : string.Empty,
                    Date = upcomingAppointment.ScheduledAt,
                    Status = upcomingAppointment.Status
                };
            }



            var dashboard = new PatientDashboardDto
            { 
      
                  LastPrescription = new PrescriptionDto
                  {
                      Id = patientLastPrescription.Id,
                       Date = patientLastPrescription.IssuedAt,
                      DoctorName = patientLastPrescription.Doctor != null
                         ? patientLastPrescription.Doctor.Personal.FullName
                         : string.Empty,
                      // لو عندك PrescriptionItems أو تفاصيل تانية تقدر تضيفها هنا برضه
                  },


                LastLabResult = labResultDto,

                    UpcomingAppointment = appointmentDto



            };

            _logger.LogInformation("Successfully fetched dashboard for patient with National ID: {NationalId}", request.NationalId);
            return Success(dashboard);
        }

        public async Task<Response<List<AppointmentDto>>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching appointments for patient: {PatientId}", request.PatientId);

            var appointments = await _unitOfWork.AppointmentRepository.GetAllWithCareria(a => a.PatientId == request.PatientId, new[] { "Doctor" }, cancellationToken);
             

            if (appointments ==null)
            {
                _logger.LogInformation("No appointments found for patient: {PatientId}", request.PatientId);
            }

            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);
            return Success(appointmentDtos);
        }

        public async Task<Response<List<MedicalRecordDto>>> Handle(GetPatientMedicalRecordsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching medical records for patient with National ID: {NationalId}, Type: {Type}", request.NationalId, request.Type);

            var patient = await _unitOfWork.PatientRepository.GetWithCareria(a => a.Id == request.NationalId, new[] { "Prescriptions", "LabResults", "Personal" });

               

            if (patient == null)
            {
                _logger.LogWarning("Patient not found for National ID: {NationalId}", request.NationalId);
                return NotFound<List<MedicalRecordDto>>("Patient not found");
            }

            var records = new List<MedicalRecordDto>();
            // ✅ Prescriptions
            if (string.IsNullOrEmpty(request.Type) || request.Type == "prescriptions")
            {
                records.AddRange(patient.Prescriptions.Select(p => new MedicalRecordDto
                {
                    Id = p.Id,
                    Type = "Prescription",
                    Name = $"Prescription by Dr. {p.Doctor?.Personal.FullName ?? "Unknown"}",
                    Date = p.IssuedAt
                }));
            }

            // ✅ Lab + Radiology Results
            if (string.IsNullOrEmpty(request.Type) || request.Type == "lab" || request.Type == "radiology")
            {
                records.AddRange(patient.labResults.Select(l => new MedicalRecordDto
                {
                    Id = l.Id,
                    Type = l.Type?.Equals("Radiology", StringComparison.OrdinalIgnoreCase) == true
                            ? "Radiology"
                            : "Lab",
                    Name = $"{l.Type} Result",
                    Date = l.UploadedAt
                }));
            }

            _logger.LogInformation("Successfully fetched {Count} medical records for patient with National ID: {NationalId}", records.Count, request.NationalId);
            return Success(records.OrderByDescending(r => r.Date).ToList());
        }
    }
}