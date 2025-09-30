import React from 'react';
import logo from '../../assets/images/logo.jpg'; // تأكد من أن المسار صحيح
const AuthLayout = ({ children, title, subtitle }) => {
  return (
    <div dir="rtl" className="min-h-screen bg-gray-50 flex items-center justify-center p-4">
      <div 
        className="bg-white shadow-xl rounded-3xl flex max-w-4xl w-full overflow-hidden" 
        data-aos="zoom-in" 
        data-aos-duration="800"
      >
        {/* قسم الصورة (يأخذ حوالي 40% من المساحة على الشاشات الكبيرة) */}
        <div className="hidden md:flex md:w-2/5 bg-gradient-to-br from-blue-900 to-teal-500 items-center justify-center p-8">
          <div className="text-center text-white">
            <div className="mx-auto mb-4 w-40 h-40 rounded-full overflow-hidden flex items-center justify-center bg-white p-2"> {/* حاوية للصورة الدائرية */}
                <img 
                    src={logo} // ⬅️ استخدام الرابط البديل هنا
                    alt="Doctor Hub" 
                    className="w-full h-full object-cover rounded-full border-4 border-teal-400" 
                />
            </div>
            
            <h2 className="text-3xl font-bold mb-4">أهلاً بك في Doctor Hub</h2>
            <p className="text-lg">نظامك المتكامل لثورة الرعاية الصحية في مصر.</p>
          </div>
        </div>

        {/* قسم النموذج (يأخذ حوالي 60% من المساحة على الشاشات الكبيرة) */}
        <div className="w-full md:w-3/5 p-8 sm:p-12">
          <div className="text-right mb-8">
            <h1 className="text-4xl font-extrabold text-blue-900 mb-2">{title}</h1>
            <p className="text-gray-600">{subtitle}</p>
          </div>
          {children}
        </div>
      </div>
    </div>
  );
};

export default AuthLayout;
