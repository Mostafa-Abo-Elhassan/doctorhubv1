import React from 'react';
import Card from '../../Pages/Patient/shared/Card';
import Empty from '../../Pages/Patient/shared/Empty';

const PrescriptionTab = ({ prescriptions }) => {
  // بيانات وهمية
  const mockPrescriptions = [
    { id: 1, doctor: "د. كريم منصور", date: "2025-10-01", meds: ["Panadol", "Amoxicillin"] },
    { id: 2, doctor: "د. نادية سالم", date: "2025-09-15", meds: ["Vitamin D"] }
  ];

  const finalData = prescriptions?.length ? prescriptions : mockPrescriptions;

  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold text-gray-800 mb-6">💊 الروشتة الإلكترونية</h2>
      {finalData.length > 0 ? (
        finalData.map(r => (
          <Card key={r.id} title={`روشتة بتاريخ ${r.date}`}>
            <p><strong>الطبيب:</strong> {r.doctor}</p>
            <p><strong>الأدوية:</strong> {r.meds.join(", ")}</p>
          </Card>
        ))
      ) : <Empty text="لا توجد روشتات." />}
    </div>
  );
};

export default PrescriptionTab;
