import React from 'react';

const Card = ({ title, children }) => (
  <div className="bg-white rounded-lg shadow-lg p-6 mb-6 hover:shadow-xl transition">
    <h2 className="text-xl font-semibold text-teal-700 mb-4">{title}</h2>
    <div className="space-y-2">{children}</div>
  </div>
);

export default Card;
