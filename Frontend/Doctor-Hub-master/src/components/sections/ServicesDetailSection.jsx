// src/components/sections/ServicesDetailSection.jsx (التحديث لتجميع الفئات)

import React from 'react';
import { 
  BriefcaseMedical, 
  Calendar, 
  FileText, 
  Stethoscope, 
  Pill, // إصلاح Pill
  FlaskConical, 
  Hospital, 
  Globe, 
  ArrowRight 
} from 'lucide-react';

// مكون فرعي لبطاقة الخدمة
const ServiceCard = ({ title, description, delay }) => (
  <div 
    className="bg-white p-6 rounded-xl shadow-md border border-gray-100 flex flex-col transition duration-300 hover:shadow-lg h-full"
    data-aos="fade-up"
    data-aos-delay={delay}
  >
    <h4 className="text-xl font-bold text-gray-800 mb-2">{title}</h4>
    <p className="text-gray-600 text-base flex-grow mb-4">{description}</p>
    
    
  </div>
);

// مكون فرعي لمجموعة الخدمات حسب الفئة
const ServiceCategory = ({ icon: Icon, title, categoryId, services, gridCols = 'md:grid-cols-3' }) => (
  <div id={categoryId} className="pt-12 pb-4" data-aos="fade-up">
    <div className="flex items-center mb-8">
      <Icon size={32} className="text-teal-600 ml-3" />
      <h3 className="text-2xl font-bold text-gray-800">{title}</h3>
    </div>

    {/* شبكة البطاقات المتجاوبة */}
    <div 
      className={`grid grid-cols-1 gap-8 ${gridCols}`}
    >
      {services.map((service, index) => (
        <ServiceCard 
          key={index}
          title={service.title}
          description={service.description}
          delay={index * 100 + 100}
        />
      ))}
    </div>
  </div>
);


const ServicesDetailSection = () => {
  const categories = [
    {
      icon: BriefcaseMedical,
      title: 'للمرضى (For Patients)',
      id: 'patients',
      services: [
        { icon: FileText, title: 'السجل الصحي الموحد', description: 'الوصول إلى سجلاتك الطبية الكاملة، التشخيصات، العلاجات، وكل شيء في ملف رقمي واحد آمن.' },
        { icon: Calendar, title: 'المواعيد عبر الإنترنت', description: 'جدولة وإدارة المواعيد مع مختصين الرعاية الصحية من راحة منزلك.' },
        { icon: Stethoscope, title: 'مساعد صحي بالذكاء الاصطناعي', description: 'احصل على نصائح صحية شخصية، فحص الأعراض، وتذكيرات بالأدوية عبر مساعد يعمل بالذكاء الاصطناعي.' },
      ],
      gridCols: 'md:grid-cols-3', // 3 أعمدة للمرضى
    },
    {
      icon: Stethoscope,
      title: 'للأطباء (For Doctors)',
      id: 'doctors',
      services: [
        { icon: BriefcaseMedical, title: 'أدوات التشخيص المساعدة', description: 'عزز قدراتك التشخيصية بالوصول إلى أدوات وموارد متقدمة.' },
        { icon: FileText, title: 'الوصفات الإلكترونية (E-Prescriptions)', description: 'إصدار وإدارة الوصفات الطبية إلكترونيًا، مما يضمن الدقة ويقلل الأخطاء.' },
      ],
      gridCols: 'md:grid-cols-2', // 2 عمود للأطباء
    },
  ];
  
  // تجميع الفئات في صفوف مزدوجة (Pairing categories)
  const pairedCategories = [
    {
      left: { 
          icon: Pill, 
          title: 'للصيادلة (For Pharmacists)', 
          id: 'pharmacists', 
          services: [{ title: 'صرف الأدوية الآمن', description: 'التحقق من الوصفات وصرف الأدوية بأمان، لضمان سلامة المريض والامتثال.' }] 
      },
      right: { 
          icon: FlaskConical, 
          title: 'للمختبرات (For Labs)', 
          id: 'labs', 
          services: [{ title: 'رفع نتائج المختبر', description: 'سهولة تحميل ومشاركة نتائج المختبر مع المرضى ومقدمي الرعاية الصحية عبر منصتنا الآمنة.' }] 
      },
    },
    {
      left: { 
          icon: Hospital, 
          title: 'للمستشفيات (For Hospitals)', 
          id: 'hospitals', 
          services: [{ title: 'دعم الطوارئ', description: 'الوصول إلى بيانات المرضى في الوقت الحقيقي وتنسيق الاستجابة لحالات الطوارئ بكفاءة.' }] 
      },
      right: { 
          icon: Globe, 
          title: 'للحكومة (For Government)', 
          id: 'government', 
          services: [{ title: 'لوحات القيادة والتحليلات الوطنية', description: 'الحصول على رؤى حول الاتجاهات الصحية الوطنية وأداء الرعاية الصحية من خلال لوحات قيادة شاملة.' }] 
      },
    }
  ];

  return (
    <section dir="rtl" className="py-20 md:py-12 bg-gray-50 overflow-x-hidden">
      <div className="container mx-auto px-4 lg:px-8">
        
        {/* العنوان الرئيسي */}
        <div className="text-center mb-16" data-aos="fade-up">
          <h1 className="text-4xl lg:text-5xl font-extrabold text-gray-800 mt-2">مجموعتنا الشاملة من الخدمات</h1>
          <p className="text-lg text-gray-600 max-w-3xl mx-auto mt-4">
            Doctor Hub يقدم مجموعة شاملة من الحلول الرقمية المصممة للمرضى، ومقدمي الرعاية الصحية، والمؤسسات في جميع أنحاء مصر.
          </p>
        </div>

        {/* 1. فئات غير المجمعة (المرضى والأطباء) */}
        {categories.map((category, index) => (
          <ServiceCategory 
            key={index}
            icon={category.icon}
            title={category.title}
            categoryId={category.id}
            services={category.services}
            gridCols={category.gridCols}
          />
        ))}

        {/* 2. الفئات المجمعة (الصيادلة، المختبرات، المستشفيات، الحكومة) */}
        <div className="pt-8 space-y-8">
        {pairedCategories.map((pair, index) => (
          <div 
            key={index}
            className="grid grid-cols-1 md:grid-cols-2 gap-12 border-t border-gray-200" // هذا هو الجزء الأساسي الذي يجعلها 2 جنبًا إلى جنب
          >
            {/* الفئة اليسرى */}
            <ServiceCategory 
                icon={pair.left.icon}
                title={pair.left.title}
                categoryId={pair.left.id}
                services={pair.left.services}
                gridCols='md:grid-cols-1' // بطاقة واحدة لكل صف فرعي
            />
            
            {/* الفئة اليمنى */}
            <ServiceCategory 
                icon={pair.right.icon}
                title={pair.right.title}
                categoryId={pair.right.id}
                services={pair.right.services}
                gridCols='md:grid-cols-1' // بطاقة واحدة لكل صف فرعي
            />
          </div>
        ))}
        </div>
        
      </div>
    </section>
  );
};

export default ServicesDetailSection;