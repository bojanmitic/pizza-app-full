import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import { useSelector } from 'react-redux';
import { loginAction } from '../../actions/auth/auth';
import { ILoginCredentials } from '../../actions/auth/apiCalls';
import Button from '../../components/Button';
import { StoreState } from './../../reducers/index';
import * as Yup from 'yup';
import { useAppDispatch } from '../../index';
import { AuthRoutes, NonAuthRoutes } from '../../Routes/index';
import { UserRoles } from '../../actions/auth/userRoles';
import isAuthenticated from './../../utils/isAuthed';

const signupSchema = Yup.object().shape({
  email: Yup.string()
    .email('Invalid email')
    .required('Email is required field!'),
  password: Yup.string()
    .min(8, 'Too Short password')
    .required('Password is required field!'),
});

const Login: React.FC = () => {
  const [loginError, setLoginError] = useState('');
  const isFormSubmitting = useSelector(
    (state: StoreState) => state.auth.isLoading
  );

  const dispatch = useAppDispatch();
  const history = useHistory();

  useEffect(() => {
    const userRole = localStorage.getItem('userRole');

    try {
      if (userRole === UserRoles.admin && isAuthenticated()) {
        history.push(AuthRoutes.dashboard);
      } else if (userRole === UserRoles.operator && isAuthenticated()) {
        history.push(AuthRoutes.orders);
      }
    } catch (error) {
      setLoginError(error.message);
    }
  }, [history]);

  return (
    <div className="container mx-auto px-4 h-full">
      <div className="flex content-center items-center justify-center h-full">
        <div className="w-full lg:w-6/12 px-4">
          <div className="relative flex flex-col min-w-0 break-words w-full mb-6 shadow-lg rounded-lg bg-gray-300 border-0">
            <div className="flex-auto px-4 lg:px-10 py-10 pt-0">
              <div className="text-gray-500 text-center my-3 font-bold">
                <h4>Sign in with credentials</h4>
              </div>
              <Formik
                initialValues={{
                  email: '',
                  password: '',
                }}
                validationSchema={signupSchema}
                onSubmit={(data: ILoginCredentials, formik) => {
                  dispatch(loginAction(data)).then(res => {
                    if (res?.data) {
                      formik.resetForm();
                      if (res.data.userRole === UserRoles.admin) {
                        history.push(AuthRoutes.dashboard);
                      } else if (res.data.userRole === UserRoles.operator) {
                        history.push(AuthRoutes.orders);
                      }
                    } else if (res?.message) {
                      setLoginError(res.message);
                    } else {
                      setLoginError('Something went wrong, please try again');
                    }
                  });
                }}
              >
                {() => (
                  <Form>
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
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                        placeholder="Email"
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
                        id="password"
                        type="password"
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                        placeholder="Password"
                      />
                      <ErrorMessage name="password">
                        {msg => <small className="text-red-400">{msg}</small>}
                      </ErrorMessage>
                    </div>
                    <small className="text-red-400">{loginError}</small>
                    <div className="text-center mt-6">
                      <Button
                        className={`${
                          isFormSubmitting ? 'opacity-70 cursor-wait' : ''
                        }bg-gray-900 disabled:opacity-50 text-white active:bg-gray-700 text-sm font-bold uppercase px-6 py-3 rounded shadow border-none hover:shadow-lg outline-none mr-1 mb-1 w-full ease-linear transition-all duration-150`}
                        loaderSize={10}
                        pending={isFormSubmitting}
                        type="submit"
                        title="Sign in"
                        icon="fas fa-sign-in-alt"
                        disabled={isFormSubmitting}
                      />
                    </div>
                  </Form>
                )}
              </Formik>
            </div>
          </div>
          <div className="flex flex-wrap mt-6 relative">
            <div className="w-1/2">
              <Link to={NonAuthRoutes.forgotPassword} className="text-gray-300">
                <small>Forgot Password</small>
              </Link>
            </div>
            <div className="w-1/2 text-right">
              <Link to={NonAuthRoutes.register} className="text-gray-300">
                <small>Create new account</small>
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
