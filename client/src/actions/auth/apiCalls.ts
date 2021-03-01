import { apiClient } from './../api';
import { IStringServiceResponse } from '../../types/interfaces';

export interface ILogin {
  data: {
    token: string;
    expiresAt: string;
    userRole: string;
  };
  message: string;
  success: boolean;
}

export interface ILoginCredentials {
  email: string;
  password: string;
}

export interface IRegisterCredentials {
  name: string;
  email: string;
  password: string;
  streetAddress: string;
  zipCode: number;
}

export interface IRenewToken {
  data: {
    token: string;
    expiresAt: string;
  };
  message: string;
  success: boolean;
}

export interface IPasswordRecEmail {
  email: string;
}

export interface IResetPassword {
  password: string;
  confirmPassword: string;
  token: string;
}

export const login = async (values: ILoginCredentials): Promise<ILogin> => {
  try {
    const response = await apiClient.post<ILogin>('/auth/login', values);
    if (response.data.success === true) {
      return response.data;
    }
    throw new Error('Invalid Username or password');
  } catch (error) {
    if (error instanceof Error) {
      throw error;
    } else {
      throw new Error(error);
    }
  }
};

export const register = async (
  values: IRegisterCredentials
): Promise<IStringServiceResponse> => {
  try {
    const response = await apiClient.post<IStringServiceResponse>(
      '/auth/register',
      values
    );
    if (response.data.success === true) {
      return response.data;
    }
    throw new Error('Invalid credentials');
  } catch (error) {
    if (error instanceof Error) {
      throw error;
    } else {
      throw new Error(error);
    }
  }
};

export const renewTokenCall = async (): Promise<IRenewToken> => {
  try {
    const response = await apiClient.post<IRenewToken>('/auth/renewtoken');
    if (response.data.success === true) {
      localStorage.setItem('token', response.data.data.token);
      localStorage.setItem('expiresAt', response.data.data.expiresAt);
    }
    return response.data;
  } catch (error) {
    if (error instanceof Error) {
      throw error;
    } else {
      throw new Error(error);
    }
  }
};

export const passwordRecoveryEmail = async (
  values: IPasswordRecEmail
): Promise<string> => {
  try {
    const response = await apiClient.post<IStringServiceResponse>(
      '/auth/forgotPassword',
      values
    );
    if (response.data.success === true) {
      return response.data.message;
    }
    throw new Error(response.data.message);
  } catch (error) {
    if (error instanceof Error) {
      throw error;
    } else {
      throw new Error(error);
    }
  }
};

export const resetPassword = async (
  values: IResetPassword
): Promise<string> => {
  try {
    const response = await apiClient.post<IStringServiceResponse>(
      '/auth/resetPassword',
      values
    );
    if (response.data.success === true) {
      return response.data.data;
    }
    throw new Error(response.data.message);
  } catch (error) {
    if (error instanceof Error) {
      throw error;
    } else {
      throw new Error(error);
    }
  }
};
