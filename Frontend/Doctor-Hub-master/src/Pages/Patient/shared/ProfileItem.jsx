import React from 'react';
import { FaFileMedicalAlt, FaFlask, FaSyringe, FaDownload } from 'react-icons/fa';

// دالة لاختيار الأيقونة المناسبة بناءً على النوع
const getIcon = (type) => {
  const props = { className: "w-5 h-5 text-white" }; // حجم الأيقونة داخل الدائرة
  switch (type) {
    case 'prescription':
      return <div className="p-3 bg-blue-500 rounded-full shadow-sm"><FaFileMedicalAlt {...props} /></div>;
    case 'labResult':
      return <div className="p-3 bg-purple-500 rounded-full shadow-sm"><FaFlask {...props} /></div>;
    case 'vaccine':
      return <div className="p-3 bg-green-500 rounded-full shadow-sm"><FaSyringe {...props} /></div>;
    default:
      return null;
  }
};

const ProfileItem = ({ type, title, subtitle, date }) => {
  return (
    <div className="flex items-center justify-between p-3 bg-slate-50 rounded-xl hover:bg-slate-100 transition-colors duration-200">
      <div className="flex items-center flex-1 min-w-0">
        {getIcon(type)}
        <div className="mr-4 flex-1 min-w-0">
          <p className="font-bold text-gray-800 truncate">{title}</p>
          <p className="text-sm text-gray-500 truncate">{subtitle}</p>
        </div>
      </div>
      <div className="flex items-center flex-shrink-0 text-sm text-gray-600">
        <p className="ml-4 whitespace-nowrap">{date}</p>
        <button className="mr-4 p-2 rounded-full hover:bg-gray-200 transition-colors" aria-label="تحميل">
          <FaDownload className="w-5 h-5 text-gray-500" />
        </button>
      </div>
    </div>
  );
};

export default ProfileItem;
