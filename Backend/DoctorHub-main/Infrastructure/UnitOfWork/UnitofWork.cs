using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.GenericRepositoryPattern;
using System.Security.AccessControl;

namespace Infrastructure.UnitOfWork
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepositoryAsync<User> UserRepository { get; }
        public IGenericRepositoryAsync<Patient> PatientRepository { get; }
        public IGenericRepositoryAsync<Doctor> DoctorRepository { get; }
        public IGenericRepositoryAsync<Appointment> AppointmentRepository { get; }
        public IGenericRepositoryAsync<Prescription> PrescriptionRepository { get; }
        public IGenericRepositoryAsync<Lab> LabRepository { get; }
        public IGenericRepositoryAsync<LabOredr> LabOrderRepository { get; }
        public IGenericRepositoryAsync<LabResult> LabResultRepository { get; }
        public IGenericRepositoryAsync<MedicationInteraction> MedicationInteractionRepository { get; }
        public IGenericRepositoryAsync<ConsentRecord> ConsentRecordRepository { get; }
        public IGenericRepositoryAsync<Pharmacy> PharmacyRepository { get; }
        public IGenericRepositoryAsync<AuditLog> AuditLogs { get; }
        public IGenericRepositoryAsync<Child> Children { get; }
        public IGenericRepositoryAsync<VaccinationRecord> VaccinationRecords { get; }
        public IGenericRepositoryAsync<PrescriptionItem> PrescriptionItemRepository { get; }


        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            UserRepository = new GenericRepositoryAsync<User>(_context);
            PatientRepository = new GenericRepositoryAsync<Patient>(_context);
            DoctorRepository = new GenericRepositoryAsync<Doctor>(_context);
            AppointmentRepository = new GenericRepositoryAsync<Appointment>(_context);
            PrescriptionRepository = new GenericRepositoryAsync<Prescription>(_context);
            LabOrderRepository = new GenericRepositoryAsync<LabOredr>(_context);
            LabResultRepository = new GenericRepositoryAsync<LabResult>(_context);
            LabRepository = new GenericRepositoryAsync<Lab>(_context);
            MedicationInteractionRepository = new GenericRepositoryAsync<MedicationInteraction>(_context);
            ConsentRecordRepository = new GenericRepositoryAsync<ConsentRecord>(_context);
            PharmacyRepository = new GenericRepositoryAsync<Pharmacy>(_context);
            AuditLogs = new GenericRepositoryAsync<AuditLog>(_context);
            Children = new GenericRepositoryAsync<Child>(_context);
            VaccinationRecords = new GenericRepositoryAsync<VaccinationRecord>(_context);
            PrescriptionItemRepository = new GenericRepositoryAsync<PrescriptionItem>(_context);

        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
