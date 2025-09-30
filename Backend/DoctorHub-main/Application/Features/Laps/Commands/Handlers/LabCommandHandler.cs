using Application.Bases;
using Application.Features.Laps.Commands.Models;
using Application.Features.Laps.Queries.Responses;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Laps.Commands.Handlers
{
    public class LabCommandHandler : ResponseHandler,
         IRequestHandler<UploadLabResultCommand, Response<LabResultCreatedDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<LabCommandHandler> _logger;

        public LabCommandHandler(IUnitOfWork unitOfWork, /*IFileStorageService fileStorageService*/ ILogger<LabCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            //_fileStorageService = fileStorageService;
            _logger = logger;
        }

        public async Task<Response<LabResultCreatedDto>> Handle(UploadLabResultCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Uploading lab result for order: {OrderId} by lab: {UploadedBy}", request.OrderId, request.UploadedBy);

            var labOrder = await _unitOfWork.LabOrderRepository.GetByIdAsync(request.OrderId);
            if (labOrder == null)
            {
                _logger.LogWarning("Lab order not found: {OrderId}", request.OrderId);
                return NotFound<LabResultCreatedDto>("Lab order not found");
            }

            var lab = await _unitOfWork.LabRepository.GetByIdAsync(request.UploadedBy);
            if (lab == null)
            {
                _logger.LogWarning("Lab not found: {UploadedBy}", request.UploadedBy);
                return NotFound<LabResultCreatedDto>("Lab not found");
            }

            //var fileUrl = await _fileStorageService.UploadFileAsync(request.File, cancellationToken);
            var fileUrl = "Drive";
            var labResult = new LabResult
            {
                LabID = request.UploadedBy,
                PatientId = labOrder.PatientId,
                LabOrderId = request.OrderId,
                LapName = request.TestType,
                DoctorName = labOrder.Doctor.Personal.FullName,
                Type = request.TestType,
                Speciality = labOrder.Doctor.Speciality,
                UploadedAt = DateTime.UtcNow,
                fileDocuments = new List<FileDocument>
                {
                    new FileDocument
                    {
                        FileName = request.File.FileName,
                        Url = fileUrl,
                        UploadedAt = DateTime.UtcNow
                    }
                }
            };

            await _unitOfWork.LabResultRepository.AddAsync(labResult);
            labOrder.Status = "Completed";
            await _unitOfWork.LabResultRepository.SaveChangesAsync();

            var result = new LabResultCreatedDto
            {
                ResultId = labResult.Id,
                Status = "Uploaded"
            };

            _logger.LogInformation("Lab result {ResultId} uploaded successfully for order: {OrderId}", labResult.Id, request.OrderId);
            return Created(result);
        }
    }
}
