const Sidebar = ({ onTabChange, onSidebarClose }) => {
  const currentUser = JSON.parse(localStorage.getItem('currentUser'));
const avatarUrl = `https://ui-avatars.com/api/?name=${currentUser?.name?.replace(' ', '+' ) || 'patient'}&background=14B8A6&color=fff&size=128`;
  const handleTabClick = (tab) => {
    onTabChange(tab);
    if (onSidebarClose) onSidebarClose();
  };

  return (
    <div className="bg-white h-full p-6 w-64">
        <div className="flex flex-col items-center text-center mb-8">
        <img 
          src={avatarUrl} 
          alt="صورة الطبيب" 
          className="w-24 h-24 rounded-full border-4 border-teal-100 shadow-md mb-4"
        />
        <h2 className="font-bold text-xl text-teal-700">د. {currentUser?.name || 'patient'}</h2>
        <p className='text-gray-500 text-sm font-medium'>{currentUser?.role}</p>
      </div>
      <h2 className="text-center font-bold text-teal-600 mb-4">لوحة تحكم المعمل 🧪</h2>
      <ul className="space-y-4">
        <li><button onClick={() => handleTabClick('emergency')} className="w-full text-right hover:text-teal-600">🚑 الطوارئ</button></li>
        <li><button onClick={() => handleTabClick('tests')} className="w-full text-right hover:text-teal-600">📋 الفحوصات والأشعة</button></li>
        <li><button onClick={() => handleTabClick('insurance')} className="w-full text-right hover:text-teal-600">💳 التأمين الصحي</button></li>
        <li><button onClick={() => handleTabClick('access')} className="w-full text-right hover:text-teal-600">🔐 صلاحيات الوصول</button></li>
      </ul>
    </div>
  );
};

export default Sidebar;
