using Application.Bases;
using Application.Features.Hospitals.Queries.Models;
using Application.Features.Hospitals.Queries.Responses;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Queries.Handlers
{
    public class HospitalQueryHandler : ResponseHandler,
         IRequestHandler<GetEmergencyPatientQuery, Response<EmergencyPatientDto>>,
         IRequestHandler<GetHospitalPatientsQuery, Response<List<HospitalPatientDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HospitalQueryHandler> _logger;

        public HospitalQueryHandler(IUnitOfWork unitOfWork, ILogger<HospitalQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<EmergencyPatientDto>> Handle(GetEmergencyPatientQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching emergency patient data for National ID: {NationalId}", request.NationalId);

            var patient = await _unitOfWork.PatientRepository.GetWithCareria(p => p.Personal.NationalId == request.NationalId, new[] { "Prescriptions", "Prescriptions.Items" });
                //.GetAll()
                //.AsNoTracking()
                //.Include(p => p.Prescriptions)
                //.ThenInclude(p => p.Items)
                //.FirstOrDefaultAsync(p => p.Personal.NationalId == request.NationalId, cancellationToken);

            if (patient == null)
            {
                _logger.LogWarning("Patient not found for National ID: {NationalId}", request.NationalId);
                return NotFound<EmergencyPatientDto>("Patient not found");
            }

            var auditLog = new AuditLog
            {
                AdminId = patient.user.Id,
                Action = "EmergencyAccess",
                Description = $"Accessed patient {request.NationalId} in emergency mode",
                Timestamp = DateTime.Now
            };
            await _unitOfWork.AuditLogs.AddAsync(auditLog);
            await _unitOfWork.AuditLogs.SaveChangesAsync();

            var patientDto = new EmergencyPatientDto
            {
                PatientId = patient.Id,
                Name = patient.Personal.FullName,
                ChronicDiseases = patient.Prescriptions.SelectMany(p => p.Notes?.Split(",") ?? Array.Empty<string>()).Distinct().ToList(),
                Allergies = new List<string> { "Penicillin" }, // Placeholder
                CurrentMedications = patient.Prescriptions.SelectMany(p => p.Items.Select(i => i.MedicineName)).Distinct().ToList()
            };

            _logger.LogInformation("Successfully fetched emergency patient data for National ID: {NationalId}", request.NationalId);
            return Success(patientDto);
        }

        public async Task<Response<List<HospitalPatientDto>>> Handle(GetHospitalPatientsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching patients for hospital: {HospitalId}", request.HospitalId);

            var patients = await _unitOfWork.PatientRepository.GetAllWithCareria(p => p.Personal.NationalId==request.HospitalId, new[] { "Appointments", "Prescriptions.Items" });


            //var patients = await _unitOfWork.Patients
            //    .GetAll()
            //    .AsNoTracking()
            //    .Include(p => p.Appointments)
            //    .Where(p => p.Appointments.Any(a => a.HospitalId == request.HospitalId))
            //    .ToListAsync(cancellationToken);

            var patientDtos = patients.Select(p => new HospitalPatientDto
            {
                Id = p.Id,
                Name = p.Personal.FullName,
                Status = p.Appointments.Any(a => a.Status == "Inpatient") ? "Inpatient" : "Discharged"
            }).ToList();

            _logger.LogInformation("Successfully fetched {Count} patients for hospital: {HospitalId}", patientDtos.Count, request.HospitalId);
            return Success(patientDtos);
        }
    }
}
