import React from 'react';
import Empty from '../../Pages/Patient/shared/Empty';
import { FaBell, FaUserPlus, FaFileMedical } from 'react-icons/fa';

// --- البيانات الوهمية ---
const mockNotifications = [
  { id: 'mock-n1', type: 'lab_result', message: 'نتائج تحليل الدم للمريض أحمد علي أصبحت متاحة.', date: '2025-09-27', read: false },
  { id: 'mock-n2', type: 'new_patient', message: 'تم تسجيل مريض جديد لك: منى حسن', date: '2025-09-28', read: true },
  { id: 'mock-n3', type: 'appointment_reminder', message: 'تذكير: لديك موعد مع أحمد عبد الجواد غداً.', date: '2025-10-14', read: false },
];
// --- نهاية البيانات الوهمية ---

// مكون بطاقة التنبيه (لا يتغير)
const NotificationCard = ({ notification }) => {
  const getIcon = () => {
    switch (notification.type) {
      case 'new_patient': return <FaUserPlus className="text-blue-500" />;
      case 'lab_result': return <FaFileMedical className="text-green-500" />;
      default: return <FaBell className="text-gray-500" />;
    }
  };
  return (
    <div className={`flex items-start p-4 rounded-xl ${notification.read ? 'bg-gray-50' : 'bg-white shadow-md'}`}>
      <div className={`flex-shrink-0 mr-4 mt-1 p-3 rounded-full ${notification.read ? 'bg-gray-200' : 'bg-blue-100'}`}>
        {getIcon()}
      </div>
      <div>
        <p className={`text-sm ${notification.read ? 'text-gray-600' : 'text-gray-800 font-semibold'}`}>{notification.message}</p>
        <p className="text-xs text-gray-400 mt-1">{new Date(notification.date).toLocaleDateString('ar-EG', { day: 'numeric', month: 'short' })}</p>
      </div>
    </div>
  );
};

// المكون الرئيسي للتبويب (مع التصحيح)
const DoctorNotificationsTab = ({ notifications, loading }) => {
  // --- بداية التصحيح ---
  // المنطق الصحيح: استخدم البيانات الوهمية فقط إذا انتهى التحميل والبيانات الحقيقية فارغة
  const useMockData = !loading && (!notifications || notifications.length === 0);
  const finalNotifications = useMockData ? mockNotifications : notifications;
  // --- نهاية التصحيح ---

  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold text-gray-800 mb-6">🔔 التنبيهات</h2>
      {loading ? (
        <div className="space-y-4 animate-pulse">
          {[1, 2, 3].map(i => <div key={i} className="h-20 bg-gray-200 rounded-xl"></div>)}
        </div>
      ) : finalNotifications.length > 0 ? ( // استخدم finalNotifications هنا
        <div className="space-y-4">
          {finalNotifications.map(n => <NotificationCard key={n.id} notification={n} />)}
        </div>
      ) : (
        <Empty text="لا توجد تنبيهات جديدة." />
      )}
    </div>
  );
};

export default DoctorNotificationsTab;
