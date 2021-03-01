import React, { useEffect, Suspense, lazy } from 'react';
import { Switch } from 'react-router-dom';
import ErrorBoundary from '../components/ErrorBoundary';
import { Loader } from '../components/Loader/GridLoader';
import { PrivateRoute } from '../components/PrivateRoute';
import { AuthRoutes } from '../Routes/index';
import { UserRoles } from '../actions/auth/userRoles';
import { renewTokenCall } from '../actions/auth/apiCalls';

const SidenavComponent = lazy(() => import('../components/Sidenav'));
const DashboardComponent = lazy(() => import('../views/admin/Dashboard'));
const OrderComponent = lazy(() => import('../views/admin/Orders'));

const Admin: React.FC = () => {
  useEffect(() => {
    const timeToExpiration = localStorage.getItem('expiresAt');
    const currentTime = Date.now();
    const timeToExpInMin = Date.parse(timeToExpiration!);

    console.log(timeToExpInMin - currentTime);
    console.log(timeToExpInMin);

    //if time to expiration is less than 50min, renew token and start interval
    //this could happen if user refresh page
    if (timeToExpInMin - currentTime < 50 * 60000) {
      renewTokenCall();
    }

    let renewTokenInterval = setInterval(() => renewTokenCall(), 50 * 60000); //50 min
    return () => clearInterval(renewTokenInterval);
  }, []);

  const DashboardComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <DashboardComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const SidenavComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <SidenavComponent />
      </ErrorBoundary>
    </Suspense>
  );

  const OrderComp: React.FC = () => (
    <Suspense
      fallback={
        <Loader styles="h-screen w-full" color="blue" size={20} loading />
      }
    >
      <ErrorBoundary>
        <OrderComponent />
      </ErrorBoundary>
    </Suspense>
  );

  return (
    <div>
      <SidenavComp />
      <div className="relative md:ml-64 bg-gray-200">
        <Switch>
          <PrivateRoute
            path={AuthRoutes.dashboard}
            exact
            component={DashboardComp}
            requiredRoles={[String(UserRoles.admin)]}
          />
          <PrivateRoute
            path={AuthRoutes.orders}
            exact
            component={OrderComp}
            requiredRoles={[
              String(UserRoles.admin),
              String(UserRoles.operator),
            ]}
          />
        </Switch>
      </div>
    </div>
  );
};

export default Admin;
