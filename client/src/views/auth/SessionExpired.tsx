import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { logout } from './../../actions/auth/logout';

const SessionExpired = () => {
  useEffect(() => {
    logout();
  });
  return (
    <div className="container relative">
      <div className="w-full lg:w-6/12 text-center mx-auto">
        <h1 className="font-semibold text-gray-100 text-2xl  md:text-5xl">
          Your Session has expired, please login again
        </h1>
      </div>
      <div className="flex mt-6 relative">
        <div className="w-1/2 text-right">
          <Link to="/auth/login" className="text-gray-300">
            <small>Login</small>
          </Link>
        </div>
      </div>
    </div>
  );
};

export default SessionExpired;
