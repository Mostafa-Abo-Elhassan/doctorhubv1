using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MoHAlert : BaseEntity<int>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // نوع التنبيه (وباء، تطعيم، دواء ناقص، حالة طوارئ)
        public string AlertType { get; set; } = "Outbreak";

        // وصف قصير للتنبيه
        public string Title { get; set; } = default!;

        // تفاصيل أكتر
        public string Description { get; set; } = default!;

        // المنطقة الجغرافية (محافظة/مدينة)
        public string? Region { get; set; }

        // مستوى الخطورة
        public string Severity { get; set; } = "Medium"; // Low, Medium, High, Critical

        // حالة التنبيه (نشط / متابع / مغلق)
        public string Status { get; set; } = "Active"; // Active, Monitoring, Resolved

        // مين أصدر التنبيه
        public string CreatedByUserId { get; set; }
        public User CreatedBy { get; set; } = null!;
    }

}
