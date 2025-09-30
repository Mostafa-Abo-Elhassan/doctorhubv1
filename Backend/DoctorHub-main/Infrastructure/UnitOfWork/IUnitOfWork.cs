using Domain.Entities;
using Infrastructure.GenericRepositoryPattern;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositoryAsync<User> UserRepository { get; }
        IGenericRepositoryAsync<Patient> PatientRepository { get; }
        IGenericRepositoryAsync<Doctor> DoctorRepository { get; }
        IGenericRepositoryAsync<Appointment> AppointmentRepository { get; }
        IGenericRepositoryAsync<Prescription> PrescriptionRepository { get; }
        IGenericRepositoryAsync<Lab> LabRepository { get; }
         IGenericRepositoryAsync<LabOredr> LabOrderRepository { get; }
         IGenericRepositoryAsync<LabResult> LabResultRepository { get; }

        IGenericRepositoryAsync<MedicationInteraction> MedicationInteractionRepository { get; }
        IGenericRepositoryAsync<ConsentRecord> ConsentRecordRepository { get; }
        IGenericRepositoryAsync<Pharmacy> PharmacyRepository { get; }
        IGenericRepositoryAsync<AuditLog> AuditLogs { get; }
        IGenericRepositoryAsync<Child> Children { get; }
        IGenericRepositoryAsync<VaccinationRecord> VaccinationRecords { get; }
        IGenericRepositoryAsync<PrescriptionItem> PrescriptionItemRepository { get; }
        //IGenericRepositoryAsync<Review> ReviewRepository { get; }
        //IGenericRepositoryAsync<Payment> PaymentRepository { get; }

        Task<int> SaveAsync();
    }

}
