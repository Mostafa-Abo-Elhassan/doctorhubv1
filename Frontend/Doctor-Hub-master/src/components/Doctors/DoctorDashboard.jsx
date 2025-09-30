import React, { useState, useEffect, useMemo } from 'react'; // <-- 1. استيراد useMemo
import axios from 'axios';
import { HiChevronLeft, HiChevronRight } from "react-icons/hi";

import DoctorSidebar from './DoctorSidebar';
import PatientsTab from './PatientsTab';
import DoctorAppointmentsTab from './DoctorAppointmentsTab';
import LabOrdersTab from './LabOrdersTab';
import DoctorNotificationsTab from './DoctorNotificationsTab';

const DoctorDashboard = () => {
  // 2. استخدم useMemo لضمان استقرار كائن currentUser
  const currentUser = useMemo(() => JSON.parse(localStorage.getItem('currentUser')), []);

  const [activeTab, setActiveTab] = useState('patients');
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [loading, setLoading] = useState(true);

  const [doctorData, setDoctorData] = useState({
    patients: [],
    appointments: [],
    labOrders: [],
    notifications: [],
  });

  // 3. الاعتماد على currentUser مباشرة أصبح الآن آمناً
  useEffect(() => {
    if (currentUser?.role === 'طبيب') {
      setLoading(true);
      
      Promise.all([
        axios.get(`http://DoctorHub.runasp.net/patients` ),
        axios.get(`http://DoctorHub.runasp.net/appointments?doctorId=${currentUser.id}` ),
        axios.get(`http://DoctorHub.runasp.net/labOrders?doctorId=${currentUser.id}` ),
        axios.get(`http://DoctorHub.runasp.net/notifications?userId=${currentUser.id}` ),
      ])
      .then(([patientsRes, appointmentsRes, labOrdersRes, notificationsRes]) => {
        setDoctorData({
          patients: patientsRes.data,
          appointments: appointmentsRes.data,
          labOrders: labOrdersRes.data,
          notifications: notificationsRes.data,
        });
      })
      .catch(err => console.error("Error loading doctor data:", err))
      .finally(() => setLoading(false));
    } else {
      setLoading(false);
    }
  }, [currentUser]); // <-- الاعتماد على currentUser الآن آمن بسبب useMemo

  const renderContent = () => {
    switch (activeTab) {
      case 'patients':
        return <PatientsTab patients={doctorData.patients} loading={loading} />;
      case 'appointments':
        return <DoctorAppointmentsTab appointments={doctorData.appointments} patients={doctorData.patients} loading={loading} />;
      case 'lab':
        return <LabOrdersTab labOrders={doctorData.labOrders} patients={doctorData.patients} loading={loading} />;
      case 'notifications':
        return <DoctorNotificationsTab notifications={doctorData.notifications} loading={loading} />;
      default:
        return <div className="p-8"><p className="text-gray-500">اختر قسماً من القائمة لعرضه.</p></div>;
    }
  };

  return (
    <div className="flex min-h-screen bg-slate-50">
      <button
        className="lg:hidden fixed top-1/2 right-0 z-50 p-2 rounded-l-md bg-white shadow-md transform -translate-y-1/2"
        onClick={() => setSidebarOpen(!sidebarOpen)}
      >
        {sidebarOpen ? <HiChevronRight className="w-6 h-6 text-teal-600" /> : <HiChevronLeft className="w-6 h-6 text-teal-600" />}
      </button>

      <aside className="hidden lg:block w-64">
        <DoctorSidebar user={currentUser} onTabChange={setActiveTab} activeTab={activeTab} />
      </aside>

      {sidebarOpen && (
        <div className="lg:hidden fixed inset-0 bg-black/30 z-40" onClick={() => setSidebarOpen(false)}>
          <div className="fixed top-0 right-0 h-full z-50 w-64 bg-white shadow-lg" onClick={(e) => e.stopPropagation()}>
            <DoctorSidebar user={currentUser} onTabChange={setActiveTab} onSidebarClose={() => setSidebarOpen(false)} activeTab={activeTab} />
          </div>
        </div>
      )}

      <main className="flex-1 lg:ml-64">
        {renderContent()}
      </main>
    </div>
  );
};

export default DoctorDashboard;
