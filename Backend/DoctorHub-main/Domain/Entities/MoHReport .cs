using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MoHReport : BaseEntity<int>
    {
        public DateTime ReportDate { get; set; } = DateTime.Now;

        // إحصائيات عامة
        public int TotalPatients { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalHospitals { get; set; }
        public int TotalLabs { get; set; }

        // بيانات تشغيلية
        public int ActiveAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public int PrescriptionsIssued { get; set; }
        public int LabTestsRequested { get; set; }
        public int LabTestsCompleted { get; set; }

        // بيانات التطعيمات
        public int VaccinationsScheduled { get; set; }
        public int VaccinationsCompleted { get; set; }

        // بيانات الطوارئ
        public int EmergencyCasesToday { get; set; }

        // تحليلات AI (إنذارات مبكرة)
        public string? EarlyWarning { get; set; } // مثلاً "زيادة في حالات الحمى 20% في القاهرة"

        // مين أصدر التنبيه
        public string CreatedByUserId { get; set; }
        public User CreatedBy { get; set; } = null!;
    }

}
