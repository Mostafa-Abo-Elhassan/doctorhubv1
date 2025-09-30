using Application.Bases;
using Application.Features.Laps.Queries.Models;
using Application.Features.Laps.Queries.Responses;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Laps.Queries.Handlers
{
    public class LabQueryHandler : ResponseHandler,
         IRequestHandler<GetLabOrdersQuery, Response<List<LabOrderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LabQueryHandler> _logger;

        public LabQueryHandler(IUnitOfWork unitOfWork, ILogger<LabQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<List<LabOrderDto>>> Handle(GetLabOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching lab orders with status: {Status}", request.Status);

            var labOrders = await _unitOfWork.LabOrderRepository.GetAllWithCareria(o => o.Status == request.Status, new[] { "Doctor", "Patient" } );
                //.GetAll()
                //.AsNoTracking()
                //.Where(o => o.Status == request.Status)
                //.Include(o => o.Patient)
                //.Include(o => o.Doctor)
                //.ToListAsync(cancellationToken);

            var labOrderDtos = labOrders.Select(o => new LabOrderDto
            {
                Id = o.Id,
                PatientName = o.Patient.Personal.FullName,
                TestType = o.TestType,
                DoctorName = o.Doctor.Personal.FullName
            }).ToList();

            _logger.LogInformation("Successfully fetched {Count} lab orders", labOrderDtos.Count);
            return Success(labOrderDtos);
        }
    }
}
