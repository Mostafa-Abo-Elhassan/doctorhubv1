import logo from '../../assets/images/logo.jpg'; // استيراد الصورة من المجلد assets
const Sidebar = ({ onTabChange, onSidebarClose }) => {
  const currentUser = JSON.parse(localStorage.getItem('currentUser'));

  const handleTabClick = (tab) => {
    onTabChange(tab);
    if (onSidebarClose) {
      onSidebarClose();
    }
  };

  return (
    <div className="bg-white h-full p-6 w-64 mt-15">
      <h2 className="text-center  font-bold text-teal-600 mb-2"> أهلاً 👋{currentUser?.name}</h2>
      <p className='text-gray-600 mb-4 text-center font-bold'>{currentUser?.role}</p>
      <div className="mx-auto mb-4 w-30 h-30 rounded-full overflow-hidden flex items-center justify-center bg-white p-2">
                      <img 
                          src={logo} // ⬅️ استخدام الرابط البديل هنا
                          alt="Doctor Hub" 
                          className="w-full h-full object-cover rounded-full border-4 border-teal-400" 
                      />
                  </div>
      <ul className="space-y-4 ">
        <li><button onClick={() => handleTabClick('profile')} className="text-right w-full hover:text-teal-600">📂 الملف الطبي</button></li>
        <li><button onClick={() => handleTabClick('appointments')} className="text-right w-full hover:text-teal-600">📅 المواعيد</button></li>
        <li><button onClick={() => handleTabClick('notifications')} className="text-right w-full hover:text-teal-600">🔔 التنبيهات</button></li>
        <li><button onClick={() => handleTabClick('assistant')} className="text-right w-full hover:text-teal-600">🤖 المساعد الذكي</button></li>
        <li><button onClick={() => handleTabClick('child')} className="text-right w-full hover:text-teal-600">🍼 متابعة الطفل</button></li>
      </ul>
    </div>
  );
};

export default Sidebar;
