import React from 'react';
import Empty from '../../Pages/Patient/shared/Empty';
import { FaUser, FaCalendarCheck, FaClock } from 'react-icons/fa';

// --- البيانات الوهمية ---
// هذه البيانات ستستخدم فقط إذا لم يجد التطبيق بيانات حقيقية قادمة من الخادم
const mockAppointments = [
  { id: 'mock-a1', patientId: '1', date: '2025-10-15T14:00:00Z', status: 'Booked', title: 'زيارة متابعة' },
  { id: 'mock-a2', patientId: '2', date: '2025-10-20T10:30:00Z', status: 'Booked', title: 'استشارة قلب' },
  { id: 'mock-a3', patientId: '3', date: '2024-09-18T11:00:00Z', status: 'Completed', title: 'استشارة أولية' },
];
const mockPatients = [
  { id: '1', name: 'أحمد علي' },
  { id: '2', name: 'منى حسن' },
  { id: '3', name: 'أحمد عبد الجواد' },
];  
// --- نهاية البيانات الوهمية ---

// مكون بطاقة الموعد: مسؤول فقط عن عرض البيانات التي يتلقاها
const AppointmentCard = ({ appointment, patient }) => {
  const isCompleted = appointment.status === 'Completed';
  const appointmentDate = new Date(appointment.date);
  const formattedDate = appointmentDate.toLocaleDateString('ar-EG', { day: 'numeric', month: 'long', year: 'numeric' });
  const formattedTime = appointmentDate.toLocaleTimeString('ar-EG', { hour: '2-digit', minute: '2-digit' });

  return (
    <div className={`bg-white rounded-xl shadow-md p-4 border-l-4 ${isCompleted ? 'border-gray-300' : 'border-blue-500'}`}>
      <div className="flex justify-between items-center">
        <div>
          <p className={`font-bold text-lg ${isCompleted ? 'text-gray-500' : 'text-gray-800'}`}>{appointment.title || 'موعد'}</p>
          <div className="flex items-center text-sm text-gray-600 mt-2">
            <FaUser className="ml-2 text-gray-400" />
            <span>{patient ? patient.name : 'مريض غير محدد'}</span>
          </div>
        </div>
        <div className="text-left">
          <div className="flex items-center justify-end text-sm text-gray-600">
            <FaCalendarCheck className="ml-2 text-gray-400" />
            <span>{formattedDate}</span>
          </div>
          <div className="flex items-center justify-end text-sm text-gray-600 mt-1">
            <FaClock className="ml-2 text-gray-400" />
            <span>{formattedTime}</span>
          </div>
        </div>
      </div>
    </div>
  );
};

// المكون الرئيسي للتبويب: مسؤول عن المنطق وجلب البيانات
const DoctorAppointmentsTab = ({ appointments, patients, loading }) => {
  // المنطق الصحيح: استخدم البيانات الوهمية فقط إذا انتهى التحميل والبيانات الحقيقية فارغة
  const useMockData = !loading && (!appointments || appointments.length === 0);
  const finalAppointments = useMockData ? mockAppointments : appointments;
  const finalPatients = useMockData ? mockPatients : patients;

  // تقسيم المواعيد إلى قادمة وسابقة
  const upcomingAppointments = (finalAppointments || []).filter(a => a.status !== 'Completed');
  const pastAppointments = (finalAppointments || []).filter(a => a.status === 'Completed');

  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold text-gray-800 mb-8">📅 مواعيدك</h2>
      
      {/* قسم المواعيد القادمة */}
      <div className="mb-10">
        <h3 className="text-xl font-semibold text-gray-700 mb-4 border-r-4 border-blue-500 pr-3">القادمة</h3>
        {loading ? (
          <div className="h-24 bg-gray-200 rounded-xl animate-pulse"></div>
        ) : upcomingAppointments.length > 0 ? (
          <div className="space-y-4">
            {upcomingAppointments.map(app => (
              <AppointmentCard 
                key={app.id} 
                appointment={app} 
                patient={(finalPatients || []).find(p => p.id === app.patientId)} 
              />
            ))}
          </div>
        ) : (
          <Empty text="لا توجد مواعيد قادمة." />
        )}
      </div>

      {/* قسم المواعيد السابقة */}
      <div>
        <h3 className="text-xl font-semibold text-gray-700 mb-4 border-r-4 border-gray-400 pr-3">السابقة</h3>
        {loading ? (
          <div className="h-24 bg-gray-200 rounded-xl animate-pulse"></div>
        ) : pastAppointments.length > 0 ? (
          <div className="space-y-4">
            {pastAppointments.map(app => (
              <AppointmentCard 
                key={app.id} 
                appointment={app} 
                patient={(finalPatients || []).find(p => p.id === app.patientId)} 
              />
            ))}
          </div>
        ) : (
          <Empty text="لا توجد مواعيد سابقة." />
        )}
      </div>
    </div>
  );
};

export default DoctorAppointmentsTab;
