import React, { useState } from 'react';
import Card from '../../Pages/Patient/shared/Card';

const EmergencyTab = () => {
  const [nationalId, setNationalId] = useState("");
  const [patient, setPatient] = useState(null);

  const handleSearch = async () => {
    // هنا هتعمل call للـ API
    // مؤقتاً: بيانات وهمية
    if (nationalId === "123456789") {
      setPatient({
        name: "أحمد محمد",
        chronic: ["ضغط دم", "سكر"],
        meds: ["Metformin", "Amlodipine"],
        allergies: ["بنسلين"]
      });
    } else {
      setPatient(null);
    }
  };

  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold mb-6">🚑 الطوارئ</h2>
      <Card title="ابحث بالرقم القومي">
        <input 
          value={nationalId}
          onChange={(e) => setNationalId(e.target.value)}
          placeholder="أدخل الرقم القومي"
          className="border p-2 rounded w-full mb-3"
        />
        <button onClick={handleSearch} className="bg-blue-600 text-white px-4 py-2 rounded">
          بحث
        </button>
      </Card>

      {patient && (
        <Card title="ملف الطوارئ">
          <p><strong>الاسم:</strong> {patient.name}</p>
          <p><strong>أمراض مزمنة:</strong> {patient.chronic.join(", ")}</p>
          <p><strong>أدوية:</strong> {patient.meds.join(", ")}</p>
          <p><strong>حساسية:</strong> {patient.allergies.join(", ")}</p>
        </Card>
      )}
    </div>
  );
};

export default EmergencyTab;
