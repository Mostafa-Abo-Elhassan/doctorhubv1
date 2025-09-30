import React from 'react';
import AppointmentCard from './shared/AppointmentCard';
import Empty from './shared/Empty';
import { HiPlus } from 'react-icons/hi';

// --- البيانات الوهمية الثابتة (كما هي) ---
const mockData = {
  appointments: [
    // --- المواعيد القادمة (3 مواعيد) ---
    { id: 'mock2', doctorId: 'doc2', date: '2025-10-15T14:00:00Z', status: 'Booked', title: 'زيارة متابعة' },
    { id: 'mock3', doctorId: 'doc3', date: '2025-10-20T10:30:00Z', status: 'Booked', title: 'استشارة عامة' },
    { id: 'mock5', doctorId: 'doc5', date: '2025-11-05T12:00:00Z', status: 'Booked', title: 'فحص أسنان' },

    // --- المواعيد السابقة (3 مواعيد) ---
    { id: 'mock1', doctorId: 'doc1', date: '2024-09-18T11:00:00Z', status: 'Completed', title: 'استشارة أولية' },
    { id: 'mock4', doctorId: 'doc4', date: '2024-09-01T09:00:00Z', status: 'Completed', title: 'فحص روتيني' },
    { id: 'mock6', doctorId: 'doc6', date: '2024-08-22T16:00:00Z', status: 'Completed', title: 'تحليل دم' },
  ],
  doctors: [
    { id: 'doc1', name: 'د. كريم منصور', hospital: 'مركز الحياة الطبي', imageUrl: 'https://images.unsplash.com/photo-1629425733761-caae3b5f2e50?w=100&h=100&fit=crop' },
    { id: 'doc2', name: 'د. أمينة خليل', hospital: 'عيادة الأمل', imageUrl: 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=100&h=100&fit=crop' },
    { id: 'doc3', name: 'د. عمر فاروق', hospital: 'مستشفى السلام', imageUrl: 'https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=100&h=100&fit=crop' },
    { id: 'doc4', name: 'د. نادية سالم', hospital: 'عيادة الشفاء', imageUrl: 'https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=100&h=100&fit=crop' },
    // الأطباء الجدد
    { id: 'doc5', name: 'د. سارة العوضي', hospital: 'عيادة الأسنان الحديثة', imageUrl: 'https://images.unsplash.com/photo-1629425733761-caae3b5f2e50?w=100&h=100&fit=crop' },
    { id: 'doc6', name: 'د. محمد الشناوي', hospital: 'مختبر البرج', imageUrl: 'https://images.unsplash.com/photo-1576091160550-2173dba999ef?w=100&h=100&fit=crop' },
  ]
};
// --- نهاية البيانات الوهمية ---

const AppointmentsTab = ({ appointments, doctors, loading } ) => {
  // استخدم البيانات الحقيقية إن وجدت، وإلا استخدم البيانات الوهمية
  const hasRealData = appointments && appointments.length > 0;
  const finalAppointments = hasRealData ? appointments : mockData.appointments;
  const finalDoctors = hasRealData ? doctors : mockData.doctors;

  // --- 1. تقسيم المواعيد إلى قائمتين ---
  const upcomingAppointments = finalAppointments.filter(app => app.status !== 'Completed');
  const pastAppointments = finalAppointments.filter(app => app.status === 'Completed');

  return (
    <div className="p-4 sm:p-6">
      {/* رأس الصفحة: زر الحجز فقط */}
      <div className="flex justify-start mb-8">
        <button className="flex items-center justify-center gap-2 bg-green-500 text-white px-5 py-2.5 rounded-lg font-semibold hover:bg-green-600 transition-colors shadow">
          <HiPlus className="w-5 h-5" />
          <span>حجز موعد جديد</span>
        </button>
      </div>

      {/* --- 2. قسم المواعيد القادمة --- */}
      <div className="mb-12">
        <h2 className="text-2xl font-bold text-gray-800 mb-6 border-r-4 border-blue-500 pr-4">
          المواعيد القادمة
        </h2>
        {upcomingAppointments.length > 0 ? (
          <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6">
            {upcomingAppointments.map(appointment => (
              <AppointmentCard 
                key={appointment.id} 
                appointment={appointment} 
                doctor={finalDoctors.find(doc => doc.id === appointment.doctorId)} 
              />
            ))}
          </div>
        ) : (
          <Empty text="لا توجد مواعيد قادمة." />
        )}
      </div>

      {/* --- 3. الخط الفاصل --- */}
      <hr className="my-8 border-t-2 border-gray-200" />

      {/* --- 4. قسم المواعيد السابقة --- */}
      <div>
        <h2 className="text-2xl font-bold text-gray-800 mb-6 border-r-4 border-gray-400 pr-4">
          المواعيد السابقة
        </h2>
        {pastAppointments.length > 0 ? (
          <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6">
            {pastAppointments.map(appointment => (
              <AppointmentCard 
                key={appointment.id} 
                appointment={appointment} 
                doctor={finalDoctors.find(doc => doc.id === appointment.doctorId)} 
              />
            ))}
          </div>
        ) : (
          <Empty text="لا توجد مواعيد سابقة في سجلك." />
        )}
      </div>
    </div>
  );
};

export default AppointmentsTab;
