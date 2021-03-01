import React from 'react';
import { Link } from 'react-router-dom';

const Landing = () => {
  return (
    <div className="container relative">
      <div className="w-full lg:w-6/12 text-center mx-auto">
        <h1 className="font-semibold text-gray-100 text-2xl  md:text-5xl">
          Welcome to landing page of Mama mia pizza restaurant portal
        </h1>
        <p className="mt-8 text:md md:text-lg text-gray-300">
          To be able to use the app you should have Admin or Operator
          credentials. Please Sign up or Login with your credentials
        </p>
        <div className="mt-8 flex justify-around">
          <Link
            className="bg-blue-500 text-white active:bg-blue-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
            to="/auth/register"
          >
            Register
          </Link>
          <Link
            className="bg-blue-500 text-white active:bg-blue-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
            to="/auth/login"
          >
            Login
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Landing;
