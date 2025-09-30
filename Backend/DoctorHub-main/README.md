# 🏥 DoctorHub – المنصّة الموحدة للسجل الصحي (Unified Health Record Platform)

# 🩺 المشكلة (Problem)

- في مصر، بيانات المريض الطبية متفرقة بين مستشفيات وعيادات مختلفة.  
- صعوبة الوصول للتاريخ الصحي السابق عند تغيير المستشفى أو الدكتور.  
- ضياع وقت المريض وإعادة نفس التحاليل والأشعة.  

---

# 💡 الحل (Solution)

- **DoctorHub** يوفر سجل صحي موحد لكل مواطن مصري.  
- الوصول للسجل يتم عبر **الرقم القومي**.  
- الأطباء والمستشفيات يقدروا يشوفوا التاريخ الطبي كامل.  
- المريض يقدر يحجز مواعيد ويتابع تقاريره في مكان واحد.  

## 📌 عن المشروع (About)
**DoctorHub** هو نظام صحي رقمي (Digital Health Platform) بيوفر **سجل صحي موحد (Unified Health Record)** لكل مواطن باستخدام الرقم القومي.  
المنصة بتسهل على:
- 👤 المريض (Patient) يتابع تاريخه الطبي (تحاليل – أدوية – مواعيد).  
- 🩺 الدكتور (Doctor) يفتح ملف المريض ويكتب روشتة (Prescription) أو يطلب تحاليل (Lab Orders).  
- 💊 الصيدلي (Pharmacist) يتأكد من صرف الدواء (Dispense).  
- 🧪 المعمل/الأشعة (Lab / Radiology) يرفع نتائج التحاليل (Lab Results).  
- 🏥 المستشفى (Hospital) تدخل ملف الطوارئ للمريض.  
- 🏛️ وزارة الصحة (MoH) تتابع إحصائيات الأمراض والمؤشرات الصحية.  

---

## 🚀 المميزات الأساسية (Core Features – MVP)
- **تسجيل دخول بالرقم القومي (National ID Login).**  
- **السجل الطبي الموحد (Unified Health Record).**  
- **الحجز الإلكتروني للمواعيد (Online Appointments).**  
- **إدارة الوصفات الطبية (Prescriptions).**  
- **متابعة نتائج التحاليل والأشعة (Lab/Radiology Results).**  
- **لوحات تحكم مخصصة لكل Role (Doctor – Patient – Pharmacist – Lab – Hospital – MoH).**

---
# 📖 دليل الاستخدام (User Guide)

يوضح هذا الدليل كيفية استخدام منصة **DoctorHub** خطوة بخطوة للمريض، الطبيب، الصيدلي، المعمل، المستشفى، ووزارة الصحة.

---

## 👤 المريض (Patient)

1. **تسجيل الدخول**
   - يدخل المريض باستخدام الرقم القومي + كلمة المرور.
   - بعد تسجيل الدخول يظهر له **Patient Dashboard**.

2. **عرض السجل الطبي**
   - من القائمة يقدر يفتح **Medical Records**.
   - يشوف:
     - آخر روشتة.
     - آخر نتيجة معمل/أشعة.
     - الأمراض المزمنة.

3. **حجز موعد (Appointment)**
   - يفتح صفحة **Appointments**.
   - يختار الدكتور + التاريخ + نوع الكشف (حضوري / أونلاين).
   - يضغط "حجز" → يظهر له إشعار أن الموعد تم تأكيده.

---

## 🩺 الطبيب (Doctor)

1. **تسجيل الدخول**
   - يدخل الطبيب باستخدام كود الطبيب (DoctorID) + كلمة المرور.

2. **Dashboard**
   - يشوف مواعيد اليوم.
   - يشوف تنبيهات AI (زي: تداخل دوائي).

3. **فتح ملف المريض**
   - يكتب الرقم القومي للمريض.
   - يفتح **Patient File**: تشخيصات، أدوية، تحاليل.

4. **كتابة روشتة إلكترونية (E-Prescription)**
   - يدخل اسم الدواء + الجرعة + المدة.
   - يضغط "إرسال" → الروشتة تروح مباشرة للصيدلي.

5. **طلب معمل/أشعة (Lab Order)**
   - يختار نوع التحليل (مثال: CBC).
   - يضيف ملاحظات (مثال: صايم 8 ساعات).
   - يضغط "إرسال" → الطلب يروح للمعمل.

---

## 💊 الصيدلي (Pharmacist)

1. **تسجيل الدخول**
   - يدخل باستخدام PharmacistID + كلمة المرور.

2. **عرض الروشتات**
   - يفتح **Pharmacy Dashboard**.
   - يشوف قائمة بالروشتات اللي لسه ما اتصرفت.

3. **صرف الدواء**
   - يضغط على روشتة معينة.
   - يراجع تفاصيلها.
   - يضغط "تم الصرف" → النظام يحدث الحالة إلى Dispensed.

---

## 🧪 المعمل / الأشعة (Lab / Radiology)

1. **تسجيل الدخول**
   - يدخل باستخدام LabID + كلمة المرور.

2. **عرض الطلبات**
   - يفتح **Lab Dashboard**.
   - يشوف قائمة التحاليل المطلوبة.

3. **رفع النتيجة**
   - يختار طلب محدد.
   - يرفع ملف PDF أو صورة DICOM.
   - يضيف ملاحظات (مثال: القيم طبيعية).
   - يضغط "رفع" → النتيجة تظهر للمريض والطبيب.

---

## 🏥 المستشفى (Hospital)

1. **تسجيل الدخول**
   - يدخل مسؤول المستشفى باستخدام HospitalID + كلمة المرور.

2. **Emergency Access**
   - في حالة الطوارئ يكتب الرقم القومي.
   - يظهر ملخص سريع عن:
     - الأمراض المزمنة.
     - الأدوية الحالية.
     - الحساسية.

3. **Hospital Dashboard**
   - يشوف كل المرضى الموجودين حاليًا بالمستشفى وحالتهم.

---

## 🏛️ وزارة الصحة (MoH)

1. **تسجيل الدخول**
   - يدخل مسؤول الوزارة باستخدام MoHID + كلمة المرور.

2. **Health Dashboard**
   - يشوف إحصائيات الأمراض.
   - تغطية التطعيمات.

3. **Reports & Analytics**
   - يحدد الفترة الزمنية (من – إلى).
   - يظهر تقرير فيه:
     - عدد الحالات الجديدة.
     - عدد التحاليل.
     - عدد الروشتات.

## 🏗️ البنية المعمارية (System Architecture)
- **Frontend (واجهة المستخدم):** ASP.NET MVC + Bootstrap.  
- **Backend (الخدمات و الـ APIs):** ASP.NET Core Web API (.NET 9).  
- **Database (قاعدة البيانات):** SQL Server (Cloud SQL).  
- **Authentication (المصادقة):** ASP.NET Identity.  
- **Deployment (النشر):** Google Cloud Run / AWS Elastic Beanstalk.  


## 📂 Database Design (ERD Snapshot)
الجداول الأساسية:
- **Patients** (المرضى).  
- **Doctors** (الأطباء).  
- **Appointments** (المواعيد).  
- **Prescriptions** (الوصفات الطبية).  
- **LabOrders & LabResults** (طلبات وفحوصات معملية).  
- **Users & Roles** (المستخدمين والصلاحيات).  

---

## 📑 Endpoints (MVP APIs)

### 👤 Patient (المريض)
- `GET /patients/{id}` → عرض ملفه الطبي.  
- `POST /appointments` → حجز ميعاد.  
- `GET /appointments?patientId={id}` → متابعة المواعيد.  
- `GET /prescriptions?patientId={id}` → عرض الروشتات.  
- `GET /labResults?patientId={id}` → عرض نتائج التحاليل.  

### 🩺 Doctor (الطبيب)
- `GET /patients/{id}` → فتح ملف المريض.  
- `POST /prescriptions` → كتابة روشتة.  
- `POST /labOrders` → طلب تحليل/أشعة.  
- `GET /appointments?doctorId={id}` → مشاهدة جدول المواعيد.  

### 💊 Pharmacist (الصيدلي)
- `GET /prescriptions/{id}` → استعراض الروشتة.  
- `PUT /prescriptions/{id}/dispense` → تسجيل صرف الدواء.  

### 🧪 Lab/Radiology (المعمل/الأشعة)
- `GET /labOrders?labId={id}` → استعراض الطلبات.  
- `POST /labResults` → رفع نتيجة التحليل.  

### 🏥 Hospital (المستشفى)
- `GET /patients/{nationalId}` → فتح ملف الطوارئ.  

### 🏛️ Ministry of Health (MoH)
- `GET /analytics/diseases` → مؤشرات وإحصائيات الأمراض.  

---

## 📊 جدول ملخص (Roles + Pages + Endpoints)

| Role            | Pages / Features         | Endpoints مختصرة |
|-----------------|--------------------------|------------------|
| 👤 Patient      | Dashboard – Appointments – Records | GET/POST /patients – /appointments – /prescriptions – /labResults |
| 🩺 Doctor       | Dashboard – Patient File – Prescriptions – Lab Orders | GET/POST /patients – /prescriptions – /labOrders – /appointments |
| 💊 Pharmacist   | Dashboard – Prescription View | GET /prescriptions – PUT /dispense |
| 🧪 Lab/Radiology| Dashboard – Upload Results | GET /labOrders – POST /labResults |
| 🏥 Hospital     | Emergency File – Patients Dashboard | GET /patients/{nationalId} |
| 🏛️ MoH         | Health Dashboard – Reports | GET /analytics/diseases |

---

## 🛠️ خطوات التشغيل (Installation & Run)
1. Clone الـ Repo:
   ```bash
   git clone https://github.com/Mostafa-Abo-Elhassan/DoctorHub.git
   cd DoctorHub
# 📄 Documentation

ملف الـ Documentation PDF موجود داخل:  
`Docs/DoctorHub.pdf`

ويحتوي على:

- Problem & Solution.  
- System Architecture Diagram.  
- Database ERD.  
- User Guide.  
- Business Model Canvas.  
- Future Work.  

---

## 🎥 Demo Video

📺 **Demo Video (15 دقيقة)** يشمل:

- تقديم الفكرة والمشكلة.  
- شرح واجهة الاستخدام.  
- رحلة المستخدم (User Journey).  
- نظرة على الكود والبنية.  
- خطة العمل المستقبلية.  

---

## 💰 Business Model

- **العملاء المستهدفين (Customer Segments):** وزارة الصحة – المستشفيات – المرضى.  
- **القيمة المقدمة (Value Proposition):** سجل صحي موحّد لكل مواطن + تقليل الازدواجية الطبية.  
- **القنوات (Channels):** مستشفيات – تطبيق ويب – تطبيق موبايل.  

**مصادر الدخل (Revenue Streams):**

- اشتراكات المستشفيات/الوزارة.  
- دعم حكومي.  
- Data Insights وإحصائيات.  

---

## 🔮 Future Work

- Mobile App للمرضى والأطباء.  
- AI Assistant لتشخيص مبدئي.  
- Integration مع Wearables (أجهزة قياس الضغط/السكر).  
- Patient Notifications بالـ SMS/WhatsApp.  
- Telemedicine (كشف بالفيديو).  

---

## 👥 Team Members

- **Mostafa Mahmoud Abo Elhassan** – Backend Lead & Team Leader.  
- **[ابوبكر]** – Frontend & UI/UX.  
- **[احمد ومصطفي]** – Database & Testing.  
- **[احمد ومصطفي]** – Business & Project Manager.  

---

## 📌 License

MIT License – Free to use & modify.  

---

## 🖼️ Diagrams (Placeholders)

- `Docs/architecture-diagram.png`  
- `Docs/database-erd.png`
