// src/pages/NotFound.jsx

import React from 'react';
import { Link } from 'react-router-dom';

const NotFound = () => {
  return (
    <div 
      dir="rtl" 
      className="min-h-screen bg-gray-50 flex flex-col items-center justify-center text-center p-6"
    >
      <div 
        className="bg-white p-12 sm:p-16 rounded-3xl shadow-2xl max-w-lg w-full"
        data-aos="zoom-in"
      >
        {/* الأيقونة (Placeholder for a real Icon) */}
        <div className="mb-8 flex justify-center">
          {/* أيقونة X كبيرة للدلالة على الخطأ، يمكنك استبدالها بأيقونة من مكتبة */}
          <svg 
            className="w-20 h-20 text-red-500" 
            fill="none" 
            stroke="currentColor" 
            viewBox="0 0 24 24" 
            xmlns="http://www.w3.org/2000/svg"
          >
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
          </svg>
        </div>

        {/* رمز الخطأ الرئيسي */}
        <h1 className="text-8xl font-extrabold text-blue-900 mb-4">
          404
        </h1>
        
        {/* العنوان والوصف */}
        <h2 className="text-3xl font-bold text-gray-800 mb-3">
          عفواً، الصفحة غير موجودة!
        </h2>
        <p className="text-lg text-gray-600 mb-8">
          يبدو أنك حاولت الوصول إلى صفحة لم تعد متوفرة أو أن الرابط غير صحيح. لا تقلق، يمكنك العودة إلى الصفحة الرئيسية.
        </p>

        {/* زر العودة للرئيسية */}
        <Link 
          to="/home" 
          className="inline-flex items-center px-8 py-3 border border-transparent text-base font-medium rounded-lg shadow-md text-white bg-teal-600 hover:bg-teal-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-teal-500 transition duration-300"
        >
          العودة إلى الصفحة الرئيسية
        </Link>
      </div>
    </div>
  );
};

export default NotFound;