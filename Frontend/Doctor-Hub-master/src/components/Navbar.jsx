import React, { useState, useEffect } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { Menu, X } from 'lucide-react';

const Navbar = () => {
  const navigate = useNavigate();
  const location = useLocation();
  
  const [isAuth, setIsAuth] = useState(false);
  const [userName, setUserName] = useState('');
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  // الروابط الأساسية
  const navLinks = [
    { name: 'الرئيسية', path: '/home' },
    { name: 'عن المنصة', path: '/about' },
    { name: 'الخدمات', path: '/services' },
    { name: 'مركز الدعم', path: '/support' },
  ];
  
  const filteredNavLinks = isAuth
    ? navLinks 
    : navLinks.filter(link => link.path === '/home'); 

  useEffect(() => {
    const checkAuth = () => {
      const authenticated = localStorage.getItem('isAuthenticated') === 'true';
      setIsAuth(authenticated);
      const user = authenticated ? JSON.parse(localStorage.getItem('currentUser')) : null;
      setUserName(user?.name || 'مستخدم');
    };
    checkAuth();
  }, [location.pathname]);

  const handleLogout = () => {
    localStorage.removeItem('isAuthenticated');
    localStorage.removeItem('currentUser');
    setIsAuth(false);
    setUserName('');
    navigate('/'); 
  };
  
  const toggleMenu = () => setIsMenuOpen(!isMenuOpen);

  const baseClasses = {
    navBg: 'bg-gray-800',
    textWhite: 'text-white',
    logoColor: 'text-teal-400',
    button: 'px-3 py-1.5 rounded-lg font-medium text-sm transition duration-300',
  };

  const AuthButtons = ({ isMobile = false }) => (
    <div className={`flex items-center gap-4 ${isMobile ? 'flex-col gap-0' : 'flex-row-reverse'}`}>
      {!isAuth ? (
        <>
          <Link 
            to="/login" 
            className={`w-full text-center ${baseClasses.button} text-white border border-white hover:bg-white hover:text-gray-800`}
            onClick={isMobile ? toggleMenu : null}
          >
            تسجيل الدخول
          </Link>
        </>
      ) : (
        <>
          <button
            onClick={handleLogout}
            className={`${baseClasses.button} bg-red-600 hover:bg-red-700 text-white`}
          >
            تسجيل الخروج
          </button>
          <div
            className="flex items-center gap-2 p-1.5 rounded-full bg-gray-700 cursor-pointer hover:bg-gray-600 transition"
            onClick={() => {
              const user = JSON.parse(localStorage.getItem('currentUser'));
              if (!user) return;

              switch (user.role) {
                case 'مريض':
                  navigate('/patient-dashboard');
                  break;
                case 'طبيب':
                  navigate('/doctor-dashboard');
                  break;
                case 'صيدلي':
                  navigate('/pharmacist-dashboard');
                  break;
                case 'معمل':
                  navigate('/lab-dashboard');
                  break;
                default:
                  navigate('/home');
              }
            }}
          >
            <div className="w-5 h-5 rounded-full bg-teal-400 flex items-center justify-center text-xs font-bold text-gray-800">
              {userName[0]}
            </div>
            <span className={`text-sm font-bold ${baseClasses.textWhite}`}>
              {`أهلاً، ${userName}`}
            </span>
          </div>
        </>
      )}
    </div>
  );
  
  return (
    <nav dir="rtl" className={`sticky top-0 z-50 ${baseClasses.navBg} shadow-lg`}>
      <div className="container mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between items-center h-16">
          
          {/* الشعار */}
          <div className="flex items-center">
            <Link to="#" className={`text-2xl font-extrabold ${baseClasses.textWhite}`}>
              <span className={baseClasses.logoColor}>Doctor</span> Hub
            </Link>
          </div>
          
          {/* روابط سطح المكتب */}
          <div className="hidden md:flex flex-1 justify-center">
            <div className="flex gap-8">
              {filteredNavLinks.map((link) => ( 
                <Link 
                  key={link.name} 
                  to={link.path} 
                  className={`text-sm font-medium ${baseClasses.textWhite} hover:text-teal-400 transition duration-150 whitespace-nowrap`}
                >
                  {link.name}
                </Link>
              ))}
            </div>
          </div>

          {/* أزرار المصادقة + القائمة للهاتف */}
          <div className="flex items-center gap-4">
            <div className="hidden md:block">
              <AuthButtons />
            </div>
            <button 
              onClick={toggleMenu} 
              className={`md:hidden ${baseClasses.textWhite} focus:outline-none p-2 rounded-lg hover:bg-gray-700 transition`}
            >
              {isMenuOpen ? <X size={24} /> : <Menu size={24} />}
            </button>
          </div>
        </div>
      </div>

      {/* القائمة المنسدلة للهاتف */}
      {isMenuOpen && (
        <div className={`md:hidden absolute w-full ${baseClasses.navBg} shadow-xl transform transition duration-300 ease-in-out`}>
          <div className="px-4 pt-4 pb-6 space-y-4">
            {filteredNavLinks.map((link) => (
              <Link
                key={link.name}
                to={link.path}
                className={`block px-3 py-2 rounded-md text-base font-medium ${baseClasses.textWhite} hover:bg-gray-700`}
                onClick={toggleMenu}
              >
                {link.name}
              </Link>
            ))}
            
            <div className="pt-4 border-t border-gray-700/50">
              <AuthButtons isMobile={true} />
            </div>
          </div>
        </div>
      )}
    </nav>
  );
};

export default Navbar;
