import { IPublicAppState, PUBLIC_INITIAL_STATE, publicReducer } from './public-state/public-store';
import { combineReducers } from 'redux';
import { IServiceAppState, SERVICE_INITIAL_STATE, serviceReducer } from './services-state/service-store';
import { USER_INITIAL_STATE, IUserAppState, userReducer } from './user-state/user-store';
import { IInterfaceAppState, INTERFACE_INITIAL_STATE, interfaceReducer } from './interface-state/interface-store';
import { PROBLEM_INITIAL_STATE, IProblemAppState, problemReducer } from './problem-state/problem-store';


export interface IAppState {
    public: IPublicAppState;
    service: IServiceAppState;
    user: IUserAppState;
    interface: IInterfaceAppState;
    problem: IProblemAppState;
}

export const INITIAL_STATE: IAppState = {
    public: PUBLIC_INITIAL_STATE,
    service: SERVICE_INITIAL_STATE,
    user: USER_INITIAL_STATE,
    interface: INTERFACE_INITIAL_STATE,
    problem: PROBLEM_INITIAL_STATE,
};

export const rootReducer = combineReducers({
    public: publicReducer,
    service: serviceReducer,
    user: userReducer,
    interface: interfaceReducer,
    problem: problemReducer
});
