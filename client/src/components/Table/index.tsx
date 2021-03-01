import React from 'react';
import TableBody from './TableBody';
import TableHead from './TableHead';

const Table = () => {
  return (
    <div>
      <div className="rounded-t mb-0 px-4 py-3 border-0">
        <div className="flex flex-wrap items-center">
          <div className="relative w-full px-4 max-w-full flex-grow flex-1">
            <h3 className="font-semibold text-2xl text-gray-800 my-10">
              Orders
            </h3>
          </div>
        </div>
      </div>
      <div
        className={
          'relative flex flex-col min-w-0 break-words w-full mb-6 shadow-lg rounded "bg-white'
        }
      >
        <div className="block w-full overflow-x-auto">
          {/* Orders table */}
          <table className="items-center w-full bg-transparent border-collapse">
            <TableHead />
            <TableBody />
          </table>
        </div>
      </div>
    </div>
  );
};

export default Table;
