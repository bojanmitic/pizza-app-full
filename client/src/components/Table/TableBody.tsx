import React from 'react';

const TableBody: React.FC = () => {
  return (
    <tbody className="bg-white">
      <tr className="text-sm">
        <td className="border-t-0 px-6 align-middle border-l-0 border-r-0 whitespace-no-wrap p-4 text-left flex items-center">
          Argon Design System
        </td>
        <td className="border-t-0 px-6 align-middle border-l-0 border-r-0 whitespace-no-wrap p-4">
          $2,500 USD
        </td>
        <td className="border-t-0 px-6 align-middle border-l-0 border-r-0 whitespace-no-wrap p-4">
          <i className="fas fa-circle text-red-500 mr-2"></i> pending
        </td>
        <td className="border-t-0 px-6 align-middle border-l-0 border-r-0 whitespace-no-wrap p-4">
          placed
        </td>
        <td className="border-t-0 px-6 align-middle border-l-0 border-r-0 whitespace-no-wrap p-4">
          total
        </td>
      </tr>
    </tbody>
  );
};

export default TableBody;
