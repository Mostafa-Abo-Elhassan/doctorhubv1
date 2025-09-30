import React from 'react';
import { FaUserInjured, FaCalendarCheck, FaFlask, FaBell, FaSignOutAlt } from 'react-icons/fa';

const sidebarLinks = [
  { id: 'patients', text: 'المرضى', icon: <FaUserInjured /> },
  { id: 'appointments', text: 'المواعيد', icon: <FaCalendarCheck /> },
  { id: 'lab', text: 'طلبات التحاليل', icon: <FaFlask /> },
  { id: 'notifications', text: 'التنبيهات', icon: <FaBell /> },
];

const DoctorSidebar = ({ onTabChange, onSidebarClose, activeTab, user }) => {
  const currentUser = user;
  const avatarUrl = `https://ui-avatars.com/api/?name=${currentUser?.name?.replace(' ', '+' ) || 'Doctor'}&background=14B8A6&color=fff&size=128`;

  const handleTabClick = (tab) => {
    onTabChange(tab);
    if (onSidebarClose) {
      onSidebarClose();
    }
  };

  return (
    <div className="bg-white h-full w-64 p-4 flex flex-col">
      <div className="flex flex-col items-center text-center mb-8">
        <img 
          src={avatarUrl} 
          alt="صورة الطبيب" 
          className="w-24 h-24 rounded-full border-4 border-teal-100 shadow-md mb-4"
        />
        <h2 className="font-bold text-xl text-teal-700">د. {currentUser?.name || 'طبيب'}</h2>
        <p className='text-gray-500 text-sm font-medium'>{currentUser?.role}</p>
      </div>

      <ul className="space-y-2 flex-grow">
        {sidebarLinks.map(link => {
          const isActive = activeTab === link.id;
          return (
            <li key={link.id}>
              <button 
                onClick={() => handleTabClick(link.id)} 
                className={`w-full flex items-center justify-end gap-3 p-3 rounded-lg text-right transition-colors duration-200 ${
                  isActive 
                    ? 'bg-teal-500 text-white shadow' 
                    : 'hover:bg-teal-50 hover:text-teal-600'
                }`}
              >
                <span>{link.text}</span>
                <span className="text-lg">{link.icon}</span>
              </button>
            </li>
          );
        })}
      </ul>

      <div className="mt-6">
        <button className="w-full flex items-center justify-end gap-3 p-3 rounded-lg text-right text-red-500 hover:bg-red-50">
          <span>تسجيل الخروج</span>
          <span className="text-lg"><FaSignOutAlt /></span>
        </button>
      </div>
    </div>
  );
};

export default DoctorSidebar;
