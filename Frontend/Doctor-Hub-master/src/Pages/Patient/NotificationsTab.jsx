import React, { useState } from 'react';
import Empty from './shared/Empty';
import { FaBell, FaFileMedical, FaFlask, FaRegCheckCircle, FaRegClock } from 'react-icons/fa';

// --- بيانات وهمية للعرض ---
const mockNotifications = [
  { id: 'n1', title: 'تذكير بموعد', message: 'لديك موعد متابعة مع د. أمينة خليل غداً الساعة 2:00 مساءً.', date: '2025-10-14T18:00:00Z', read: false, type: 'appointment' },
  { id: 'n2', title: 'نتائج التحليل متوفرة', message: 'نتائج تحليل الدم الشامل أصبحت متوفرة الآن.', date: '2024-08-25T11:00:00Z', read: true, type: 'lab_result' },
  { id: 'n3', title: 'روشتة جديدة', message: 'قام د. كريم منصور بإصدار روشتة جديدة لك.', date: '2024-09-18T13:00:00Z', read: true, type: 'prescription' },
  { id: 'n4', title: 'تم تأكيد الحجز', message: 'تم تأكيد حجزك مع د. سارة العوضي بنجاح.', date: '2025-10-01T10:00:00Z', read: false, type: 'appointment' },
];
// --- نهاية البيانات الوهمية ---

// مكون بطاقة التنبيه (يمكن فصله في ملف خاص إذا أردت)
const NotificationCard = ({ notification }) => {
  const getIcon = () => {
    const props = { className: "w-6 h-6" };
    switch (notification.type) {
      case 'appointment': return <FaBell {...props} className={`${props.className} text-blue-500`} />;
      case 'prescription': return <FaFileMedical {...props} className={`${props.className} text-green-500`} />;
      case 'lab_result': return <FaFlask {...props} className={`${props.className} text-purple-500`} />;
      default: return <FaBell {...props} className={`${props.className} text-gray-500`} />;
    }
  };

  return (
    <div className={`flex items-start p-4 rounded-xl transition-all duration-300 ${notification.read ? 'bg-gray-50' : 'bg-white shadow-md'}`}>
      <div className={`flex-shrink-0 mr-4 mt-1 p-3 rounded-full ${notification.read ? 'bg-gray-200' : 'bg-blue-100'}`}>
        {getIcon()}
      </div>
      <div className="flex-grow">
        <h3 className={`font-bold ${notification.read ? 'text-gray-600' : 'text-gray-800'}`}>{notification.title}</h3>
        <p className="text-sm text-gray-600 mt-1">{notification.message}</p>
        <div className="flex items-center text-xs text-gray-400 mt-2">
          {notification.read ? <FaRegCheckCircle className="ml-1" /> : <FaRegClock className="ml-1" />}
          <span>{new Date(notification.date).toLocaleDateString('ar-EG', { day: 'numeric', month: 'short', year: 'numeric' })}</span>
        </div>
      </div>
      {!notification.read && <span className="w-2.5 h-2.5 bg-blue-500 rounded-full flex-shrink-0 mt-1 mr-2" aria-label="غير مقروء"></span>}
    </div>
  );
};


const NotificationsTab = ({ notifications, loading }) => {
  const [filter, setFilter] = useState('all'); // 'all' or 'unread'

  const useMockData = !loading && (!notifications || notifications.length === 0);
  const finalNotifications = useMockData ? mockNotifications : notifications;

  const filteredNotifications = finalNotifications.filter(n => {
    if (filter === 'unread') return !n.read;
    return true;
  });

  return (
    <div className="p-4 sm:p-6">
      <div className="flex flex-col sm:flex-row justify-between items-center mb-6 gap-4">
        <h2 className="text-2xl font-bold text-gray-800 self-start sm:self-center">🔔 التنبيهات</h2>
        <div className="flex items-center self-end sm:self-center space-x-1 bg-gray-100 p-1 rounded-lg">
          <button onClick={() => setFilter('all')} className={`px-4 py-1.5 rounded-md text-sm font-semibold transition-colors ${filter === 'all' ? 'bg-white text-blue-600 shadow-sm' : 'text-gray-600'}`}>الكل</button>
          <button onClick={() => setFilter('unread')} className={`px-4 py-1.5 rounded-md text-sm font-semibold transition-colors ${filter === 'unread' ? 'bg-white text-blue-600 shadow-sm' : 'text-gray-600'}`}>الجديدة</button>
        </div>
      </div>

      {loading ? (
        <div className="space-y-4 animate-pulse">
          {[1, 2, 3].map(i => <div key={i} className="h-24 bg-gray-200 rounded-xl"></div>)}
        </div>
      ) : filteredNotifications.length > 0 ? (
        <div className="space-y-4">
          {filteredNotifications.map(notification => (
            <NotificationCard key={notification.id} notification={notification} />
          ))}
        </div>
      ) : (
        <Empty text="لا توجد تنبيهات لعرضها." />
      )}
    </div>
  );
};

export default NotificationsTab;
