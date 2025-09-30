import React from 'react';
import { HiOutlineCalendar, HiOutlineClock } from 'react-icons/hi';

// دالة لترجمة الحالة من الإنجليزية للعربية
const translateStatus = (status) => {
  if (status === 'Booked' || status === 'مؤكد') return 'مؤكد';
  if (status === 'Completed' || status === 'مكتمل') return 'مكتمل';
  return status;
};

const AppointmentCard = ({ appointment, doctor }) => {
  // 1. تجهيز البيانات
  const statusText = translateStatus(appointment.status);
  const isCompleted = statusText === 'مكتمل';

  const appointmentDate = new Date(appointment.date);
  const formattedDate = appointmentDate.toLocaleDateString('ar-EG', { day: 'numeric', month: 'long', year: 'numeric' });
  const formattedTime = appointmentDate.toLocaleTimeString('ar-EG', { hour: '2-digit', minute: '2-digit', hour12: true });

  const doctorName = doctor ? doctor.name : 'طبيب غير محدد';
  const clinicInfo = doctor ? (doctor.hospital || doctor.specialty) : 'عيادة غير محددة';
  const appointmentTitle = appointment.title || "موعد طبي"; // استخدم العنوان من البيانات الوهمية أو عنوان افتراضي
  const imageUrl = doctor?.imageUrl || `https://i.pravatar.cc/150?u=${appointment.id}`; // صورة فريدة لكل بطاقة

  // 2. تحديد الألوان والكلاسات بناءً على الحالة
  const statusClasses = isCompleted 
    ? 'bg-gray-100 text-gray-500' 
    : 'bg-green-100 text-green-700';
  
  const buttonClasses = `w-full text-center py-3 font-semibold rounded-b-xl transition-colors ${
    isCompleted 
      ? 'bg-gray-100 text-gray-400' 
      : 'bg-blue-500 text-white hover:bg-blue-600'
  }`;

  return (
    <div className="bg-white rounded-xl shadow-md flex flex-col justify-between overflow-hidden transition-transform hover:transform hover:-translate-y-1">
      {/* المحتوى العلوي للبطاقة */}
      <div className="p-5">
        <div className="flex justify-between items-start">
          {/* المعلومات النصية */}
          <div className="flex-1 pr-4">
            <p className={`inline-block px-2 py-0.5 text-xs font-semibold rounded-full ${statusClasses}`}>
              {statusText}
            </p>
            <h3 className="mt-2 text-lg font-bold text-gray-800">{appointmentTitle}</h3>
            <p className="text-sm text-gray-500">{doctorName} | {clinicInfo}</p>
          </div>
          {/* الصورة */}
          <div className="flex-shrink-0">
            <img 
              className="h-14 w-14 object-cover rounded-md" 
              src={imageUrl}
              alt={doctorName} 
            />
          </div>
        </div>
        
        {/* الخط الفاصل */}
        <hr className="my-3" />

        {/* التاريخ والوقت */}
        <div className="flex justify-between items-center text-xs text-gray-500">
          <div className="flex items-center">
            <HiOutlineCalendar className="h-4 w-4 ml-1.5 text-gray-400" />
            <span>{formattedDate}</span>
          </div>
          <div className="flex items-center">
            <HiOutlineClock className="h-4 w-4 ml-1.5 text-gray-400" />
            <span>{formattedTime}</span>
          </div>
        </div>
      </div>

      {/* الزر السفلي */}
      <button className={buttonClasses} disabled={isCompleted}>
        {isCompleted ? 'عرض التفاصيل' : 'عرض التفاصيل'}
      </button>
    </div>
   );
};

export default AppointmentCard;
