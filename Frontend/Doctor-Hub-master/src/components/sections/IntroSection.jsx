// src/components/sections/IntroSection.jsx (الإصلاح والتحسين)

import React from 'react';
import { ChevronLeft } from 'lucide-react';
import { Link } from 'react-router-dom';

const IntroSection = () => {
  return (
    <section dir="rtl" className="py-16 md:py-24 bg-white overflow-x-hidden"> {/* تم إضافة overflow-x-hidden */}
      <div className="container mx-auto px-4 lg:px-8">
        
        {/* نستخدم Grid هنا لتقسيم المحتوى. يتم الترتيب التلقائي عمودياً للهاتف */}
        <div className="grid grid-cols-1 md:grid-cols-12 gap-8 md:gap-16 items-center">
          
          {/* الجانب الأيسر: الصورة (تظهر الآن على الهاتف في الأعلى) */}
          <div className="md:col-span-5 md:order-last" data-aos="fade-left" data-aos-duration="1000">
            {/* تم إزالة "hidden md:block" لجعلها تظهر على الهاتف */}
            <div className="relative w-full h-72 md:h-96 rounded-3xl overflow-hidden shadow-2xl">
              <img 
                src="https://m.gomhuriaonline.com/Upload/News/6-3-2019_10_46_25_GomhuriaOnline_1551861985.jpg" 
                alt="طبيب يبتسم مرحبا بكم في Doctor Hub" 
                className="w-full h-full object-cover"
              />
              <div className="absolute inset-0 bg-blue-900 opacity-10 rounded-3xl"></div>
            </div>
          </div>

          {/* الجانب الأيمن: المحتوى النصي والدعوة للإجراء (يأخذ 7 أعمدة) */}
          <div className="md:col-span-7" data-aos="fade-right" data-aos-duration="1000">
            <h1 className="text-4xl lg:text-5xl font-extrabold text-gray-900 mb-6 leading-tight">
              أهلاً بك في مستقبل الرعاية الصحية الرقمية
            </h1>
            
            <p className="text-lg text-gray-600 mb-8">
              نحن نؤمن بأن الرعاية الصحية يجب أن تكون في متناول الجميع، بسيطة وفعالة. **Doctor Hub** هي منصتك الموحدة لإدارة رحلتك الصحية بالكامل، من الموعد الأول حتى المتابعة.
            </p>
            
            {/* النقاط السريعة */}
            <ul className="space-y-3 mb-8 text-gray-700">
              <li className="flex items-start">
                {/* تم تعديل الدوران ليتناسب مع الاتجاه RTL */}
                <ChevronLeft size={20} className="text-teal-500 flex-shrink-0 mt-1 ml-2 rtl:transform rtl:rotate-180" />
                <span className="font-medium">سهولة حجز المواعيد مع نخبة الأطباء.</span>
              </li>
              <li className="flex items-start">
                <ChevronLeft size={20} className="text-teal-500 flex-shrink-0 mt-1 ml-2 rtl:transform rtl:rotate-180" />
                <span className="font-medium">الوصول الفوري للسجلات الطبية والوصفات.</span>
              </li>
              <li className="flex items-start">
                <ChevronLeft size={20} className="text-teal-500 flex-shrink-0 mt-1 ml-2 rtl:transform rtl:rotate-180" />
                <span className="font-medium">تواصل سلس وآمن بين جميع أطراف الرعاية الصحية.</span>
              </li>
            </ul>
            
            {/* زر الدعوة لاتخاذ إجراء */}
            <Link 
              to="/register" 
              className="inline-block bg-teal-600 hover:bg-teal-700 text-white text-lg font-bold py-3 px-8 rounded-lg shadow-xl transition duration-300 transform hover:scale-105"
            >
              ابدأ رحلتك الصحية اليوم
            </Link>
          </div>
          
        </div>
      </div>
    </section>
  );
};

export default IntroSection;