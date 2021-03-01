import React from 'react';
import { Route, Redirect, RouteComponentProps } from 'react-router-dom';
import { NonAuthRoutes } from '../../Routes/index';
import isAuthenticated from '../../utils/isAuthed';

interface IProps {
  component: React.FC<RouteComponentProps>;
  path: string;
  exact?: boolean;
  requiredRoles: string[];
}

const PrivateRoute = ({
  component: Component,
  path,
  exact = false,
  requiredRoles,
}: IProps): JSX.Element => {
  const userRole = localStorage.getItem('userRole');
  const userHasRequiredRole = requiredRoles.includes(userRole || '');
  const expiresAt = localStorage.getItem('expiresAt');
  const isExpired = expiresAt ? new Date().toISOString() > expiresAt : true;

  return (
    <Route
      exact={exact}
      path={path}
      render={(props: RouteComponentProps) =>
        isAuthenticated() && userHasRequiredRole ? (
          <Component {...props} />
        ) : (
          <Redirect
            to={{
              pathname: userHasRequiredRole
                ? isExpired
                  ? NonAuthRoutes.sessionExpired
                  : NonAuthRoutes.login
                : NonAuthRoutes.unauthorized,
            }}
          />
        )
      }
    />
  );
};

export { PrivateRoute };
