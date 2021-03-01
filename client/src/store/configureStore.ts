import { createStore, compose, applyMiddleware, Store } from 'redux';
import reduxImmutableStateInvariant from 'redux-immutable-state-invariant';
import thunk from 'redux-thunk';
import { rootReducer } from '../reducers';
import { StoreState } from '../reducers/index';

declare global {
  interface Window {
    __REDUX_DEVTOOLS_EXTENSION_COMPOSE__?: typeof compose;
  }
}

function configureStoreProd(initialState?: StoreState): Store<StoreState> {
  const middlewares = [thunk];

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middlewares))
  );
}

function configureStoreDev(initialState?: StoreState): Store<StoreState> {
  const middlewares = [
    // Redux middleware that spits an error on you when you try to mutate your state either inside a dispatch or between dispatches.
    reduxImmutableStateInvariant(),
    thunk,
  ];

  const composeEnhancers =
    window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose; // add support for Redux dev tools
  const store = createStore(
    rootReducer,
    initialState,
    composeEnhancers(applyMiddleware(...middlewares))
  );

  return store;
}

const configureStore =
  process.env.NODE_ENV === 'production'
    ? configureStoreProd
    : configureStoreDev;

export default configureStore;
