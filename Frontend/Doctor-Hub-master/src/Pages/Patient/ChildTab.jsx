import React from 'react';
import Empty from './shared/Empty';
import { FaSyringe, FaCalendarAlt } from 'react-icons/fa';

// --- بيانات وهمية لصفحة الطفل (للعرض إذا لم تكن هناك بيانات حقيقية) ---
const mockChildData = {
  nextVaccine: {
    age: '4 أشهر',
    dueDate: 'مستحق خلال أسبوعين',
    description: 'احم طفلك من الدفتيريا والكزاز والسعال الديكي وشلل الأطفال والمستدمية النزلية من النوع ب وأمراض المكورات الرئوية والفيروس العجلي.',
    vaccineNames: 'DTaP, IPV, Hib, PCV, RV'
  },
  vaccineSchedule: [
    { age: 'عند الولادة', vaccines: 'BCG, HepB', status: 'Completed' },
    { age: 'شهرين', vaccines: 'DTaP, IPV, Hib, PCV, RV', status: 'Completed' },
    { age: '4 أشهر (قادم)', vaccines: 'DTaP, IPV, Hib, PCV, RV', status: 'Pending' },
    { age: '6 أشهر', vaccines: 'DTaP, IPV, Hib, PCV, RV, Influenza (annually)', status: 'Upcoming' },
    { age: '12-15 شهراً', vaccines: 'MMR, Varicella, HepA, Hib, PCV', status: 'Upcoming' },
  ]
};
// --- نهاية البيانات الوهمية ---

const ChildTab = ({ childData }) => {
  // استخدم البيانات الحقيقية إن وجدت، وإلا استخدم البيانات الوهمية
  const finalChildData = childData || mockChildData;

  if (!finalChildData) {
    return <Empty text="لا توجد بيانات لمتابعة الطفل." />;
  }

  // دالة مساعدة لعرض حالة التطعيم (مكتمل، معلق، قادم)
  const renderStatusBadge = (status) => {
    let classes = 'px-3 py-1 text-xs font-semibold rounded-full';
    let text = '';
    switch (status) {
      case 'Completed':
        classes += ' bg-green-100 text-green-800';
        text = 'مكتمل';
        break;
      case 'Pending':
        classes += ' bg-yellow-100 text-yellow-800';
        text = 'معلق';
        break;
      case 'Upcoming':
        classes += ' bg-blue-100 text-blue-800';
        text = 'قادم';
        break;
      default:
        classes += ' bg-gray-100 text-gray-800';
        text = status;
    }
    return <span className={classes}>{text}</span>;
  };

  return (
    <div className="p-4 sm:p-6">
      <h1 className="text-3xl font-bold text-gray-800 mb-2">صحة الطفل</h1>
      <p className="text-gray-600 mb-8">إدارة السجلات الصحية وجداول التطعيمات لطفلك.</p>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
        {/* العمود الأيمن: تذكير التطعيم القادم */}
        <div className="bg-white rounded-xl shadow-md p-6">
          <h2 className="text-xl font-bold text-gray-800 mb-6">تذكير التطعيم القادم</h2>
          {finalChildData.nextVaccine ? (
            <div className="border border-blue-200 rounded-lg overflow-hidden">
              <div className="bg-blue-50 p-4 flex items-center justify-between">
                <div>
                  <p className="text-blue-600 text-sm">{finalChildData.nextVaccine.dueDate}</p>
                  <h3 className="text-xl font-bold text-blue-800 mt-1">تطعيمات {finalChildData.nextVaccine.age}</h3>
                </div>
                <FaSyringe className="w-10 h-10 text-blue-400" />
              </div>
              <div className="p-4 bg-white text-gray-700 text-sm leading-relaxed">
                <p>{finalChildData.nextVaccine.description}</p>
                <p className="mt-2 font-semibold">التطعيمات: {finalChildData.nextVaccine.vaccineNames}</p>
              </div>
              <button className="w-full bg-blue-500 text-white py-3 flex items-center justify-center gap-2 font-semibold hover:bg-blue-600 transition-colors rounded-b-lg">
                <FaCalendarAlt className="w-5 h-5" />
                <span>حجز موعد الآن</span>
              </button>
            </div>
          ) : (
            <Empty text="لا يوجد تذكير بالتطعيمات القادمة حالياً." />
          )}
        </div>

        {/* العمود الأيسر: جدول التطعيمات */}
        <div className="bg-white rounded-xl shadow-md p-6">
          <h2 className="text-xl font-bold text-gray-800 mb-6">جدول التطعيمات</h2>
          {finalChildData.vaccineSchedule && finalChildData.vaccineSchedule.length > 0 ? (
            <div className="space-y-4">
              {finalChildData.vaccineSchedule.map((item, index) => (
                <div key={index} className="flex items-center justify-between pb-4 border-b last:border-b-0 border-gray-100">
                  <div className="flex items-center">
                    <span className={`w-2.5 h-2.5 rounded-full ml-3 ${
                      item.status === 'Completed' ? 'bg-green-500' : 
                      item.status === 'Pending' ? 'bg-yellow-500' : 'bg-blue-500'
                    }`}></span>
                    <div>
                      <p className="font-semibold text-gray-800">{item.age}</p>
                      <p className="text-sm text-gray-600">{item.vaccines}</p>
                    </div>
                  </div>
                  {renderStatusBadge(item.status)}
                </div>
              ))}
            </div>
          ) : (
            <Empty text="لا يوجد جدول تطعيمات متاح." />
          )}
        </div>
      </div>
    </div>
  );
};

export default ChildTab;
