import React from 'react';
import Empty from '../../Pages/Patient/shared/Empty';
import { FaFlask, FaUser, FaCheckCircle, FaHourglassHalf, FaPlus } from 'react-icons/fa';

// --- البيانات الوهمية ---
const mockLabOrders = [
  { id: 'mock-lo1', patientId: '1', type: 'تحليل دم شامل (CBC)', status: 'Pending' },
  { id: 'mock-lo2', patientId: '2', type: 'تحليل فيتامين د', status: 'Completed' },
  { id: 'mock-lo3', patientId: '3', type: 'أشعة رنين مغناطيسي (MRI)', status: 'Pending' },
];
const mockPatients = [
  { id: '1', name: 'أحمد علي' },
  { id: '2', name: 'منى حسن' },
  { id: '3', name: 'أحمد عبد الجواد' },
];
// --- نهاية البيانات الوهمية ---

// مكون بطاقة طلب التحليل (لا يتغير)
const LabOrderCard = ({ order, patient }) => {
  const isCompleted = order.status === 'Completed';
  return (
    <div className="bg-white rounded-xl shadow-md p-4">
      <div className="flex justify-between items-center">
        <div className="flex items-center">
          <FaFlask className={`w-8 h-8 ml-4 ${isCompleted ? 'text-gray-400' : 'text-purple-500'}`} />
          <div>
            <p className="font-bold text-gray-800">{order.type}</p>
            <div className="flex items-center text-sm text-gray-500 mt-1">
              <FaUser className="ml-2" />
              <span>{patient ? patient.name : 'مريض غير محدد'}</span>
            </div>
          </div>
        </div>
        <div className={`flex items-center px-3 py-1 rounded-full text-sm font-semibold ${isCompleted ? 'bg-green-100 text-green-800' : 'bg-yellow-100 text-yellow-800'}`}>
          {isCompleted ? <FaCheckCircle className="ml-2" /> : <FaHourglassHalf className="ml-2" />}
          <span>{isCompleted ? 'مكتمل' : 'قيد التنفيذ'}</span>
        </div>
      </div>
    </div>
  );
};

// المكون الرئيسي للتبويب (مع التصحيح)
const LabOrdersTab = ({ labOrders, patients, loading }) => {
  // --- بداية التصحيح ---
  // المنطق الصحيح: استخدم البيانات الوهمية فقط إذا انتهى التحميل والبيانات الحقيقية فارغة
  const useMockData = !loading && (!labOrders || labOrders.length === 0);
  const finalLabOrders = useMockData ? mockLabOrders : labOrders;
  const finalPatients = useMockData ? mockPatients : patients;
  // --- نهاية التصحيح ---

  return (
    <div className="p-4 sm:p-6">
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-2xl font-bold text-gray-800">🧪 طلبات التحاليل</h2>
        <button className="bg-teal-500 text-white px-4 py-2 rounded-lg flex items-center gap-2 hover:bg-teal-600">
          <FaPlus />
          <span>طلب جديد</span>
        </button>
      </div>
      
      {loading ? (
        <div className="space-y-4 animate-pulse">
          {[1, 2].map(i => <div key={i} className="h-20 bg-gray-200 rounded-xl"></div>)}
        </div>
      ) : finalLabOrders.length > 0 ? ( // استخدم finalLabOrders هنا
        <div className="space-y-4">
          {finalLabOrders.map(order => (
            <LabOrderCard 
              key={order.id} 
              order={order} 
              patient={(finalPatients || []).find(p => p.id === order.patientId)} 
            />
          ))}
        </div>
      ) : (
        <Empty text="لا توجد طلبات تحاليل حالياً." />
      )}
    </div>
  );
};

export default LabOrdersTab;
