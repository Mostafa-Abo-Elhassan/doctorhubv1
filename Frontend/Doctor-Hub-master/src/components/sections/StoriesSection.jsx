// src/components/sections/StoriesSection.jsx (التحديث بالصور)

import React from 'react';
import { Quote, Star } from 'lucide-react'; 

const StoryCard = ({ name, role, quote, rating, image, delay }) => ( // تم إضافة 'image' هنا
  <div 
    className="bg-white p-6 rounded-xl shadow-lg border border-gray-100 hover:shadow-xl transition duration-500 flex flex-col h-full"
    data-aos="fade-up"
    data-aos-delay={delay}
  >
    {/* أيقونة الاقتباس */}
    <Quote size={32} className="text-teal-500 mb-4 flex-shrink-0" />
    
    {/* التقييم النجمي */}
    <div className="flex items-center mb-4">
      {[...Array(5)].map((_, i) => (
        <Star 
          key={i} 
          size={16} 
          className={i < rating ? 'text-yellow-400 fill-yellow-400 ml-1' : 'text-gray-300 ml-1'} 
        />
      ))}
    </div>

    {/* الاقتباس النصي */}
    <p className="text-gray-700 italic mb-6 flex-grow">"{quote}"</p>
    
    {/* معلومات المستخدم والصورة */}
    <div className="mt-auto pt-4 flex items-center border-t border-gray-200">
      {image && ( // عرض الصورة إذا كانت موجودة
        <img 
          src={image} 
          alt={name} 
          className="w-12 h-12 rounded-full object-cover ml-4 flex-shrink-0" 
        />
      )}
      <div>
        <h4 className="text-lg font-bold text-gray-800">{name}</h4>
        <p className="text-sm text-teal-600">{role}</p>
      </div>
    </div>
  </div>
);

const StoriesSection = () => {
  const storiesData = [
    {
      name: 'د. أحمد محمود',
      role: 'طبيب استشاري',
      quote: 'منصة Doctor Hub سهلت علي إدارة مواعيدي ومتابعة سجلات مرضاي بشكل لم أتصوره، إنها ثورة حقيقية في إدارة العيادات.',
      rating: 5,
      image: 'https://img.pikbest.com/photo/20250223/male-doctor-smiling-at-the-camera_11549073.jpg!f305cw', // الصورة 1
      delay: 100,
    },
    {
      name: '  محمد علي',
      role: 'طبيب',
      quote: 'أصبح حجز المواعيد والحصول على الوصفات أمرًا سهلاً للغاية. المنصة وفرت علي الكثير من الوقت والجهد.',
      rating: 4,
      image: 'https://img.pikbest.com/photo/20241218/-smiling-young-doctor-image-isolated-on-gray-background_11270153.jpg!f305cw', // الصورة 2
      delay: 200,
    },
    {
      name: 'صيدلية النور',
      role: 'صيدلي',
      quote: 'الربط المباشر مع الأطباء قلل من الأخطاء في صرف الأدوية وساعدنا على إدارة المخزون بكفاءة عالية.',
      rating: 5,
      image: 'https://png.pngtree.com/thumb_back/fh260/background/20250410/pngtree-confident-male-doctor-with-face-mask-showing-thumbs-up-in-hospital-image_17179851.jpg', // الصورة 3
      delay: 300,
    },
    {
      name: 'مستشفى الأمل',
      role: 'مدير العمليات',
      quote: 'إدارة تدفق المرضى وتكامل البيانات أدى إلى تحسين كفاءة المستشفى بنسبة تفوق الـ 30%. منصة لا غنى عنها.',
      rating: 5,
      image: 'https://png.pngtree.com/thumb_back/fh260/background/20250121/pngtree-a-caring-male-doctor-holding-medical-chart-in-one-hand-and-image_16914039.jpg', // الصورة 4
      delay: 400,
    },
  ];

  return (
    <section dir="rtl" className="py-20 bg-gray-50 overflow-x-hidden">
      <div className="container mx-auto px-4 lg:px-8">
        
        {/* العنوان والوصف الرئيسي */}
        <div className="text-center mb-12" data-aos="fade-up">
          <h2 className="text-4xl font-extrabold text-blue-900 mb-3">قصص نجاح من مجتمعنا</h2>
          <p className="text-lg text-gray-600 max-w-3xl mx-auto">
            تعرف على آراء وخبرات المستفيدين من منصة Doctor Hub من مختلف فئات المجتمع الطبي والمرضى.
          </p>
        </div>

        {/* شبكة البطاقات المتجاوبة (Responsive Grid) */}
        <div 
          className="grid grid-cols-1 gap-8 
                     md:grid-cols-2 lg:grid-cols-4" 
        >
          {storiesData.map((story, index) => (
            <StoryCard 
              key={index}
              name={story.name}
              role={story.role}
              quote={story.quote}
              rating={story.rating}
              image={story.image} // تمرير الصورة إلى البطاقة
              delay={story.delay}
            />
          ))}
        </div>
      </div>
    </section>
  );
};

export default StoriesSection;