import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Card from '../../Pages/Patient/shared/Card';
import Empty from '../../Pages/Patient/shared/Empty';

const PatientProfileTab = () => {
  const [patients, setPatients] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchPatients = async () => {
      try {
        setLoading(true);
        const res = await axios.get("http://DoctorHub.runasp.net/patients");
        setPatients(res.data);
      } catch (err) {
        console.error("Error fetching patients:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchPatients();
  }, []);

  return (
    <div className="p-4 sm:p-6">
      <h2 className="text-2xl font-bold text-gray-800 mb-6">👤 المرضى</h2>

      {loading ? (
        <p className="text-gray-500">جاري تحميل البيانات...</p>
      ) : patients.length > 0 ? (
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {patients.map((p) => (
            <Card key={p.id} title={p.name}>
              <p><strong>العمر:</strong> {p.age} سنة</p>
              <p><strong>النوع:</strong> {p.gender}</p>
              <p><strong>الحساسيات:</strong> {p.allergies?.length ? p.allergies.join(", ") : "لا توجد"}</p>
            </Card>
          ))}
        </div>
      ) : (
        <Empty text="لا يوجد مرضى مسجلين." />
      )}
    </div>
  );
};

export default PatientProfileTab;
