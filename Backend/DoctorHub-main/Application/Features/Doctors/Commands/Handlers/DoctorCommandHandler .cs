using Application.Bases;
using Application.Features.Doctors.Commands.Models;
using Application.Features.Doctors.Queries.Responses;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Commands.Handlers
{
    public class DoctorCommandHandler : ResponseHandler,
      IRequestHandler<CreatePrescriptionCommand, Response<PrescriptionCreatedDto>>,
      IRequestHandler<CreateLabOrderCommand, Response<LabOrderCreatedDto>>,
      IRequestHandler<UpdateLabOrderCommand, Response<LabOrderUpdatedDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DoctorCommandHandler> _logger;

        public DoctorCommandHandler(IUnitOfWork unitOfWork, ILogger<DoctorCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // ✅ Create Prescription
        public async Task<Response<PrescriptionCreatedDto>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating prescription for DoctorId={DoctorId}, PatientId={PatientId}", request.DoctorId, request.PatientId);

            // تحقق من وجود الدكتور والمريض
            var doctorExists = await _unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
            var patientExists = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);

            if (doctorExists==null || patientExists==null)
            {
                _logger.LogWarning("Doctor or patient not found (DoctorId={DoctorId}, PatientId={PatientId})", request.DoctorId, request.PatientId);
                return NotFound<PrescriptionCreatedDto>("Doctor or patient not found");
            }

            var prescription = new Prescription
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                IssuedAt = DateTime.Now,
                Notes = request.Notes,
                Items = request.Medications.Select(m => new PrescriptionItem
                {
                    MedicineName = m.Name,
                    Dosage = m.Dose,
                    Frequency = m.Duration
                
                }).ToList()
            };

            await _unitOfWork.PrescriptionRepository.AddAsync(prescription);
            await _unitOfWork.PrescriptionRepository.SaveChangesAsync();

            var response = new PrescriptionCreatedDto
            {
                PrescriptionId = prescription.Id,
                Status = "Created"
            };

            _logger.LogInformation("Prescription {Id} created successfully", prescription.Id);
            return Created(response);
        }

        // ✅ Create Lab Order
        public async Task<Response<LabOrderCreatedDto>> Handle(CreateLabOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating lab order for DoctorId={DoctorId}, PatientId={PatientId}", request.DoctorId, request.PatientId);

             // تحقق من وجود الدكتور والمريض
            var doctorExists = await _unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
            var patientExists = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);

            if (doctorExists == null || patientExists == null)
            {
                _logger.LogWarning("Doctor or patient not found (DoctorId={DoctorId}, PatientId={PatientId})", request.DoctorId, request.PatientId);
                return NotFound<LabOrderCreatedDto>("Doctor or patient not found");
            }

            var order = new LabOredr
            {
                
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                TestType = request.TestType,
                Description = request.Notes
            };

            await _unitOfWork.LabOrderRepository.AddAsync(order);
            await _unitOfWork.LabOrderRepository.SaveChangesAsync();

            var response = new LabOrderCreatedDto
            {
                OrderId = order.Id,
                Status = "Created"
            };

            _logger.LogInformation("Lab order {Id} created successfully", order.Id);
            return Created(response);
        }

        // ✅ Update Lab Order
        public async Task<Response<LabOrderUpdatedDto>> Handle(UpdateLabOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating lab order {OrderId}", request.OrderId);

            var order = await _unitOfWork.LabOrderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                _logger.LogWarning("Lab order not found: {OrderId}", request.OrderId);
                return NotFound<LabOrderUpdatedDto>("Lab order not found");
            }

            order.Description = request.Notes ?? order.Description;
            _unitOfWork.LabOrderRepository.Update(order);
            await _unitOfWork.LabOrderRepository.SaveChangesAsync();

            var response = new LabOrderUpdatedDto
            {
                OrderId = order.Id,
                Status = "Updated"
            };

            _logger.LogInformation("Lab order {Id} updated successfully", order.Id);
            return Success(response);
        }
    }
}
