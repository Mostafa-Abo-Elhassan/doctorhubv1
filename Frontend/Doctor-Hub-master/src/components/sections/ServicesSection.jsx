// src/components/sections/ServicesSection.jsx

import React from 'react';
import { BriefcaseMedical, Calendar, FileText, Globe, MessageSquare, Clipboard } from 'lucide-react';
import { Link } from 'react-router-dom';

// مكون فرعي لبطاقة الخدمة
const ServiceCard = ({ icon: Icon, title, description, delay }) => (
  <div 
    className="bg-white p-6 rounded-xl shadow-lg border border-gray-100 flex flex-col items-center text-center transition duration-500 hover:shadow-2xl hover:border-teal-400 h-full"
    data-aos="fade-up"
    data-aos-delay={delay}
  >
    <div className="flex items-center justify-center w-16 h-16 mb-4 rounded-full bg-teal-100 text-teal-600">
      <Icon size={32} />
    </div>
    <h3 className="text-xl font-bold text-gray-800 mb-2">{title}</h3>
    <p className="text-gray-600 text-base flex-grow">{description}</p>
    
    <Link 
      to="/contact" 
      className="mt-4 text-teal-600 font-medium hover:text-teal-700 transition duration-300"
    >
      تواصل معانا →
    </Link>
  </div>
);

const ServicesSection = () => {
  const servicesData = [
    {
      icon: Calendar,
      title: 'حجز المواعيد الفوري',
      description: 'ابحث عن طبيبك المفضل، وتعرف على جدول مواعيده، واحجز بضغطة زر دون انتظار.',
      delay: 100,
    },
    {
      icon: BriefcaseMedical,
      title: 'الاستشارات عن بعد',
      description: 'تواصل مع طبيبك عبر مكالمات الفيديو الآمنة للحصول على استشارة سريعة من أي مكان.',
      delay: 200,
    },
    {
      icon: FileText,
      title: 'السجلات الطبية الرقمية',
      description: 'ملف طبي واحد موحد وآمن لجميع سجلاتك، والوصفات الطبية، ونتائج التحاليل.',
      delay: 300,
    },
    {
      icon: Clipboard,
      title: 'تكامل المختبرات والصيدليات',
      description: 'إرسال الوصفات والتحاليل إلكترونياً لضمان الدقة والسرعة في التنفيذ.',
      delay: 400,
    },
    {
      icon: MessageSquare,
      title: 'التوعية الصحية',
      description: 'الوصول إلى مقالات وموارد تعليمية موثوقة لتعزيز الوعي الصحي العام.',
      delay: 500,
    },
    {
      icon: Globe,
      title: 'إدارة الرعاية الحكومية',
      description: 'تسهيل تواصل المؤسسات الصحية الحكومية وإدارة بيانات الرعاية الصحية الوطنية.',
      delay: 600,
    },
  ];

  return (
    <section dir="rtl" className="py-20 md:py-32 bg-white overflow-x-hidden">
      <div className="container mx-auto px-4 lg:px-8">
        
        {/* العنوان الرئيسي */}
        <div className="text-center mb-16" data-aos="fade-up">
          <span className="text-teal-600 font-semibold text-lg">كل ما تحتاجه في مكان واحد</span>
          <h2 className="text-4xl lg:text-5xl font-extrabold text-gray-800 mt-2">خدماتنا الشاملة لجميع الأطراف</h2>
        </div>

        {/* شبكة الخدمات المتجاوبة (Responsive Grid) */}
        <div 
          className="grid grid-cols-1 gap-8 
                     md:grid-cols-2 
                     lg:grid-cols-3" // عرض 3 بطاقات في الصف على الشاشات الكبيرة
        >
          {servicesData.map((service, index) => (
            <ServiceCard 
              key={index}
              icon={service.icon}
              title={service.title}
              description={service.description}
              delay={service.delay}
            />
          ))}
        </div>
        
        
      </div>
    </section>
  );
};

export default ServicesSection;