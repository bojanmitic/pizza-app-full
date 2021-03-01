import React from 'react';

const Unauthorized = () => {
  return (
    <div className="container relative">
      <div className="w-full lg:w-6/12 text-center mx-auto">
        <h1 className="font-semibold text-gray-100 text-2xl  md:text-5xl">
          You are not authorized to view this page!!!
        </h1>
      </div>
    </div>
  );
};

export default Unauthorized;
