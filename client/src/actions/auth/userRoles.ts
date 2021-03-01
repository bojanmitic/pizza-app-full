export enum UserRoles {
  admin = 'Admin',
  operator = 'Operator',
}

export const userRoles = {
  admins: [String(UserRoles.admin), String(UserRoles.operator)],
  operators: [String(UserRoles.operator)],
};
