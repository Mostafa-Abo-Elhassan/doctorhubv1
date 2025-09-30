import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import AuthLayout from '../../components/Auth/AuthLayout';

const Login = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({ email: '', password: '' });
  const [error, setError] = useState('');

  const primaryColor = 'bg-teal-600 hover:bg-teal-700';
  const linkColor = 'text-teal-600 hover:text-teal-700';

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
    setError('');
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const { email, password } = formData;

    try {
      const endpoints = ['patients', 'doctors', 'pharmacies', 'labs'];
      let user = null;
      let role = '';

      // البحث في جميع الجداول
      for (const ep of endpoints) {
        const res = await axios.get(`http://DoctorHub.runasp.net/${ep}`);
        const found = res.data.find(
          (u) => u.email && u.email.toLowerCase().trim() === email.toLowerCase().trim()
        );
        if (found) {
          user = found;
          switch (ep) {
            case 'patients': role = 'مريض'; break;
            case 'doctors': role = 'طبيب'; break;
            case 'pharmacies': role = 'صيدلي'; break;
            case 'labs': role = 'معمل'; break;
            default: role = 'مستخدم';
          }
          break;
        }
      }

      // لو المستخدم مش موجود
      if (!user || !user.password) {
        setError('هذا البريد الإلكتروني غير مسجل أو البيانات غير مكتملة.');
        return;
      }

      // التحقق من الباسورد
      if (String(user.password).trim() !== String(password).trim()) {
        setError('كلمة المرور غير صحيحة.');
        return;
      }

      // تسجيل دخول ناجح
      localStorage.setItem('isAuthenticated', 'true');
      localStorage.setItem(
        'currentUser',
        JSON.stringify({
          id: Number(user.id), // تخزين id كرقم
          email: user.email,
          name: user.name,
          role: role,
        })
      );

      // التوجيه حسب الدور
      switch (role) {
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
    } catch (err) {
      console.error(err);
      setError('حدث خطأ أثناء تسجيل الدخول. تأكد من تشغيل السيرفر.');
    }
  };

  return (
    <AuthLayout
      title="تسجيل الدخول"
      subtitle="أدخل بياناتك للمتابعة إلى منصة Doctor Hub."
    >
      <form onSubmit={handleSubmit} className="space-y-6">
        {/* البريد الإلكتروني */}
        <div>
          <label htmlFor="email" className="block text-sm font-medium text-gray-700 text-right mb-1">
            البريد الإلكتروني
          </label>
          <input
            id="email"
            name="email"
            type="email"
            required
            value={formData.email}
            onChange={handleChange}
            className="w-full p-3 border border-gray-300 rounded-lg focus:ring-teal-500 focus:border-teal-500 transition duration-150 text-right"
            placeholder="ادخل بريدك الإلكتروني"
          />
        </div>

        {/* كلمة المرور */}
        <div>
          <label htmlFor="password" className="block text-sm font-medium text-gray-700 text-right mb-1">
            كلمة المرور
          </label>
          <input
            id="password"
            name="password"
            type="password"
            required
            value={formData.password}
            onChange={handleChange}
            className="w-full p-3 border border-gray-300 rounded-lg focus:ring-teal-500 focus:border-teal-500 transition duration-150 text-right"
            placeholder="********"
          />
        </div>

        {/* رسالة الخطأ */}
        {error && <p className="text-sm text-red-600 text-right">{error}</p>}

        {/* تذكرني + نسيت كلمة المرور */}
        <div className="flex items-center justify-between">
          <div className="flex items-center">
            <input
              id="remember-me"
              name="remember-me"
              type="checkbox"
              className="h-4 w-4 text-teal-600 border-gray-300 rounded focus:ring-teal-500 ml-2"
            />
            <label htmlFor="remember-me" className="text-sm text-gray-900 select-none">
              تذكرني
            </label>
          </div>
          <div className="text-sm">
            <a href="#" className={`font-medium ${linkColor}`}>
              نسيت كلمة المرور؟
            </a>
          </div>
        </div>

        {/* زر تسجيل الدخول */}
        <div>
          <button
            type="submit"
            className={`w-full flex justify-center py-3 px-4 rounded-lg shadow-sm text-sm font-medium text-white ${primaryColor} focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-teal-500 transition duration-200`}
            data-aos="fade-up"
          >
            تسجيل الدخول
          </button>
        </div>
      </form>

      {/* رابط إنشاء حساب */}
      <p className="mt-8 text-center text-sm text-gray-600">
        هل أنت مستخدم جديد؟{' '}
        <a href="/" className={`font-medium ${linkColor}`}>
          إنشاء حساب جديد
        </a>
      </p>
    </AuthLayout>
  );
};

export default Login;
