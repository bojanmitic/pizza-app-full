import { AuthActionTypes } from './types';
import { Dispatch } from 'redux';
import {
  login,
  ILoginCredentials,
  IRegisterCredentials,
  register,
  passwordRecoveryEmail,
  IPasswordRecEmail,
  IResetPassword,
  resetPassword,
} from './apiCalls';

export interface ILoginActionStart {
  type: AuthActionTypes.loginStart;
}

export interface ILoginActionSuccess {
  type: AuthActionTypes.loginSuccess;
  payload: {
    token: string;
    expiresAt: string;
    userRole: string;
  };
}

export interface ILoginActionFail {
  type: AuthActionTypes.loginFail;
  payload: string;
}

export const loginAction = (values: ILoginCredentials) => async (
  dispatch: Dispatch
) => {
  dispatch<ILoginActionStart>({
    type: AuthActionTypes.loginStart,
  });
  try {
    const result = await login(values);
    localStorage.setItem('token', result.data.token);
    localStorage.setItem('userRole', result.data.userRole);
    localStorage.setItem('expiresAt', result.data.expiresAt);

    dispatch<ILoginActionSuccess>({
      type: AuthActionTypes.loginSuccess,
      payload: result.data,
    });
    return result;
  } catch (error) {
    dispatch<ILoginActionFail>({
      type: AuthActionTypes.loginFail,
      payload: error.message,
    });
  }
};

export interface IRegisterActionStart {
  type: AuthActionTypes.registerStart;
}

export interface IRegisterActionSuccess {
  type: AuthActionTypes.registerSuccess;
  payload: string;
}

export interface IRegisterActionFail {
  type: AuthActionTypes.registerFail;
  payload: string;
}

export const registerAction = (values: IRegisterCredentials) => async (
  dispatch: Dispatch
) => {
  dispatch<IRegisterActionStart>({
    type: AuthActionTypes.registerStart,
  });
  try {
    const result = await register(values);

    dispatch<IRegisterActionSuccess>({
      type: AuthActionTypes.registerSuccess,
      payload: result.data,
    });
    return result;
  } catch (error) {
    dispatch<IRegisterActionFail>({
      type: AuthActionTypes.registerFail,
      payload: error.message,
    });
  }
};

export interface IPasswordRecEmailStart {
  type: AuthActionTypes.passwordRecStart;
}

export interface IPasswordRecEmailSuccess {
  type: AuthActionTypes.passwordRecSuccess;
  payload: string;
}

export interface IPasswordRecEmailFail {
  type: AuthActionTypes.passwordRecFail;
  payload: string;
}

export const passwordRecEmailAction = (values: IPasswordRecEmail) => async (
  dispatch: Dispatch
) => {
  dispatch<IPasswordRecEmailStart>({
    type: AuthActionTypes.passwordRecStart,
  });
  try {
    const response = await passwordRecoveryEmail(values);
    dispatch<IPasswordRecEmailSuccess>({
      type: AuthActionTypes.passwordRecSuccess,
      payload: response,
    });
  } catch (error) {
    dispatch<IPasswordRecEmailFail>({
      type: AuthActionTypes.passwordRecFail,
      payload: error.message,
    });
  }
};

export interface IResetPasswordStart {
  type: AuthActionTypes.resetPasswordStart;
}

export interface IResetPasswordSuccess {
  type: AuthActionTypes.resetPasswordSuccess;
  payload: string;
}

export interface IResetPasswordFail {
  type: AuthActionTypes.resetPasswordFail;
  payload: string;
}

export const resetPasswordAction = (values: IResetPassword) => async (
  dispatch: Dispatch
) => {
  dispatch<IResetPasswordStart>({
    type: AuthActionTypes.resetPasswordStart,
  });
  try {
    const response = await resetPassword(values);
    dispatch<IResetPasswordSuccess>({
      type: AuthActionTypes.resetPasswordSuccess,
      payload: response,
    });
  } catch (error) {
    dispatch<IResetPasswordFail>({
      type: AuthActionTypes.resetPasswordFail,
      payload: error.message,
    });
  }
};
