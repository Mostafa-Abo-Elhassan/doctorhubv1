using Application.Bases;
using Application.Features.Patients.Commands.Models;
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

namespace Application.Features.Patients.Commands.Handlers
{
    public class PatientCommandHandler : ResponseHandler,
         IRequestHandler<BookAppointmentCommand, Response<AppointmentCreatedDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientCommandHandler> _logger;

        public PatientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PatientCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<AppointmentCreatedDto>> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Booking appointment for patient: {PatientId} with doctor: {DoctorId}", request.PatientId, request.DoctorId);

            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
            {
                _logger.LogWarning("Patient not found: {PatientId}", request.PatientId);
                return NotFound<AppointmentCreatedDto>("Patient not found");
            }

            var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor not found: {DoctorId}", request.DoctorId);
                return NotFound<AppointmentCreatedDto>("Doctor not found");
            }

            var conflictingAppointment = await _unitOfWork.AppointmentRepository.GetAllWithCareria
                (a => a.DoctorId == request.DoctorId && a.ScheduledAt == request.Date);

            if (conflictingAppointment!=null)
            {
                _logger.LogWarning("Doctor {DoctorId} is not available at {Date}", request.DoctorId, request.Date);
                return BadRequest<AppointmentCreatedDto>("Doctor is not available at this time");
            }

            var appointment = new Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                ScheduledAt = request.Date,
                Mode = request.Type,
                Status = "Scheduled"
            };

            await _unitOfWork.AppointmentRepository.AddAsync(appointment);
            await _unitOfWork.AppointmentRepository.SaveChangesAsync();

            var result = new AppointmentCreatedDto
            {
                AppointmentId = appointment.Id,
                Status = appointment.Status
            };

            _logger.LogInformation("Appointment {AppointmentId} booked successfully for patient: {PatientId}", appointment.Id, request.PatientId);
            return Created(result);
        }
    }
}
