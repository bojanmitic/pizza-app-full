export enum AuthRoutes {
  admin = '/admin',
  dashboard = '/admin/dashboard',
  orders = '/admin/orders',
}

export enum NonAuthRoutes {
  landing = '/auth/landing',
  auth = '/auth',
  login = '/auth/login',
  register = '/auth/register',
  forgotPassword = '/auth/forgotPassword',
  resetPassword = '/auth/resetPassword',
  unauthorized = '/auth/unauthorized',
  sessionExpired = '/auth/sessionExpired',
}
