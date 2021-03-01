import React from 'react';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import Button from '../../components/Button';
import * as Yup from 'yup';
import { useAppDispatch } from '../../index';
import { useSelector } from 'react-redux';
import { StoreState } from '../../reducers/index';
import { IResetPassword } from '../../actions/auth/apiCalls';
import { resetPasswordAction } from '../../actions/auth/auth';

const signupSchema = Yup.object().shape({
  password: Yup.string()
    .min(8, 'Too Short password')
    .required('Password is required field!'),
  confirmPassword: Yup.string()
    .required()
    .test('passwords-match', 'Passwords must match!', function (value) {
      return this.parent.password === value;
    }),
});

const ResetPassword: React.FC = () => {
  const isFormSubmitting = useSelector(
    (state: StoreState) => state.auth.isLoading
  );
  const message = useSelector((state: StoreState) => state.auth.resetPassword);
  const error = useSelector(
    (state: StoreState) => state.auth.resetPasswordError
  );
  const dispatch = useAppDispatch();

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
                  password: '',
                  confirmPassword: '',
                  token: '1231',
                }}
                validationSchema={signupSchema}
                onSubmit={(data: IResetPassword) => {
                  dispatch(resetPasswordAction(data));
                }}
              >
                {() => (
                  <Form>
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
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                        placeholder="Password"
                      />
                      <ErrorMessage name="password">
                        {msg => <small className="text-red-400">{msg}</small>}
                      </ErrorMessage>
                    </div>

                    <div className="relative w-full mb-3">
                      <label
                        className="block uppercase text-gray-700 text-xs font-bold mb-2"
                        htmlFor="confirm-password"
                      >
                        Confirm Password
                      </label>
                      <Field
                        name="confirmPassword"
                        type="password"
                        id="confirm-password"
                        className="px-3 py-3 placeholder-gray-400 text-gray-700 bg-white rounded text-sm shadow focus:outline-none focus:shadow-outline w-full ease-linear transition-all duration-150"
                        placeholder="Confirm Password"
                      />
                      <ErrorMessage name="confirmPassword">
                        {msg => <small className="text-red-400">{msg}</small>}
                      </ErrorMessage>
                    </div>

                    <small className="text-gray-600 font-bold">{message}</small>
                    <small className="text-red-400">{error}</small>
                    <div className="text-center mt-6">
                      <Button
                        className={`${
                          isFormSubmitting ? 'opacity-70 cursor-wait' : ''
                        }bg-gray-900 disabled:opacity-50 text-white active:bg-gray-700 text-sm font-bold uppercase px-6 py-3 rounded shadow border-none hover:shadow-lg outline-none mr-1 mb-1 w-full ease-linear transition-all duration-150`}
                        loaderSize={10}
                        pending={isFormSubmitting}
                        type="submit"
                        title="Send Email"
                        icon="fas fa-paper-plane"
                        disabled={isFormSubmitting}
                      />
                    </div>
                  </Form>
                )}
              </Formik>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ResetPassword;
