import React from 'react';

const TableHead: React.FC = () => {
  return (
    <thead>
      <tr className="text-base">
        <th className="px-6 align-middle border border-solid py-3 uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left bg-gray-100 text-gray-600 border-gray-200">
          Type
        </th>
        <th className="px-6 align-middle border border-solid py-3 uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left bg-gray-100 text-gray-600 border-gray-200">
          Name
        </th>
        <th className="px-6 align-middle border border-solid py-3 uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left bg-gray-100 text-gray-600 border-gray-200">
          Due
        </th>
        <th className="px-6 align-middle border border-solid py-3 uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left bg-gray-100 text-gray-600 border-gray-200">
          Placed
        </th>
        <th className="px-6 align-middle border border-solid py-3 uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left bg-gray-100 text-gray-600 border-gray-200">
          Total
        </th>
      </tr>
    </thead>
  );
};

export default TableHead;
