import React, { Suspense, lazy } from 'react';
import {
  BrowserRouter as Router,
  Redirect,
  Route,
  Switch,
} from 'react-router-dom';
import { Loader } from './components/Loader/GridLoader';
import ErrorBoundary from './components/ErrorBoundary';
import { PrivateRoute } from './components/PrivateRoute';
import { AuthRoutes, NonAuthRoutes } from './Routes/index';
import { UserRoles } from './actions/auth/userRoles';

const AuthComponent = lazy(() => import('./layout/Auth'));
const AdminComponent = lazy(() => import('./layout/Admin'));

export const App = (): JSX.Element => {
  const AuthComp = () => (
    <Suspense
      fallback={
        <Loader styles="h-full w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <AuthComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const AdminComp = () => (
    <Suspense
      fallback={
        <Loader styles="h-full w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <AdminComponent />
      </ErrorBoundary>
    </Suspense>
  );

  return (
    <Suspense
      fallback={
        <Loader styles="h-full w-full" color="blue" size={20} loading />
      }
    >
      <Router>
        <Switch>
          <Route path={NonAuthRoutes.auth} component={AuthComp} />
          <PrivateRoute
            path={AuthRoutes.admin}
            component={AdminComp}
            requiredRoles={[
              String(UserRoles.admin),
              String(UserRoles.operator),
            ]}
          />
          <Redirect from="*" to="/auth/landing" />
        </Switch>
      </Router>
    </Suspense>
  );
};
