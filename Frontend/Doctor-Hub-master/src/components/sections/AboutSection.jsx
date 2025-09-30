// src/components/sections/AboutSection.jsx

import React from 'react';
import { Target, TrendingUp, Handshake, Heart } from 'lucide-react';
import { Link } from 'react-router-dom';

// مكون فرعي لبطاقة الهدف
const GoalCard = ({ icon: Icon, title, description, delay }) => (
  <div 
    className="flex p-5 bg-white rounded-lg shadow-md border border-gray-100"
    data-aos="fade-up"
    data-aos-delay={delay}
  >
    <div className="flex-shrink-0 ml-4">
      <Icon size={32} className="text-teal-600" />
    </div>
    <div>
      <h3 className="text-xl font-bold text-gray-800 mb-1">{title}</h3>
      <p className="text-gray-600 text-base">{description}</p>
    </div>
  </div>
);

const AboutSection = () => {
  const goalsData = [
    {
      icon: Target,
      title: 'الوصول الشامل',
      description: 'جعل الرعاية الصحية عالية الجودة في متناول كل مواطن، بغض النظر عن الموقع.',
      delay: 100,
    },
    {
      icon: TrendingUp,
      title: 'تحسين الكفاءة',
      description: 'رقمنة العمليات لتقليل الأخطاء الإدارية وزيادة سرعة تقديم الخدمات.',
      delay: 200,
    },
    {
      icon: Handshake,
      title: 'التعاون المتكامل',
      description: 'إنشاء نظام بيئي يربط جميع مقدمي الرعاية الصحية بشكل آمن وسلس.',
      delay: 300,
    },
    {
      icon: Heart,
      title: 'التركيز على المريض',
      description: 'وضع المريض في صميم العملية، بتقديم خدمة شخصية تركز على احتياجاته.',
      delay: 400,
    },
  ];

  return (
    <section dir="rtl" className="py-20 md:py-32 bg-gray-100 overflow-x-hidden">
      <div className="container mx-auto px-4 lg:px-8">
        
        {/* العنوان الرئيسي */}
        <div className="text-center mb-16" data-aos="fade-up">
          <span className="text-teal-600 font-semibold text-lg">رؤيتنا وقيمنا</span>
          <h2 className="text-4xl lg:text-5xl font-extrabold text-gray-800 mt-2">عن Doctor Hub</h2>
        </div>

        {/* تقسيم المحتوى باستخدام Grid (قسم النص والصورة) */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-12 items-center mb-20">
          
          {/* الجانب الأيمن: النص التفصيلي */}
          <div data-aos="fade-right" data-aos-duration="1000">
            <h3 className="text-3xl font-bold text-gray-900 mb-6">نحن لسنا مجرد منصة، نحن شركاء في رحلتك الصحية.</h3>
            
            <p className="text-lg text-gray-700 mb-6">
              **Doctor Hub** هو مشروع وطني يهدف إلى تطبيق مفهوم التحول الرقمي الشامل في قطاع الرعاية الصحية بمصر. نحن نعمل كجسر يربط بين **المريض، الطبيب، الصيدلي، المختبر، والمؤسسات الحكومية** تحت مظلة رقمية واحدة.
            </p>
            
            <p className="text-lg text-gray-700 mb-8 font-medium">
              مهمتنا هي تبسيط العمليات الطبية المعقدة، وتعزيز الشفافية، وتقديم تجربة صحية آمنة ومريحة للجميع.
            </p>

            <Link 
              to="/about" 
              className="inline-flex items-center text-teal-600 font-bold text-lg hover:text-teal-700 transition duration-300"
            >
             
            </Link>
          </div>
          
          {/* الجانب الأيسر: الصورة / الرسم البياني */}
          <div data-aos="fade-left" data-aos-duration="1000">
            <div className="relative w-full h-80 md:h-96 rounded-3xl overflow-hidden shadow-2xl">
               {/* نستخدم صورة تعبر عن الرؤية الرقمية أو الفريق الطبي */}
              <img 
                src="https://cdnx.premiumread.com/?url=https://www.okaz.com.sa/uploads/images/2019/01/12/1118514.jpg&w=400&q=100&f=webp" 
                alt="رؤية Doctor Hub للرعاية الصحية الرقمية" 
                className="w-full h-full object-cover"
              />
            </div>
          </div>
        </div>

        {/* قسم الأهداف (Goals Section) */}
        <div className="mt-16 text-center" data-aos="fade-up">
            <h3 className="text-3xl font-bold text-gray-900 mb-8">أهدافنا الاستراتيجية</h3>
        </div>

        {/* شبكة الأهداف المتجاوبة */}
        <div 
          className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8"
        >
          {goalsData.map((goal, index) => (
            <GoalCard
              key={index}
              icon={goal.icon}
              title={goal.title}
              description={goal.description}
              delay={goal.delay}
            />
          ))}
        </div>
        
      </div>
    </section>
  );
};

export default AboutSection;