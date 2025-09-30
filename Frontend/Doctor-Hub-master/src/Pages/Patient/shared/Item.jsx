import React from 'react';

const Item = ({ title, subtitle }) => (
  <div className="p-3 bg-gray-100 rounded-md">
    <p className="font-medium">{title}</p>
    <p className="text-sm text-gray-600">{subtitle}</p>
  </div>
);

export default Item;
