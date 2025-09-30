import React from 'react';
import Card from '../../Pages/Patient/shared/Card';

const InsuranceTab = () => {
  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold mb-6">💳 التأمين الصحي</h2>
      <Card title="التكامل مع التأمين">
        <p>يمكن ربط الحسابات مباشرة مع شركات التأمين لتسهيل عمليات الدفع والمراجعة.</p>
      </Card>
    </div>
  );
};

export default InsuranceTab;
