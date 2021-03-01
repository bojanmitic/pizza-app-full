import {
  ILoginActionStart,
  ILoginActionSuccess,
  ILoginActionFail,
  IRegisterActionFail,
  IRegisterActionStart,
  IRegisterActionSuccess,
  IPasswordRecEmailStart,
  IPasswordRecEmailSuccess,
  IPasswordRecEmailFail,
  IResetPasswordStart,
  IResetPasswordSuccess,
  IResetPasswordFail,
} from './auth';

export enum AuthActionTypes {
  loginStart = 'LOGIN_START',
  loginSuccess = 'LOGIN_SUCCESS',
  loginFail = 'LOGIN_FAIL',
  registerStart = 'REGISTER_START',
  registerSuccess = 'REGISTER_SUCCESS',
  registerFail = 'REGISTER_FAIL',
  passwordRecStart = 'PASSWORD_RECOVERY_START',
  passwordRecSuccess = 'PASSWORD_REC_SUCCESS',
  passwordRecFail = 'PASSWORD_REC_FAIL',
  resetPasswordStart = 'RESET_PASSWORD_START',
  resetPasswordSuccess = 'RESET_PASSWORD_SUCCESS',
  resetPasswordFail = 'RESET_PASSWORD_FAIL',
}

export type AuthActions =
  | ILoginActionStart
  | ILoginActionFail
  | ILoginActionSuccess
  | IRegisterActionFail
  | IRegisterActionStart
  | IRegisterActionSuccess
  | IPasswordRecEmailStart
  | IPasswordRecEmailSuccess
  | IPasswordRecEmailFail
  | IResetPasswordStart
  | IResetPasswordSuccess
  | IResetPasswordFail;
