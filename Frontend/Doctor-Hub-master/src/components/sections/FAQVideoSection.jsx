// src/components/sections/FAQVideoSection.jsx

import React, { useState } from 'react';
import { ChevronDown } from 'lucide-react';

// مكون فرعي للأسئلة الشائعة القابلة للطي
const FAQItem = ({ question, answer, index, defaultOpen }) => {
  const [isOpen, setIsOpen] = useState(defaultOpen || false);

  return (
    <div 
      className="border-b border-gray-200 py-4"
      data-aos="fade-up"
      data-aos-delay={index * 100}
    >
      <button
        className="flex justify-between items-center w-full text-right focus:outline-none"
        onClick={() => setIsOpen(!isOpen)}
      >
        <span className="text-lg font-semibold text-gray-800 hover:text-teal-600 transition duration-300">{question}</span>
        <ChevronDown size={24} className={`text-teal-600 transform transition-transform duration-300 ${isOpen ? 'rotate-180' : 'rotate-0'}`} />
      </button>
      
      {isOpen && (
        <div className="mt-3 pr-6 transition-all duration-500 ease-in-out">
          <p className="text-gray-600">{answer}</p>
        </div>
      )}
    </div>
  );
};


const FAQVideoSection = () => {
  // فيديو Placeholder (يمكن استبدال الرابط برابط YouTube الفعلي)
  const videoUrl = "https://www.shutterstock.com/shutterstock/videos/1111088999/preview/stock-footage-tracking-dolly-shot-of-hospital-corridor-and-lobby-filled-with-group-of-diverse-indian-patients.webm"; // Placeholder URL
  
  const faqData = [
    {
      question: 'كيف يمكنني حجز موعد عبر Doctor Hub؟',
      answer: 'يمكنك استخدام شريط البحث لتحديد التخصص أو اسم الطبيب، ثم اختر الموعد المناسب من الجدول الزمني المتاح للطبيب مباشرة عبر المنصة.',
    },
    {
      question: 'هل بياناتي الطبية آمنة؟',
      answer: 'نعم، نستخدم أعلى معايير التشفير والأمان العالمية (HIPAA compliant) لحماية جميع السجلات والمعلومات الطبية والشخصية للمستخدمين.',
    },
    {
      question: 'ما هي أنواع الخدمات الطبية المتاحة عن بعد؟',
      answer: 'نوفر استشارات فيديو لمتابعة الحالات المزمنة، قراءة التحاليل، والمتابعات الروتينية، بالإضافة إلى إمكانية الحصول على الوصفات الطبية الإلكترونية.',
    },
    {
      question: 'كيف يمكن للمستشفيات الانضمام إلى شبكتكم؟',
      answer: 'يجب على المؤسسات الصحية التواصل مع فريق الدعم لدينا لترتيب جلسة عرض خاصة وبدء عملية التكامل التقني لربط الأنظمة.',
    },
  ];

  return (
    <section dir="rtl" className="py-20 md:py-32 bg-gray-50 overflow-x-hidden">
      <div className="container mx-auto px-4 lg:px-8">
        
        {/* العنوان الرئيسي */}
        <div className="text-center mb-16" data-aos="fade-up">
          <h2 className="text-4xl lg:text-5xl font-extrabold text-gray-800 mt-2">مركز الدعم والأسئلة الشائعة</h2>
          <p className="text-lg text-gray-600 mt-3">كل ما تحتاج لمعرفته عن رحلتك معنا.</p>
        </div>

        {/* تقسيم المحتوى (فيديو + أسئلة شائعة) */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-12 items-start">
          
          {/* الجانب الأيمن: الفيديو التعريفي */}
          <div data-aos="fade-right" data-aos-duration="1000">
            <h3 className="text-3xl font-bold text-gray-800 mb-6">شاهد رؤيتنا في دقيقة واحدة</h3>
            <div className="relative w-full overflow-hidden rounded-xl shadow-2xl" style={{ paddingTop: '56.25%' }}> {/* نسبة 16:9 للفيديو */}
              <iframe
                className="absolute top-0 left-0 w-full h-full border-0"
                src={videoUrl}
                title="فيديو تعريفي عن Doctor Hub"
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                allowFullScreen
              ></iframe>
            </div>
            
            
          </div>
          
          {/* الجانب الأيسر: الأسئلة الشائعة */}
          <div data-aos="fade-left" data-aos-duration="1000">
            <h3 className="text-3xl font-bold text-gray-800 mb-6">الأسئلة الأكثر شيوعاً</h3>
            <div className="bg-white p-6 rounded-xl shadow-lg">
              {faqData.map((item, index) => (
                <FAQItem 
                  key={index}
                  question={item.question}
                  answer={item.answer}
                  index={index}
                  defaultOpen={index === 0} // فتح أول سؤال بشكل افتراضي
                />
              ))}
            </div>
          </div>
        </div>
        
      </div>
    </section>
  );
};

export default FAQVideoSection;