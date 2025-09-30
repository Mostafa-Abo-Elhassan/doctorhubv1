// src/components/sections/SolutionsSection.jsx

import React from 'react';
// افتراض استخدام أيقونات من مكتبة مثل Lucide أو Font Awesome
import { Users, Stethoscope, Briefcase, Hospital } from 'lucide-react'; 

const SolutionCard = ({ icon: Icon, title, description, delay }) => (
  // استخدام التجاوب لطباعة الظل والتحويل عند التمرير
  <div 
    className="p-6 bg-white rounded-xl shadow-lg hover:shadow-2xl transition duration-500 transform hover:-translate-y-1 border border-gray-100"
    data-aos="fade-up"
    data-aos-delay={delay}
  >
    <div className="flex items-center justify-center w-12 h-12 mb-4 rounded-full bg-teal-100 text-teal-600">
      <Icon size={24} />
    </div>
    <h3 className="text-xl font-bold text-gray-800 mb-2">{title}</h3>
    <p className="text-gray-600 text-base">{description}</p>
  </div>
);

const SolutionsSection = () => {
  const solutionsData = [
    {
      icon: Users,
      title: 'إدارة المرضى',
      description: 'إدارة المواعيد، الوصول إلى السجلات الطبية، والتواصل السهل مع مقدمي الرعاية الصحية.',
      delay: 100,
    },
    {
      icon: Stethoscope,
      title: 'التعاون الطبي',
      description: 'التعاون مع الزملاء، مشاركة بيانات المرضى، والوصول إلى المعلومات الطبية الأساسية.',
      delay: 200,
    },
    {
      icon: Briefcase,
      title: 'دمج الصيدليات',
      description: 'تبسيط تلبية الوصفات الطبية، إدارة المخزون، والربط مع المرضى بشكل فعال.',
      delay: 300,
    },
    {
      icon: Hospital,
      title: 'توصيل المستشفيات',
      description: 'تحسين الكفاءة التشغيلية، إدارة تدفق المرضى، وتكامل بيانات الرعاية الصحية.',
      delay: 400,
    },
  ];

  return (
    <section dir="rtl" className="py-20 bg-gray-50">
      <div className="container mx-auto px-4 lg:px-8">
        
        {/* العنوان والوصف الرئيسي */}
        <div className="text-center mb-12" data-aos="fade-up">
          <h2 className="text-4xl font-extrabold text-gray-800 mb-3">حلول شاملة للرعاية الصحية</h2>
          <p className="text-lg text-gray-600 max-w-3xl mx-auto">
            Doctor Hub يقدم مجموعة من الميزات لتعزيز تقديم الرعاية الصحية وسهولة الوصول لجميع الأطراف المعنية.
          </p>
        </div>

        {/* شبكة البطاقات المتجاوبة (Responsive Grid) */}
        <div 
          className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8"
        >
          {solutionsData.map((solution, index) => (
            <SolutionCard 
              key={index}
              icon={solution.icon}
              title={solution.title}
              description={solution.description}
              delay={solution.delay}
            />
          ))}
        </div>
      </div>
    </section>
  );
};

export default SolutionsSection;