import React, { useState } from 'react';
import Card from '../../Pages/Patient/shared/Card';

const DispenseTab = () => {
  const [med, setMed] = useState("");
  const [dispensed, setDispensed] = useState([]);

  const handleDispense = () => {
    if (!med) return;
    setDispensed([...dispensed, { name: med, date: new Date().toLocaleString() }]);
    setMed("");
  };

  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold text-gray-800 mb-6">📦 صرف الدواء</h2>
      <Card title="صرف دواء جديد">
        <input 
          type="text" 
          value={med} 
          onChange={(e) => setMed(e.target.value)} 
          placeholder="اسم الدواء" 
          className="border p-2 rounded w-full mb-3"
        />
        <button 
          onClick={handleDispense}
          className="bg-teal-600 text-white px-4 py-2 rounded"
        >
          صرف
        </button>
      </Card>

      <Card title="الأدوية التي تم صرفها">
        {dispensed.length > 0 ? (
          <ul className="list-disc pl-5">
            {dispensed.map((d, i) => (
              <li key={i}>{d.name} <span className="text-gray-500">({d.date})</span></li>
            ))}
          </ul>
        ) : <p className="text-gray-500">لم يتم صرف أي دواء بعد.</p>}
      </Card>
    </div>
  );
};

export default DispenseTab;
