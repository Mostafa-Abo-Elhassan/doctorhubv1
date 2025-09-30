import React from 'react';
import { FaTimes, FaIdCard, FaEnvelope, FaPhone } from 'react-icons/fa';

const PatientDetailModal = ({ patient, onClose }) => {
  // إذا لم يكن هناك مريض، لا تعرض شيئاً
  if (!patient) return null;

  const avatarUrl = `https://ui-avatars.com/api/?name=${patient.name.replace(' ', '+' )}&background=0284C7&color=fff&size=128`;

  return (
    // الخلفية الشفافة
    <div 
      className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50"
      onClick={onClose} // إغلاق النافذة عند الضغط على الخلفية
    >
      {/* جسم النافذة */}
      <div 
        className="bg-white rounded-2xl shadow-xl p-6 w-full max-w-md m-4 relative transform transition-all"
        onClick={(e) => e.stopPropagation()} // منع إغلاق النافذة عند الضغط عليها
      >
        {/* زر الإغلاق */}
        <button 
          onClick={onClose}
          className="absolute top-4 left-4 text-gray-400 hover:text-gray-600"
        >
          <FaTimes size={20} />
        </button>

        {/* محتوى النافذة */}
        <div className="text-center">
          <img src={avatarUrl} alt={patient.name} className="w-28 h-28 rounded-full mx-auto mb-4 border-4 border-gray-100" />
          <h2 className="text-2xl font-bold text-gray-800">{patient.name}</h2>
          <p className="text-gray-500">{patient.gender === 'ذكر' ? 'Male' : 'Female'}, {patient.age || 'N/A'} years old</p>
        </div>

        <div className="mt-6 border-t pt-4 space-y-3 text-right">
          <div className="flex items-center justify-end">
            <span className="text-gray-700">{patient.nationalId}</span>
            <FaIdCard className="text-gray-400 ml-3" />
          </div>
          <div className="flex items-center justify-end">
            <span className="text-gray-700">{patient.email || 'لا يوجد بريد إلكتروني'}</span>
            <FaEnvelope className="text-gray-400 ml-3" />
          </div>
          <div className="flex items-center justify-end">
            <span className="text-gray-700">{patient.phone || 'لا يوجد هاتف'}</span>
            <FaPhone className="text-gray-400 ml-3" />
          </div>
        </div>
        
        <div className="mt-6 text-center">
            <button className="bg-teal-500 text-white px-6 py-2 rounded-lg hover:bg-teal-600">
                فتح الملف الطبي الكامل
            </button>
        </div>
      </div>
    </div>
  );
};

export default PatientDetailModal;
