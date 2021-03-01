import React, { Suspense, lazy } from 'react';
import { Route, Switch, Redirect } from 'react-router-dom';
import ErrorBoundary from '../components/ErrorBoundary';
import { Loader } from '../components/Loader/GridLoader';
import { NonAuthRoutes } from '../Routes/index';

const LandingComponent = lazy(() => import('../views/Landing'));
const LoginComponent = lazy(() => import('../views/auth/Login'));
const RegisterComponent = lazy(() => import('../views/auth/Register'));
const UnauthorizedComponent = lazy(() => import('../views/auth/Unauthorized'));
const SessionExpiredComponent = lazy(
  () => import('../views/auth/SessionExpired')
);
const PasswordRecoveryEmailComponent = lazy(
  () => import('../views/auth/PasswordRecoveryEmail')
);
const ResetPasswordComponent = lazy(
  () => import('../views/auth/ResetPassword')
);

const Auth: React.FC = () => {
  const LandingComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <LandingComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const LoginComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <LoginComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const RegisterComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <RegisterComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const UnauthorizedComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <UnauthorizedComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const SessionExpiredComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <SessionExpiredComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const PasswordRecoveryEmailComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <PasswordRecoveryEmailComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const ResetPasswordComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <ResetPasswordComponent />
      </ErrorBoundary>
    </Suspense>
  );

  return (
    <div>
      <main>
        <section className="relative w-full min-h-screen flex justify-center items-center">
          <div
            className="absolute top-0 w-full h-full bg-gray-900 bg-no-repeat"
            style={{
              backgroundImage:
                'url(' + require('../assets/img/register_bg_2.png') + ')',
            }}
          ></div>
          <Switch>
            <Route path={NonAuthRoutes.landing} exact component={LandingComp} />
            <Route path={NonAuthRoutes.login} exact component={LoginComp} />
            <Route
              path={NonAuthRoutes.unauthorized}
              exact
              component={UnauthorizedComp}
            />
            <Route
              path={NonAuthRoutes.sessionExpired}
              exact
              component={SessionExpiredComp}
            />
            <Route
              path={NonAuthRoutes.register}
              exact
              component={RegisterComp}
            />
            <Route
              path={NonAuthRoutes.forgotPassword}
              exact
              component={PasswordRecoveryEmailComp}
            />
            <Route
              path={NonAuthRoutes.resetPassword}
              exact
              component={ResetPasswordComp}
            />
            <Redirect from="/auth" to={NonAuthRoutes.landing} />
          </Switch>
        </section>
      </main>
    </div>
  );
};

export default Auth;
