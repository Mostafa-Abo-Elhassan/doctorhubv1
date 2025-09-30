using Application.Bases;
using Application.Features.Patients.Commands.Models;
using Application.Features.Patients.Queries.Responses;
using Application.Features.Pharmacies.Commands.Models;
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

namespace Application.Features.Pharmacies.Commands.Handlers
{
    public class PharmacyCommandHandler : ResponseHandler,
        IRequestHandler<DispensePrescriptionCommand, Response<PrescriptionDispensedDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PharmacyCommandHandler> _logger;

        public PharmacyCommandHandler(IUnitOfWork unitOfWork, ILogger<PharmacyCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<PrescriptionDispensedDto>> Handle(DispensePrescriptionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Dispensing prescription: {PrescriptionId} by pharmacist: {PharmacistId}", request.PrescriptionId, request.PharmacistId);

            var prescription = await _unitOfWork.PrescriptionRepository.GetByIdAsync(request.PrescriptionId);
            if (prescription == null)
            {
                _logger.LogWarning("Prescription not found: {PrescriptionId}", request.PrescriptionId);
                return NotFound<PrescriptionDispensedDto>("Prescription not found");
            }

            var pharmacy = await _unitOfWork.PharmacyRepository.GetByIdAsync(request.PharmacistId);
            if (pharmacy == null)
            {
                _logger.LogWarning("Pharmacist not found: {PharmacistId}", request.PharmacistId);
                return NotFound<PrescriptionDispensedDto>("Pharmacist not found");
            }

            prescription.pharmacyId = request.PharmacistId;
            prescription.pharmacy = pharmacy;
            await _unitOfWork.PharmacyRepository.SaveChangesAsync();

            var result = new PrescriptionDispensedDto
            {
                PrescriptionId = prescription.Id,
                Status = "Dispensed",
                DispensedDate = DateTime.Now
            };

            _logger.LogInformation("Prescription {PrescriptionId} dispensed successfully by pharmacist: {PharmacistId}", prescription.Id, request.PharmacistId);
            return Success(result);
        }
    }
}
