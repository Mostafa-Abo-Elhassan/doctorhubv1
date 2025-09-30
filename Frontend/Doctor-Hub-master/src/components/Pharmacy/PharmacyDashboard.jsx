import React, { useState } from 'react';
import { HiChevronLeft, HiChevronRight } from "react-icons/hi";

import Sidebar from './Sidebar';
import PatientProfileTab from './PatientProfileTab';
import PrescriptionTab from './PrescriptionTab';
import DispenseTab from './DispenseTab';
import HistoryTab from './HistoryTab';
import AIDrugCheckTab from './AIDrugCheckTab';

const PharmacistDashboard = () => {
  const currentUser = JSON.parse(localStorage.getItem('currentUser'));
  const [activeTab, setActiveTab] = useState('profile');
  const [sidebarOpen, setSidebarOpen] = useState(false);

  const renderContent = () => {
    switch (activeTab) {
      case 'profile':
        return <PatientProfileTab />;
      case 'prescriptions':
        return <PrescriptionTab />;
      case 'dispense':
        return <DispenseTab />;
      case 'history':
        return <HistoryTab />;
      case 'ai-check':
        return <AIDrugCheckTab />;
      default:
        return <p className="text-gray-500">اختر قسم من القائمة</p>;
    }
  };

  return (
    <div className="flex min-h-screen bg-slate-50">
      {/* زرار Sidebar في الموبايل */}
      <button
        className="sm:hidden fixed top-1/2 right-0 z-50 p-2 rounded-l-md bg-white shadow-md transform -translate-y-1/2"
        onClick={() => setSidebarOpen(!sidebarOpen)}
      >
        {sidebarOpen
          ? <HiChevronRight className="w-6 h-6 text-teal-600" />
          : <HiChevronLeft className="w-6 h-6 text-teal-600" />}
      </button>

      {/* Sidebar للشاشات الكبيرة */}
      <aside className="hidden sm:block w-64">
        <Sidebar onTabChange={setActiveTab} />
      </aside>

      {/* Sidebar في الموبايل */}
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

export default PharmacistDashboard;
