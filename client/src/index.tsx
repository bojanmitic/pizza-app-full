import React from 'react';
import ReactDOM from 'react-dom';
import { Provider, useDispatch } from 'react-redux';
import { ThunkDispatch } from 'redux-thunk';
import { App } from './App';
import configureStore from './store/configureStore';
import './index.css';
import '@fortawesome/fontawesome-free/css/all.min.css';
import { StoreState } from './reducers/index';
import { Action } from './actions';

const store = configureStore();

//Enhanced redux dispatched
export type ReduxDispatch = ThunkDispatch<StoreState, any, Action>;
export const useAppDispatch = (): ReduxDispatch => useDispatch<ReduxDispatch>();

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('root')
);
