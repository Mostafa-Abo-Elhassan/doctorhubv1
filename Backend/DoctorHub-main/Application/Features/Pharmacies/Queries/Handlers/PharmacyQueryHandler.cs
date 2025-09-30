using Application.Bases;
using Application.Features.Patients.Queries.Models;
using Application.Features.Patients.Queries.Responses;
using Application.Features.Pharmacies.Queries.Models;
using Application.Features.Pharmacies.Queries.Responses;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrescriptionDto = Application.Features.Pharmacies.Queries.Responses.PrescriptionDto;

namespace Application.Features.Pharmacies.Queries.Handlers
{
    public class PharmacyQueryHandler : ResponseHandler,
         IRequestHandler<GetPrescriptionsQuery, Response<List<PrescriptionDto>>>,
         IRequestHandler<GetPrescriptionByIdQuery, Response<PrescriptionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PharmacyQueryHandler> _logger;

        public PharmacyQueryHandler(IUnitOfWork unitOfWork, ILogger<PharmacyQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<List<PrescriptionDto>>> Handle(GetPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching prescriptions with status: {Status}", request.Status);
            var prescriptions = await _unitOfWork.PrescriptionRepository.GetAllWithCareria(p => p.Id == null && request.Status == "pending", new[] { "Patient", "Doctor", "Items" });

            //var prescriptions = await _unitOfWork.Prescriptions
            //    .GetAll()
            //    .AsNoTracking()
            //    .Where(p => p.pharmacyId == null && request.Status == "pending")
            //    .Include(p => p.Patient)
            //    .Include(p => p.Doctor)
            //    .Include(p => p.Items)
            //    .ToListAsync(cancellationToken);

            var prescriptionDtos = prescriptions.Select(p => new PrescriptionDto
            {
                Id = p.Id,
                PatientName = p.Patient.Personal.FullName,
                DoctorName = p.Doctor.Personal.FullName,
                Date = p.IssuedAt,
                Medications = p.Items.Select(i => new MedicationDto
                {
                    Name = i.MedicineName,
                    Dose = i.Dosage,
                    Duration = i.Frequency
                }).ToList()
            }).ToList();

            _logger.LogInformation("Successfully fetched {Count} prescriptions", prescriptionDtos.Count);
            return Success(prescriptionDtos);
        }

        public async Task<Response<PrescriptionDto>> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching prescription with ID: {PrescriptionId}", request.Id);

            var prescription = await _unitOfWork.PrescriptionRepository.GetWithCareria(p => p.Id == request.Id, new[] { "Patient", "Doctor", "Items" });
                //.GetAll()
                //.AsNoTracking()
                //.Include(p => p.Patient)
                //.Include(p => p.Doctor)
                //.Include(p => p.Items)
                //.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (prescription == null)
            {
                _logger.LogWarning("Prescription not found: {PrescriptionId}", request.Id);
                return NotFound<PrescriptionDto>("Prescription not found");
            }

            var prescriptionDto = new PrescriptionDto
            {
                Id = prescription.Id,
                DoctorName = prescription.Doctor.Personal.FullName,
                Date = prescription.IssuedAt,
                Medications = prescription.Items.Select(i => new MedicationDto
                {
                    Name = i.MedicineName,
                    Dose = i.Dosage,
                    Duration = i.Frequency
                }).ToList()
            };

            _logger.LogInformation("Successfully fetched prescription: {PrescriptionId}", request.Id);
            return Success(prescriptionDto);
        }
    }
}
