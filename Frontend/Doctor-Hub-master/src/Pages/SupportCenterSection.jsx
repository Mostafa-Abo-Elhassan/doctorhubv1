// src/components/sections/SupportCenterSection.jsx (التحديث: رقم جديد وإرسال واتس آب)

import React, { useState } from 'react';
import { ChevronDown, PhoneCall } from 'lucide-react';

// مكون فرعي لنموذج الأسئلة الشائعة القابل للطي (FAQ Item)
const DarkFAQItem = ({ question, answer, index }) => {
  const [isOpen, setIsOpen] = useState(index === 0);

  return (
    <div 
      className="mb-4"
      data-aos="fade-up"
      data-aos-delay={index * 100}
    >
      <button
        className="flex justify-between items-center w-full text-right p-4 rounded-xl bg-gray-800 hover:bg-gray-700 transition duration-300 focus:outline-none"
        onClick={() => setIsOpen(!isOpen)}
      >
        <span className="text-lg font-medium text-white">{question}</span>
        <ChevronDown size={24} className={`text-teal-400 transform transition-transform duration-300 ${isOpen ? 'rotate-180' : 'rotate-0'}`} />
      </button>
      
      {isOpen && (
        <div className="mt-2 p-4 text-gray-300 bg-gray-800 rounded-xl border border-gray-700">
          <p className="text-base">{answer}</p>
        </div>
      )}
    </div>
  );
};


const SupportCenterSection = () => {
  // الرقم الجديد الذي طلبه المستخدم (مع إضافة كود الدولة 2+ قبل الرقم)
  const WHATSAPP_NUMBER = '201201302871'; 
  const [formData, setFormData] = useState({ name: '', email: '', message: '' });

  const faqData = [
    {
      question: 'كيف يمكنني حجز موعد؟',
      answer: 'يمكنك ببساطة البحث عن طبيبك أو التخصص المطلوب في الصفحة الرئيسية، ثم النقر على "حجز موعد" واختيار الوقت المناسب من الجدول الزمني المتاح للطبيب مباشرة عبر المنصة.',
    },
    {
      question: 'ما هي خيارات الدفع المتاحة؟',
      answer: 'نحن نقبل الدفع عبر بطاقات الائتمان والخصم (فيزا وماستركارد)، وكذلك خدمات الدفع الإلكتروني المحلية مثل فوري وميزة، وخيار الدفع عند الحجز في بعض العيادات.',
    },
    {
      question: 'هل يمكنني إلغاء موعدي؟',
      answer: 'نعم، يمكن إلغاء الموعد أو إعادة جدولته مجانًا قبل 24 ساعة من الموعد المحدد. يمكنك القيام بذلك عبر لوحة تحكم المستخدم الخاصة بك.',
    },
    {
      question: 'كيف يمكنني التواصل مع الطبيب؟',
      answer: 'يمكنك التواصل مع طبيبك عبر خاصية الدردشة الآمنة في المنصة بعد تأكيد الحجز، أو من خلال خدمة الاستشارات عن بعد عبر الفيديو.',
    },
    {
      question: 'ماذا لو واجهت مشكلة تقنية؟',
      answer: 'يمكنك استخدام نموذج التواصل في هذه الصفحة أو الاتصال بالخط الساخن الموضح أدناه، وسيتولى فريق الدعم التقني حل مشكلتك على الفور.',
    },
  ];
  
  const inputClass = "w-full p-4 rounded-lg bg-gray-800 border border-gray-700 focus:border-teal-400 text-white placeholder-gray-500 transition duration-300";

  // دالة لتحديث حالة النموذج
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.id]: e.target.value });
  };
  
  // دالة لمعالجة إرسال النموذج وإعادة التوجيه إلى واتس آب
  const handleWhatsAppSubmit = (e) => {
    e.preventDefault(); // منع الإرسال الافتراضي

    const encodedMessage = encodeURIComponent(
      `مرحباً فريق Doctor Hub للدعم،\nأود التواصل بخصوص استفسار.\n\nالاسم: ${formData.name}\nالبريد الإلكتروني: ${formData.email}\nالرسالة:\n${formData.message}`
    );

    const whatsappLink = `https://wa.me/${WHATSAPP_NUMBER}?text=${encodedMessage}`;
    
    // فتح رابط الواتس آب في نافذة جديدة
    window.open(whatsappLink, '_blank');
  };


  return (
    <section dir="rtl" className="py-20 md:py-32 bg-gray-900 text-white overflow-x-hidden">
      <div className="container mx-auto px-4 lg:px-8">
        
        {/* العنوان الرئيسي */}
        <div className="text-center mb-16" data-aos="fade-down">
          <h1 className="text-5xl lg:text-6xl font-extrabold mb-2">مركز الدعم</h1>
          <p className="text-lg text-gray-400">نحن هنا لمساعدتك في أي شيء تحتاجه.</p>
        </div>

        {/* تقسيم المحتوى (FAQ + Contact Form) */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-12 items-start">
          
          {/* الجانب الأيمن: الأسئلة الشائعة والخط الساخن */}
          <div data-aos="fade-right" data-aos-duration="1000">
            <h2 className="text-3xl font-bold mb-6">الأسئلة الأكثر شيوعاً</h2>
            <div className="space-y-4">
              {faqData.map((item, index) => (
                <DarkFAQItem 
                  key={index}
                  question={item.question}
                  answer={item.answer}
                  index={index}
                />
              ))}
            </div>
            
            {/* الخط الساخن (Hotline) - الرقم الجديد */}
            <div className="mt-12 p-6 rounded-xl bg-gray-800 shadow-xl border border-teal-500/30">
                <h3 className="text-xl font-bold mb-4">الخط الساخن</h3>
                <p className="text-gray-400 mb-3">للاستفسارات العاجلة، اتصل بنا على:</p>
                <a href={`tel:+${WHATSAPP_NUMBER}`} className="flex items-center text-2xl font-extrabold text-teal-400 hover:text-teal-300 transition duration-200">
                    <PhoneCall size={28} className="ml-3" />
                    0120 130 2871
                </a>
            </div>
          </div>
          
          {/* الجانب الأيسر: نموذج التواصل (WhatsApp Submission) */}
          <div data-aos="fade-left" data-aos-duration="1000">
            <h2 className="text-3xl font-bold mb-6">تواصل معنا</h2>
            <form onSubmit={handleWhatsAppSubmit} className="space-y-6 bg-gray-900">
                
                {/* الاسم */}
                <div>
                    <label htmlFor="name" className="block text-sm font-medium mb-2">الاسم</label>
                    <input type="text" id="name" placeholder="اسمك بالكامل" value={formData.name} onChange={handleChange} required className={inputClass} />
                </div>
                
                {/* البريد الإلكتروني */}
                <div>
                    <label htmlFor="email" className="block text-sm font-medium mb-2">البريد الإلكتروني</label>
                    <input type="email" id="email" placeholder="بريدك الإلكتروني" value={formData.email} onChange={handleChange} required className={inputClass} />
                </div>
                
                {/* الرسالة */}
                <div>
                    <label htmlFor="message" className="block text-sm font-medium mb-2">الرسالة</label>
                    <textarea id="message" rows="5" placeholder="اكتب رسالتك هنا" value={formData.message} onChange={handleChange} required className={inputClass}></textarea>
                </div>
                
                {/* زر الإرسال */}
                <button 
                  type="submit" 
                  className="w-full bg-blue-600 hover:bg-blue-700 text-white font-bold py-3 rounded-lg transition duration-300 transform hover:scale-[1.01] shadow-lg"
                >
                  إرسال عبر واتس آب
                </button>
            </form>
          </div>
        </div>
        
      </div>
    </section>
  );
};

export default SupportCenterSection;