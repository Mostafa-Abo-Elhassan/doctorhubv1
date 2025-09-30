using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // 🗂️ DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<LabResult> LabResults { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<LabOredr> LabOrders { get; set; }
        public DbSet<FileDocument> FileDocuments { get; set; }
        public DbSet<MoHReport> MoHReports { get; set; }
        public DbSet<MoHAlert> MoHAlerts { get; set; }

        // ✨ Entities اللي نسيناها
        public DbSet<Child> Children { get; set; }
        public DbSet<ConsentRecord> ConsentRecords { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<MedicationInteraction> MedicationInteractions { get; set; }
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // تطبيق كل Configurations تلقائياً
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);


            // Roles IDs (ثابتة)
            var Rider = "9E466E21-3B55-4F4D-8D99-123456789001";
            var Driver = "9E466E21-3B55-4F4D-8D99-123456789002";
            var Company = "9E466E21-3B55-4F4D-8D99-123456789003";
            var Admin = "9E466E21-3B55-4F4D-8D99-123456789999";
            var SuperAdmin = "9E466E21-3B55-4F4D-8D99-123456789998";
            var MoHAdmin = "9E466E21-3B55-4F4D-8D99-123456789997";

            var Patient = "9E466E21-3B55-4F4D-8D99-123456789004";
            var Doctor = "9E466E21-3B55-4F4D-8D99-123456789005";
            var Pharmacy = "9E466E21-3B55-4F4D-8D99-123456789006";
            var Lab = "9E466E21-3B55-4F4D-8D99-123456789007";
            var Hospital = "9E466E21-3B55-4F4D-8D99-123456789008";
            var HealthCenter = "9E466E21-3B55-4F4D-8D99-123456789009";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Rider, Name = "Rider", NormalizedName = "RIDER" },
                new IdentityRole { Id = Driver, Name = "Driver", NormalizedName = "DRIVER" },
                new IdentityRole { Id = Company, Name = "Company", NormalizedName = "COMPANY" },
                new IdentityRole { Id = Admin, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = SuperAdmin, Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                new IdentityRole { Id = MoHAdmin, Name = "MoHAdmin", NormalizedName = "MOHADMIN" },

                // ✅ أدوار النظام الطبي
                new IdentityRole { Id = Patient, Name = "Patient", NormalizedName = "PATIENT" },
                new IdentityRole { Id = Doctor, Name = "Doctor", NormalizedName = "DOCTOR" },
                new IdentityRole { Id = Pharmacy, Name = "Pharmacy", NormalizedName = "PHARMACY" },
                new IdentityRole { Id = Lab, Name = "Lab", NormalizedName = "LAB" },
                new IdentityRole { Id = Hospital, Name = "Hospital", NormalizedName = "HOSPITAL" },
                new IdentityRole { Id = HealthCenter, Name = "HealthCenter", NormalizedName = "HEALTHCENTER" }
                    );

        }
    }
}
