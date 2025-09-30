import React from 'react';
import Card from '../../Pages/Patient/shared/Card';

const HistoryTab = ({ history }) => {
  // بيانات وهمية
  const mockHistory = [
    { id: 1, med: "Panadol", date: "2025-09-01", signedBy: "Pharmacist Ahmed" },
    { id: 2, med: "Amoxicillin", date: "2025-09-15", signedBy: "Pharmacist Ahmed" }
  ];

  const finalData = history?.length ? history : mockHistory;

  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold text-gray-800 mb-6">📝 سجل الصرف</h2>
      {finalData.map(h => (
        <Card key={h.id} title={h.med}>
          <p><strong>تاريخ:</strong> {h.date}</p>
          <p><strong>تم التوقيع بواسطة:</strong> {h.signedBy}</p>
        </Card>
      ))}
    </div>
  );
};

export default HistoryTab;
