import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import AuthLayout from '../../components/Auth/AuthLayout';

const Register = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: '',
    role: '', // 'مريض', 'طبيب', 'صيدلي', 'معمل'
    nationalId: '',
    gender: '', // 'ذكر' أو 'أنثى'
  });
  const [error, setError] = useState('');

  const primaryColor = 'bg-teal-600 hover:bg-teal-700';
  const linkColor = 'text-teal-600 hover:text-teal-700';

  const roles = [
    { value: '', label: 'اختر دورك...' },
    { value: 'مريض', label: 'مريض (Patient)' },
    { value: 'طبيب', label: 'طبيب (Doctor)' },
    { value: 'صيدلي', label: 'صيدلي (Pharmacist)' },
    { value: 'معمل', label: 'معمل / أشعة (Lab / Radiology)' },
  ];

  const genders = [
    { value: '', label: 'اختر النوع...' },
    { value: 'ذكر', label: 'ذكر' },
    { value: 'أنثى', label: 'أنثى' },
  ];

  // 🔹 تحديث الحقول
  const handleChange = (e) => {
    const { name, value } = e.target;

    if (name === 'nationalId') {
      const numericValue = value.replace(/\D/g, '');
      if (numericValue.length <= 14) {
        setFormData({ ...formData, [name]: numericValue });
      }
    } else {
      setFormData({ ...formData, [name]: value });
    }
    setError('');
  };

  // 🔹 إرسال النموذج
  const handleSubmit = async (e) => {
    e.preventDefault();
    const { name, email, password, confirmPassword, role, nationalId, gender } = formData;

    // ✅ تحقق من البيانات
    if (password.length < 8) {
      setError('يجب أن تكون كلمة المرور 8 أحرف على الأقل.');
      return;
    }
    if (password !== confirmPassword) {
      setError('كلمة المرور وتأكيدها غير متطابقين.');
      return;
    }
    if (!role) {
      setError('الرجاء تحديد دورك للمتابعة.');
      return;
    }
    if (role === 'مريض') {
      if (nationalId.length !== 14 || !/^\d{14}$/.test(nationalId)) {
        setError('يجب أن يتكون الرقم القومي من 14 رقمًا بالضبط.');
        return;
      }
      if (!gender) {
        setError('الرجاء تحديد النوع (ذكر/أنثى).');
        return;
      }
    }

    try {
      // ✅ تحديد الجدول حسب الدور
      let endpoint = '';
      switch (role) {
        case 'مريض': endpoint = 'patients'; break;
        case 'طبيب': endpoint = 'doctors'; break;
        case 'صيدلي': endpoint = 'pharmacies'; break;
        case 'معمل': endpoint = 'labs'; break;
        default: endpoint = 'users';
      }

      // ✅ تحقق من البريد المكرر
      const existing = await axios.get(`http://DoctorHub.runasp.net/${endpoint}?email=${email}`);
      if (existing.data.length > 0) {
        setError('البريد الإلكتروني مستخدم بالفعل. جرّب تسجيل الدخول.');
        return;
      }

      // ✅ إنشاء المستخدم الجديد
      const newUser = {
        id: Date.now(), // رقم فريد
        name,
        email,
        password,
        role,
        ...(role === 'مريض' && { nationalId, gender })
      };

      await axios.post(`http://DoctorHub.runasp.net/${endpoint}`, newUser);

      console.log('✅ تم إنشاء الحساب بنجاح.');
      navigate('/login');
    } catch (err) {
      console.error(err);
      setError('حدث خطأ أثناء إنشاء الحساب. تأكد من تشغيل السيرفر.');
    }
  };

  return (
    <AuthLayout
      title="إنشاء حساب"
      subtitle="سجل بياناتك للانضمام إلى منصة Doctor Hub وبدء الثورة الصحية."
    >
      <form onSubmit={handleSubmit} className="space-y-4">
        {/* الاسم */}
        <div>
          <label htmlFor="name" className="block text-sm font-medium text-gray-700 text-right mb-1">
            الاسم الكامل
          </label>
          <input
            id="name"
            name="name"
            type="text"
            required
            value={formData.name}
            onChange={handleChange}
            className="w-full p-3 border border-gray-300 rounded-lg focus:ring-teal-500 focus:border-teal-500 transition duration-150 text-right"
            placeholder=" أدخل اسمك الكامل"
          />
        </div>

        {/* البريد */}
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

        {/* الدور */}
        <div>
          <label htmlFor="role" className="block text-sm font-medium text-gray-700 text-right mb-1">
            اختر دورك
          </label>
          <select
            id="role"
            name="role"
            required
            value={formData.role}
            onChange={handleChange}
            className="w-full p-3 border border-gray-300 rounded-lg focus:ring-teal-500 focus:border-teal-500 transition duration-150 text-right appearance-none bg-white pr-4"
          >
            {roles.map((r) => (
              <option key={r.value} value={r.value} disabled={r.value === ''}>
                {r.label}
              </option>
            ))}
          </select>
        </div>

        {/* الحقول الخاصة بالمريض */}
        {formData.role === 'مريض' && (
          <div className="space-y-4 pt-2">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label htmlFor="nationalId" className="block text-sm font-medium text-gray-700 text-right mb-1">
                  الرقم القومي (14 رقم)
                </label>
                <input
                  id="nationalId"
                  name="nationalId"
                  type="tel"
                  inputMode="numeric"
                  pattern="[0-9]{14}"
                  required={formData.role === 'مريض'}
                  value={formData.nationalId}
                  onChange={handleChange}
                  className="w-full p-3 border border-gray-300 rounded-lg focus:ring-teal-500 focus:border-teal-500 transition duration-150 text-right tracking-widest"
                  placeholder="00000000000000"
                  maxLength={14}
                  minLength={14}
                />
              </div>

              <div>
                <label htmlFor="gender" className="block text-sm font-medium text-gray-700 text-right mb-1">
                  النوع
                </label>
                <select
                  id="gender"
                  name="gender"
                  required={formData.role === 'مريض'}
                  value={formData.gender}
                  onChange={handleChange}
                  className="w-full p-3 border border-gray-300 rounded-lg focus:ring-teal-500 focus:border-teal-500 transition duration-150 text-right appearance-none bg-white pr-4"
                >
                  {genders.map((g) => (
                    <option key={g.value} value={g.value} disabled={g.value === ''}>
                      {g.label}
                    </option>
                  ))}
                </select>
              </div>
            </div>
          </div>
        )}

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

        <div>
          <label htmlFor="confirmPassword" className="block text-sm font-medium text-gray-700 text-right mb-1">
            تأكيد كلمة المرور
          </label>
          <input
            id="confirmPassword"
            name="confirmPassword"
            type="password"
            required
            value={formData.confirmPassword}
            onChange={handleChange}
            className="w-full p-3 border border-gray-300 rounded-lg focus:ring-teal-500 focus:border-teal-500 transition duration-150 text-right"
            placeholder="********"
          />
        </div>

        {/* رسالة الخطأ */}
        {error && <p className="text-sm text-red-600 text-right">{error}</p>}

        {/* زر الإنشاء */}
        <div>
          <button
            type="submit"
            className={`w-full flex justify-center py-3 px-4 rounded-lg shadow-sm text-sm font-medium text-white ${primaryColor} focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-teal-500 transition duration-200`}
          >
            إنشاء حساب جديد
          </button>
        </div>
      </form>

      <p className="mt-6 text-center text-sm text-gray-600">
        هل لديك حساب بالفعل؟{' '}
        <a href="/login" className={`font-medium ${linkColor}`}>
          تسجيل الدخول
        </a>
      </p>
    </AuthLayout>
  );
};

export default Register;
