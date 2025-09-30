using Application.Bases;
using Application.Features.MOH.Queries.Models;
using Application.Features.MOH.Queries.Responses;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Features.MOH.Queries.Handlers
{
    public class MoHQueryHandler : ResponseHandler,
        IRequestHandler<GetMoHDashboardQuery, Response<MoHDashboardDto>>,
        IRequestHandler<GetMoHReportsQuery, Response<MoHReportDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MoHQueryHandler> _logger;

        public MoHQueryHandler(IUnitOfWork unitOfWork, ILogger<MoHQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<MoHDashboardDto>> Handle(GetMoHDashboardQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching MoH dashboard");

            var diseaseStats = await _unitOfWork.PrescriptionItemRepository
      .GetGroupedAsync(
          i => i.MedicineName,
          g => new DiseaseStatDto
          {
              Disease = g.Key,
              Cases = g.Count()
          });



            var vaccinationCoverage = await _unitOfWork.VaccinationRecords
                .GetAllWithCareria(v => v.AdministeredAt != null);

            var totalChildren = await _unitOfWork.Children.CountAsync();

            var dashboard = new MoHDashboardDto
            {
                DiseaseStats = diseaseStats,
                VaccinationCoverage = new VaccinationCoverageDto
                {
                    Children = totalChildren > 0
                        ? $"{(vaccinationCoverage.Count() * 100 / totalChildren)}%"
                        : "0%"
                }
            };

            _logger.LogInformation("Successfully fetched MoH dashboard");
            return Success(dashboard);
        }


        public async Task<Response<MoHReportDto>> Handle(GetMoHReportsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Generating MoH report for period: {FromDate} to {ToDate}", request.FromDate, request.ToDate);

            var newCases = await _unitOfWork.PatientRepository.GetAllWithCareria(p => p.user.CreatedDate >= request.FromDate && p.user.CreatedDate <= request.ToDate);

            var labTests = await _unitOfWork.LabResultRepository.GetAllWithCareria(l => l.UploadedAt >= request.FromDate && l.UploadedAt <= request.ToDate);

            //var labTests = await _unitOfWork.LabResults
            //    .GetAll()
            //    .AsNoTracking()
            //    .CountAsync(l => l.UploadedAt >= request.FromDate && l.UploadedAt <= request.ToDate, cancellationToken);
            var prescriptions = await _unitOfWork.PrescriptionRepository.GetAllWithCareria(p => p.IssuedAt >= request.FromDate && p.IssuedAt <= request.ToDate);


            var report = new MoHReportDto
            {
                Period = $"{request.FromDate:yyyy-MM-dd} to {request.ToDate:yyyy-MM-dd}",
                NewCases = (List<Patient>)newCases,
                LabTests = (List<LabResult>)labTests,
                Prescriptions = (List<Prescription>)prescriptions
            };

            _logger.LogInformation("Successfully generated MoH report for period: {Period}", report.Period);
            return Success(report);
        }
    }
}
