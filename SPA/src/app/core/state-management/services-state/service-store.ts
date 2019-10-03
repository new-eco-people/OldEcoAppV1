import { tassign } from 'tassign';
import { ServiceActionConstant } from './service-action-constant';


export interface IServiceAppState {

    // Auth Service
    authConfirmEmailLoading: boolean;
    emailAvailableLoader: boolean;

    // Problem Service
    searchProblemLoader: boolean;

}

export const SERVICE_INITIAL_STATE: IServiceAppState = {

    // Auth Service
    authConfirmEmailLoading: true,
    emailAvailableLoader: false,

    // Problem Service
    searchProblemLoader: false,

};



export function serviceReducer(state: IServiceAppState = SERVICE_INITIAL_STATE, action): IServiceAppState {

    const obj = new ServiceActions(state, action);

    switch (action.type) {

        // Auth Service
        case ServiceActionConstant.CONFIRM_EMAIL_LOADING: return obj.changeAuthLoading(true);
        case ServiceActionConstant.CONFIRM_EMAIL_LOADING_FINISHED: return obj.changeAuthLoading(false);
        case ServiceActionConstant.UPDATE_CHECKING_AVALIABILITY_LOADER: return obj.updateEmailAvailableLoader();

        // Problem Service
        case ServiceActionConstant.SET_SEARCH_FOR_PROBLEM_LOADER: return obj.updateSearchLoader();



        default: return state;
    }
}

export class ServiceActions {

    constructor(private state: IServiceAppState, private action) {
    }

    // Auth Service
    changeAuthLoading(state: boolean) {
        return tassign(this.state, { authConfirmEmailLoading: state });
    }

    updateEmailAvailableLoader() {
        return tassign(this.state, { emailAvailableLoader: this.action.state });
    }

    updateSearchLoader() {
        return tassign(this.state, { searchProblemLoader: this.action.state });
    }

}
