import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import { useSelector } from 'react-redux';
import Button from '../../components/Button';
import { StoreState } from './../../reducers/index';
import * as Yup from 'yup';
import { useAppDispatch } from '../../index';
import { IRegisterCredentials } from '../../actions/auth/apiCalls';
import { registerAction } from '../../actions/auth';

const signupSchema = Yup.object().shape({
  name: Yup.string().required('Name is required field!'),
  email: Yup.string()
    .email('Invalid email!')
    .required('Email is required field!'),
  password: Yup.string()
    .min(8, 'Too Short password')
    .required('Password is required field!'),
  streetAddress: Yup.string().required('Street address is required field!'),
  zipCode: Yup.number().required('Zip code is required field!'),
});

const Register = () => {
  const [message, setMessage] = useState('');
  const isFormSubmitting = useSelector(
    (state: StoreState) => state.auth.isLoading
  );
  const dispatch = useAppDispatch();
  return (
    <div className="container mx-auto px-4 h-full">
      <div className="flex content-center items-center justify-center h-full">
        <div className="w-full lg:w-6/12 px-4">
          <div className="relative flex flex-col min-w-0 break-words w-full mb-6 shadow-lg rounded-lg bg-gray-300 border-0">
            <div className="flex-auto px-4 lg:px-10 py-10 pt-0">
              <div className="text-gray-500 text-center my-3 font-bold">
                <h5>Register</h5>
              </div>
              <Formik
                initialValues={{
                  name: '',
                  email: '',
                  password: '',
                  streetAddress: '',
                  zipCode: 0,
                }}
                validationSchema={signupSchema}
                onSubmit={(data: IRegisterCredentials, formik) => {
                  dispatch(registerAction(data))
                    .then(res => {
                      if (res?.data) {
                        setMessage(
                          'You are successfully registered, please contact admin.'
                        );
                      }
                      formik.resetForm();
                    })
                    .catch(err => setMessage(err.message));
                }}
              >
                {() => (
                  <Form>
                    <div className="relative w-full mb-3">
                      <label
                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                        htmlFor="name"
                      >
                        Name
                      </label>
                      <Field
                        name="name"
                        type="text"
                        id="name"
                        placeholder="Name"
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                      />
                      <ErrorMessage name="name">
                        {msg => <small className="text-red-400">{msg}</small>}
                      </ErrorMessage>
                    </div>

                    <div className="relative w-full mb-3">
                      <label
                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                        htmlFor="email"
                      >
                        Email
                      </label>
                      <Field
                        name="email"
                        type="email"
                        id="email"
                        placeholder="Email"
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                      />
                      <ErrorMessage name="email">
                        {msg => <small className="text-red-400">{msg}</small>}
                      </ErrorMessage>
                    </div>

                    <div className="relative w-full mb-3">
                      <label
                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                        htmlFor="password"
                      >
                        Password
                      </label>
                      <Field
                        name="password"
                        type="password"
                        id="password"
                        placeholder="Password"
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                      />
                      <ErrorMessage name="password">
                        {msg => <small className="text-red-400">{msg}</small>}
                      </ErrorMessage>
                    </div>

                    <div className="relative w-full mb-3">
                      <label
                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                        htmlFor="streetAddress"
                      >
                        Street Address
                      </label>
                      <Field
                        name="streetAddress"
                        type="text"
                        id="streetAddress"
                        placeholder="Street Address"
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                      />
                      <ErrorMessage name="streetAddress">
                        {msg => <small className="text-red-400">{msg}</small>}
                      </ErrorMessage>
                    </div>

                    <div className="relative w-full mb-3">
                      <label
                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                        htmlFor="zipCode"
                      >
                        Zip Code
                      </label>
                      <Field
                        name="zipCode"
                        type="number"
                        id="zipCode"
                        placeholder="Zip Code"
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                      />
                      <ErrorMessage name="zipCode">
                        {msg => <small className="text-red-400">{msg}</small>}
                      </ErrorMessage>
                    </div>

                    <div>
                      <label className="inline-flex items-center cursor-pointer">
                        <input
                          id="customCheckLogin"
                          type="checkbox"
                          className="form-checkbox text-gray-800 ml-1 w-5 h-5 ease-linear transition-all duration-150"
                        />
                        <span className="ml-2 text-sm font-semibold text-gray-700">
                          I agree with the{' '}
                          <a
                            href="#pablo"
                            className="text-blue-500"
                            onClick={e => e.preventDefault()}
                          >
                            Privacy Policy
                          </a>
                        </span>
                      </label>
                      <small className="block my-4 font-bold">{message}</small>
                    </div>

                    <div className="text-center mt-6">
                      <Button
                        className={`${
                          isFormSubmitting ? 'opacity-70 cursor-wait' : ''
                        }bg-gray-900 disabled:opacity-50 text-white active:bg-gray-700 text-sm font-bold uppercase px-6 py-3 rounded shadow border-none hover:shadow-lg outline-none mr-1 mb-1 w-full ease-linear transition-all duration-150`}
                        loaderSize={10}
                        pending={isFormSubmitting}
                        type="submit"
                        title="Register"
                        icon="fas fa-address-card"
                        disabled={isFormSubmitting}
                      />
                    </div>
                  </Form>
                )}
              </Formik>
            </div>
          </div>
          <div className="flex mt-6 relative">
            <div className="w-1/2 text-right">
              <Link to="/auth/login" className="text-gray-300">
                <small>Login</small>
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
