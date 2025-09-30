import React, { useState } from 'react';
import Card from '../../Pages/Patient/shared/Card';

const AIDrugCheckTab = () => {
  const [meds, setMeds] = useState("");
  const [result, setResult] = useState(null);

  const handleCheck = () => {
    // هنا المفروض تنادي API الذكاء الاصطناعي
    // مؤقتاً هنعمل نتيجة وهمية
    if (meds.includes("Panadol") && meds.includes("Ibuprofen")) {
      setResult("⚠️ يوجد تداخل دوائي محتمل بين Panadol و Ibuprofen.");
    } else {
      setResult("✅ لا توجد تداخلات دوائية معروفة.");
    }
  };

  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold text-gray-800 mb-6">🤖 فحص AI للتداخلات</h2>
      <Card title="أدخل الأدوية للفحص">
        <textarea
          value={meds}
          onChange={(e) => setMeds(e.target.value)}
          className="border p-2 rounded w-full mb-3"
          placeholder="اكتب أسماء الأدوية مفصولة "
        />
        <button 
          onClick={handleCheck}
          className="bg-purple-600 text-white px-4 py-2 rounded"
        >
          فحص
        </button>
      </Card>

      {result && (
        <Card title="النتيجة">
          <p>{result}</p>
        </Card>
      )}
    </div>
  );
};

export default AIDrugCheckTab;
