using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MedicationInteraction : BaseEntity<int>
    {
        public int MedicationAId { get; set; }
        public PrescriptionItem MedicationA { get; set; } = default!;

        public int MedicationBId { get; set; }
        public PrescriptionItem MedicationB { get; set; } = default!;

        public string Severity { get; set; } = "Mild";
        public string Description { get; set; } = default!;
    }

}

//"Mild" → تفاعل بسيط(ممكن يعمل أعراض خفيفة أو مش مؤثرة قوي).

//"Moderate" → تفاعل متوسط(ممكن يأثر بشكل ملحوظ و محتاج متابعة أو تقليل الجرعة).

//"Severe" → تفاعل خطير(ممكن يسبب مشاكل صحية قوية أو خطر على حياة المريض).