const Sidebar = ({ onTabChange, onSidebarClose }) => {
  const currentUser = JSON.parse(localStorage.getItem('currentUser'));
const avatarUrl = `https://ui-avatars.com/api/?name=${currentUser?.name?.replace(' ', '+' ) || 'patient'}&background=14B8A6&color=fff&size=128`;
  const handleTabClick = (tab) => {
    onTabChange(tab);
    if (onSidebarClose) onSidebarClose();
  };

  return (
    <div className="bg-white h-full p-6 w-64 mt-15">
      <div className="flex flex-col items-center text-center mb-8">
        <img 
          src={avatarUrl} 
          alt="صورة الطبيب" 
          className="w-24 h-24 rounded-full border-4 border-teal-100 shadow-md mb-4"
        />
        <h2 className="font-bold text-xl text-teal-700">د. {currentUser?.name || 'patient'}</h2>
        <p className='text-gray-500 text-sm font-medium'>{currentUser?.role}</p>
      </div>
     
     <ul className="space-y-4">
  <li>
    <button 
      onClick={() => handleTabClick('profile')} 
      className="w-full text-right hover:text-teal-600"
    >
      👤 بروفايل المريض
    </button>
  </li>
  <li><button onClick={() => handleTabClick('prescriptions')} className="w-full text-right hover:text-teal-600">💊 الروشتة الإلكترونية</button></li>
  <li><button onClick={() => handleTabClick('dispense')} className="w-full text-right hover:text-teal-600">📦 صرف الدواء</button></li>
  <li><button onClick={() => handleTabClick('history')} className="w-full text-right hover:text-teal-600">📝 سجل الصرف</button></li>
  <li><button onClick={() => handleTabClick('ai-check')} className="w-full text-right hover:text-teal-600">🤖 فحص AI للتداخلات</button></li>
</ul>

    </div>
  );
};

export default Sidebar;
