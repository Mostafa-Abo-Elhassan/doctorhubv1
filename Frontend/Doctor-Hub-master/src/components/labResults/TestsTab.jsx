import React from 'react';
import Card from '../../Pages/Patient/shared/Card';

const TestsTab = () => {
  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold mb-6">📋 الفحوصات والأشعة</h2>
      <Card title="تقليل التكرار">
        <p>يمكن ربط النظام بالتحاليل والأشعة السابقة لتفادي تكرار نفس الفحوصات.</p>
      </Card>
    </div>
  );
};

export default TestsTab;
