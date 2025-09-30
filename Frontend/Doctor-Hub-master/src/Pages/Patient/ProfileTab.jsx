import React from 'react';
import Card from './shared/Card';
import ProfileItem from './shared/ProfileItem';
import Empty from './shared/Empty';

// --- بيانات وهمية ---
const mockProfileData = {
  prescriptions: [
    { id: 'p1', date: '10 أكتوبر 2025', doctorName: 'د. كريم منصور', medications: [{ name: 'Amoxicillin 500mg' }, { name: 'Vitamin C' }] },
    { id: 'p2', date: '20 أكتوبر 2025', doctorName: 'د. نادية سالم', medications: [{ name: 'Panadol 250mg' }] },
  ],
  labResults: [
    { id: 'l1', result: 'تحليل دم شامل (CBC)', uploadedAt: '25 نوفمبر 2025', labName: 'مختبر البرج' },
    { id: 'l2', result: 'تحليل فيتامين د', uploadedAt: '10 نوفمبر 2025', labName: 'مختبر ألفا' },
  ],
  vaccines: [
    { id: 'v1', name: 'لقاح الإنفلونزا الموسمية', date: '14 أكتوبر 2025' },
    { id: 'v2', name: 'جرعة تنشيطية - شلل الأطفال', date: '4 أكتوبر 2025' },
    { id: 'v3', name: 'لقاح الحصبة (MMR)', date: '3 ديسمبر 2025' },
  ]
};
// --- نهاية البيانات الوهمية ---

const ProfileTab = ({ prescriptions, labResults, vaccines }) => {
  const useMockData = !prescriptions?.length && !labResults?.length && !vaccines?.length;

  const finalPrescriptions = useMockData ? mockProfileData.prescriptions : prescriptions;
  const finalLabResults = useMockData ? mockProfileData.labResults : labResults;
  const finalVaccines = useMockData ? mockProfileData.vaccines : vaccines;

  return (
    <div className="p-4 sm:p-6 w-full overflow-x-hidden"> {/* منع الاسكرول الأفقي */}
      <h2 className="text-2xl font-bold text-gray-800 mb-6">📂 الملف الطبي</h2>

      {/* الروشتات */}
      <Card title="💊 الروشتات">
        {finalPrescriptions.length > 0 ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            {finalPrescriptions.map(p => (
              <ProfileItem
                key={p.id}
                type="prescription"
                title={(p.medications || []).map(m => m.name).join(', ')}
                subtitle={`من طرف: ${p.doctorName || 'طبيب'}`}
                date={p.date}
                className="break-words"  // يكسر النص الطويل
              />
            ))}
          </div>
        ) : <Empty text="لا توجد روشتات في سجلك." />}
      </Card>

      {/* التحاليل */}
      <Card title="🧪 التحاليل">
        {finalLabResults.length > 0 ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            {finalLabResults.map(r => (
              <ProfileItem
                key={r.id}
                type="labResult"
                title={r.result}
                subtitle={`من: ${r.labName || 'معمل'}`}
                date={r.uploadedAt}
                className="break-words"
              />
            ))}
          </div>
        ) : <Empty text="لا توجد نتائج تحاليل." />}
      </Card>

      {/* التطعيمات */}
      <Card title="💉 التطعيمات">
        {finalVaccines.length > 0 ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            {finalVaccines.map(v => (
              <ProfileItem
                key={v.id}
                type="vaccine"
                title={v.name}
                subtitle="تطعيم مكتمل"
                date={v.date}
                className="break-words"
              />
            ))}
          </div>
        ) : <Empty text="لا توجد تطعيمات مسجلة." />}
      </Card>
    </div>
  );
};

export default ProfileTab;
