import { combineReducers } from 'redux';
import { authReducer, IAuthReducerState } from './auth';

export interface StoreState {
  auth: IAuthReducerState;
}

export const rootReducer = combineReducers<StoreState>({
  auth: authReducer,
});
