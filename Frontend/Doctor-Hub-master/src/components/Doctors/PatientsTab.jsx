import React, { useState } from 'react';
import Empty from '../../Pages/Patient/shared/Empty';
import { FaSearch, FaIdCard } from 'react-icons/fa';
import PatientDetailModal from './PatientDetailModal';

const PatientCard = ({ patient, onViewDetails }) => (
  <div className="bg-white rounded-xl shadow-md p-4 flex items-center justify-between transition-transform hover:transform hover:-translate-y-1">
    <div className="flex items-center">
      <img 
        src={`https://ui-avatars.com/api/?name=${patient.name.replace(' ', '+' )}`}
        alt={patient.name}
        className="w-14 h-14 rounded-full border-2 border-gray-200"
      />
      <div className="mr-4">
        <p className="font-bold text-gray-800">{patient.name}</p>
        <div className="flex items-center text-sm text-gray-500 mt-1">
          <FaIdCard className="ml-2" />
          <span>{patient.nationalId}</span>
        </div>
      </div>
    </div>
    <button 
      onClick={() => onViewDetails(patient)}
      className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 transition-colors"
    >
      عرض الملف
    </button>
  </div>
);

const PatientsTab = ({ patients, loading }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedPatient, setSelectedPatient] = useState(null);

  // --- بداية التصحيح ---
  const filteredPatients = (patients || []).filter(p => {
    const nameMatch = p.name.toLowerCase().includes(searchTerm.toLowerCase());
    // 1. تأكد من أن nationalId موجود وقم بتحويله إلى نص قبل البحث
    const idMatch = p.nationalId && p.nationalId.toString().includes(searchTerm);
    return nameMatch || idMatch;
  });
  // --- نهاية التصحيح ---

  return (
    <>
      <div className="p-4 sm:p-6">
        <h2 className="text-2xl font-bold text-gray-800 mb-6">👥 مرضاك</h2>
        
        <div className="relative mb-6">
          <input 
            type="text"
            placeholder="ابحث بالاسم أو الرقم القومي..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="w-full p-3 pr-10 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-teal-500"
          />
          <FaSearch className="absolute top-1/2 right-3 transform -translate-y-1/2 text-gray-400" />
        </div>

        {loading ? (
          <div className="space-y-4 animate-pulse">
            {[1, 2, 3].map(i => <div key={i} className="h-24 bg-gray-200 rounded-xl"></div>)}
          </div>
        ) : filteredPatients.length > 0 ? (
          <div className="space-y-4">
            {filteredPatients.map(patient => (
              <PatientCard key={patient.id} patient={patient} onViewDetails={setSelectedPatient} />
            ))}
          </div>
        ) : (
          <Empty text="لم يتم العثور على مرضى بهذا الاسم أو الرقم القومي." />
        )}
      </div>

      <PatientDetailModal patient={selectedPatient} onClose={() => setSelectedPatient(null)} />
    </>
  );
};

export default PatientsTab;
