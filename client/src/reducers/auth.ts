import { AuthActionTypes, AuthActions } from '../actions/auth';

export interface IAuthReducerState {
  loginData: {
    token: string;
    expiresAt: string;
    userRole: string;
  };
  registerData: string;
  loginError: string;
  registerError: string;
  passwordRecEmail: string;
  passwordRecEmailError: string;
  resetPassword: string;
  resetPasswordError: string;
  isLoading: boolean;
}

const initialState = {
  loginData: {
    token: '',
    expiresAt: '',
    userRole: '',
  },
  registerData: '',
  loginError: '',
  registerError: '',
  passwordRecEmail: '',
  passwordRecEmailError: '',
  resetPassword: '',
  resetPasswordError: '',
  isLoading: false,
};

export const authReducer = (
  state: IAuthReducerState = initialState,
  action: AuthActions
) => {
  switch (action.type) {
    case AuthActionTypes.loginStart:
      return {
        ...state,
        isLoading: true,
      };
    case AuthActionTypes.loginSuccess:
      return {
        ...state,
        isLoading: false,
        loginData: action.payload,
        loginError: '',
      };
    case AuthActionTypes.loginFail:
      return {
        ...state,
        isLoading: false,
        loginError: action.payload,
      };
    case AuthActionTypes.registerStart:
      return {
        ...state,
        isLoading: true,
      };
    case AuthActionTypes.registerSuccess:
      return {
        ...state,
        isLoading: false,
        registerData: action.payload,
        registerError: '',
      };
    case AuthActionTypes.registerFail:
      return {
        ...state,
        isLoading: false,
        registerError: action.payload,
      };
    case AuthActionTypes.passwordRecStart:
      return {
        ...state,
        isLoading: true,
        passwordRecEmail: '',
      };
    case AuthActionTypes.passwordRecSuccess:
      return {
        ...state,
        isLoading: false,
        passwordRecEmail: action.payload,
      };
    case AuthActionTypes.passwordRecFail:
      return {
        ...state,
        isLoading: false,
        passwordRecEmailError: action.payload,
        passwordRecEmail: '',
      };
    case AuthActionTypes.resetPasswordStart:
      return {
        ...state,
        isLoading: true,
      };
    case AuthActionTypes.resetPasswordSuccess:
      return {
        ...state,
        isLoading: false,
        resetPassword: action.payload,
      };
    case AuthActionTypes.resetPasswordFail:
      return {
        ...state,
        isLoading: false,
        resetPasswordError: action.payload,
      };
    default:
      return {
        ...state,
      };
  }
};
