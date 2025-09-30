using Application.Bases;
using Application.Features.Doctors.Queries.Models;
using Application.Features.Doctors.Queries.Responses;
using Application.Features.Patients.Queries.Responses;
using Domain.Entities.Enums;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.Handlers
{
    public class DoctorQueryHandler : ResponseHandler,
      IRequestHandler<GetDoctorDashboardQuery, Response<DoctorDashboardDto>>,
      IRequestHandler<GetPatientFileQuery, Response<PatientFileDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DoctorQueryHandler> _logger;

        public DoctorQueryHandler(IUnitOfWork unitOfWork, ILogger<DoctorQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // 🔹 Dashboard
        public async Task<Response<DoctorDashboardDto>> Handle(GetDoctorDashboardQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching dashboard for doctor: {DoctorId}", request.DoctorId);

            var today = DateTime.Now.Date;

            var appointments = await _unitOfWork.AppointmentRepository
                .GetAllWithCareria(
                    a => a.DoctorId == request.DoctorId && a.ScheduledAt.Date == today,
                    new[] { "Patient.Personal", "Doctor.Personal" },
                    cancellationToken);

            var alerts = await _unitOfWork.MedicationInteractionRepository
                .GetFilteredProjectedWithIncludesAsync(
                    m => m.MedicationA.Prescription.DoctorId == request.DoctorId,   // الفلترة
                    m => new AIAlertDto                                            // الـ projection
                    {
                        PatientName = m.MedicationA.Prescription.Patient.Personal.FullName,
                        Issue = m.Description
                    },
                    new[] { "MedicationA.Prescription.Doctor", "MedicationA.Prescription.Patient" }, // includes
                    cancellationToken);

            var appointmentDtos = appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                DoctorName = a.Doctor.Personal.FullName,
                Date = a.ScheduledAt,
                Status = a.Status
            }).ToList();

            var dashboard = new DoctorDashboardDto
            {
                TodayAppointments = appointmentDtos,
                AIAlerts = alerts
            };

            _logger.LogInformation("Successfully fetched dashboard for doctor: {DoctorId}", request.DoctorId);
            return Success(dashboard);
        }

        // 🔹 Patient File
        public async Task<Response<PatientFileDto>> Handle(GetPatientFileQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching patient file for patient: {PatientId} by doctor: {DoctorId}", request.PatientId, request.DoctorId);

            

            var patient = await _unitOfWork.PatientRepository.GetWithCareria(
    p => p.Id == request.NationalId,
    new[] { "Prescriptions.Items", "LabResults", "Personal" });




            if (patient == null)
            {
                _logger.LogWarning("Patient not found: {PatientId}", request.PatientId);
                return NotFound<PatientFileDto>("Patient not found");
            }

            var hasAccess = await _unitOfWork.ConsentRecordRepository
                .GetWithCareria(
                               c => c.PatientId == request.PatientId &&
                               c.GrantedToUserId == request.DoctorId &&
                               c.Status == ConsentStatus.Active);

                //.GetAll()
                //.AnyAsync(c => c.PatientId == request.PatientId &&
                //               c.GrantedToUserId == request.DoctorId &&
                //               c.Status == ConsentStatus.Active, cancellationToken);

            if (hasAccess==null)
            {
                _logger.LogWarning("Doctor {DoctorId} does not have access to patient: {PatientId}", request.DoctorId, request.PatientId);
                return NotFound<PatientFileDto>("Access to patient file denied");
            }

            // حساب العمر صح
            var dob = patient.Personal.DateOfBirth;
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;

            var patientFile = new PatientFileDto
            {
                PatientId = patient.Id,
                Name = patient.Personal.FullName,
                Age = age,
                History = new PatientHistoryDto
                {
                    Diagnoses = patient.Prescriptions
                        .SelectMany(p => p.Notes?.Split(",") ?? Array.Empty<string>())
                        .Distinct()
                        .ToList(),

                    Medications = patient.Prescriptions
                        .SelectMany(p => p.Items.Select(i => i.MedicineName))
                        .Distinct()
                        .ToList(),

                    Labs = patient.labResults.Select(l => new LabResultDto
                    {
                        Id = l.Id,
                        Type = l.Type,
                        Date = l.UploadedAt
                    }).ToList()
                }
            };

            _logger.LogInformation("Successfully fetched patient file for patient: {PatientId}", request.PatientId);
            return Success(patientFile);
        }
    }
}
