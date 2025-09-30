import { Route, Routes, Navigate } from "react-router-dom";
import Login from "./Pages/Auth/Login";
import Register from "./Pages/Auth/Register";
import NotFound from "./Pages/NotFound";
import HomePage from "./Pages/HomePage";
import AboutSection from "./components/sections/AboutSection";
import Navbar from "./components/Navbar";
import ServicesSection from "./components/sections/ServicesSection";
import SupportCenterSection from "./Pages/SupportCenterSection";
import FAQVideoSection from "./components/sections/FAQVideoSection";
import PatientDashboard from "./Pages/Patient/PatientDashboard";
import DoctorDashboard from './components/Doctors/DoctorDashboard';
import PharmacistDashboard from "./components/Pharmacy/PharmacyDashboard"
import LabDashboard from "./components/labResults/LabDashboard";
// ⬅️ مكون حماية المسارات (ProtectedRoute)
const ProtectedRoute = ({ element }) => {
  // التحقق من حالة المصادقة من localStorage
  const isAuth = localStorage.getItem('isAuthenticated') === 'true';

  // إذا لم يكن المستخدم مسجلاً، وجهه إلى صفحة التسجيل (/)
  if (!isAuth) {
    return <Navigate to="/" replace />; // ⬅️ تم تغيير التوجيه من /login إلى /
  }

  // إذا كان مسجلاً، اعرض المكون المطلوب
  return element;
};

const App = () => {
  return (
    <div className="App">
      
        <Navbar />
      <Routes>
        {/* مسارات المصادقة (متاحة دائماً) */}
        <Route path="/" element={<Register />} />
        <Route path="/login" element={<Login />} />
        
        {/* المسار الرئيسي (متاح دائماً للجميع) */}
        <Route path="/home" element={<HomePage />} />

        {/* المسارات المحمية: تتطلب تسجيل الدخول */}
        <Route path="/about" element={<ProtectedRoute element={<AboutSection />} />} />
        <Route path="/services" element={<ProtectedRoute element={<ServicesSection />} />} />
        <Route path="/contact" element={<ProtectedRoute element={<SupportCenterSection />} />} />
        <Route path="/Support" element={<ProtectedRoute element={<FAQVideoSection />} />} />
        {/* Dashboards لكل دور */}
        <Route path="/patient-dashboard" element={<ProtectedRoute element={<PatientDashboard />} />} />
        <Route path="/doctor-dashboard" element={<ProtectedRoute element={<DoctorDashboard />} />} />
        <Route path="/pharmacist-dashboard" element={<ProtectedRoute element={<PharmacistDashboard />} />} />
        <Route path="/lab-dashboard" element={<ProtectedRoute element={<LabDashboard />} />} />


        {/* مسار الصفحة غير موجودة */}
        <Route path="*" element={<div dir="rtl" className="text-center p-10 font-bold text-4xl"><NotFound /></div>} />
      </Routes>
      
    </div>
  );
};

export default App;