import React from 'react';
import Card from '../../Pages/Patient/shared/Card';

const AccessControlTab = () => {
  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold mb-6">🔐 نظام الصلاحيات</h2>
      <Card title="View Glass - Break">
        <p>في الحالات الحرجة، يمكن للصلاحيات أن تُفتح مؤقتاً للوصول السريع لملف المريض.</p>
      </Card>
    </div>
  );
};

export default AccessControlTab;
