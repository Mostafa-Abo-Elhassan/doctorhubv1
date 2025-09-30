import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { HiChevronLeft, HiChevronRight } from "react-icons/hi";

// --- المكونات ---
import Sidebar from './Sidebar';
import ProfileTab from './ProfileTab';
import AppointmentsTab from './AppointmentsTab';
import ChildTab from './ChildTab';
import NotificationsTab from './NotificationsTab';
import AssistantTab from './AssistantTab';

const PatientDashboard = () => {
  const currentUser = JSON.parse(localStorage.getItem('currentUser'));

  const [activeTab, setActiveTab] = useState('profile');
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [loading, setLoading] = useState(true);

  const [patientData, setPatientData] = useState({
    appointments: [],
    doctors: [],
    prescriptions: [],
    labResults: [],
    vaccines: [],
    notifications: [],
    childData: null,
  });

  useEffect(() => {
  if (!currentUser || currentUser.role !== 'مريض') {
    setLoading(false);
    return;
  }

  const fetchData = async () => {
    try {
      setLoading(true);
      const [apps, docs, pres, labs, vacc, notifs, child] = await Promise.all([
        axios.get(`http://DoctorHub.runasp.net/appointments?patientId=${currentUser.id}`),
        axios.get(`http://DoctorHub.runasp.net/doctors`),
        axios.get(`http://DoctorHub.runasp.net/prescriptions?patientId=${currentUser.id}`),
        axios.get(`http://DoctorHub.runasp.net/labResults?patientId=${currentUser.id}`),
        axios.get(`http://DoctorHub.runasp.net/vaccines?patientId=${currentUser.id}`),
        axios.get(`http://DoctorHub.runasp.net/notifications?patientId=${currentUser.id}`),
        axios.get(`http://DoctorHub.runasp.net/childData?patientId=${currentUser.id}`),
      ]);

      setPatientData({
        appointments: apps.data,
        doctors: docs.data,
        prescriptions: pres.data,
        labResults: labs.data,
        vaccines: vacc.data,
        notifications: notifs.data,
        childData: child.data[0] || null,
      });
    } catch (err) {
      console.error("Error loading patient data:", err);
    } finally {
      setLoading(false);
    }
  };

  fetchData();
}, [currentUser?.id, currentUser?.role]); // ✅ ثابت دايمًا
 // ⬅️ مفيش loop


  // --- التبويبات ---
  const renderContent = () => {
    switch (activeTab) {
      case 'profile':
        return (
          <ProfileTab
            prescriptions={patientData.prescriptions}
            labResults={patientData.labResults}
            vaccines={patientData.vaccines}
            loading={loading}
          />
        );
      case 'appointments':
        return (
          <AppointmentsTab
            appointments={patientData.appointments}
            doctors={patientData.doctors}
            loading={loading}
          />
        );
      case 'notifications':
        return (
          <NotificationsTab
            notifications={patientData.notifications}
            loading={loading}
          />
        );
      case 'child':
        return (
          <ChildTab
            childData={patientData.childData}
            loading={loading}
          />
        );
      case 'assistant':
        return <AssistantTab />;
      default:
        return <p className="text-gray-500">اختر قسم من القائمة</p>;
    }
  };

  return (
    <div className="flex min-h-screen bg-slate-50">
      {/* زرار فتح/غلق الـ Sidebar في الموبايل */}
      <button
        className="sm:hidden fixed top-1/2 right-0 z-50 p-2 rounded-l-md bg-white shadow-md transform -translate-y-1/2"
        onClick={() => setSidebarOpen(!sidebarOpen)}
      >
        {sidebarOpen
          ? <HiChevronRight className="w-6 h-6 text-teal-600" />
          : <HiChevronLeft className="w-6 h-6 text-teal-600" />}
      </button>

      {/* الـ Sidebar في الشاشات الكبيرة */}
      <aside className="hidden sm:block w-64">
        <Sidebar onTabChange={setActiveTab} />
      </aside>

      {/* الـ Sidebar في الموبايل */}
      {sidebarOpen && (
        <div
          className="sm:hidden fixed inset-0 bg-black/30 z-40"
          onClick={() => setSidebarOpen(false)}
        >
          <div
            className="fixed top-0 right-0 h-full z-50 w-64 bg-white shadow-lg"
            onClick={(e) => e.stopPropagation()}
          >
            <Sidebar
              onTabChange={setActiveTab}
              onSidebarClose={() => setSidebarOpen(false)}
            />
          </div>
        </div>
      )}

      {/* المحتوى */}
      <main className="flex-1 lg:ml-64">
        {renderContent()}
      </main>
    </div>
  );
};

export default PatientDashboard;
