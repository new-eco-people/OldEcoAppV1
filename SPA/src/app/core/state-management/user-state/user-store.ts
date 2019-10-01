import { TokenService } from 'src/app/shared/_services/token.service';
import { tassign } from 'tassign';
import { AppToken } from '../../app-domain/app-token';
import { UserActionConstant } from './user-action-constant';
import { AppPersonalDetails } from '../../app-domain/app-user';


export interface IUserAppState {
    token: AppToken;
    personalDetails: AppPersonalDetails;
}

export const USER_INITIAL_STATE: IUserAppState = {
    token: Object.assign({}),
    personalDetails: Object.assign({}),
};




export function userReducer(state: IUserAppState = USER_INITIAL_STATE, action): IUserAppState {

    const obj = new UserActions(state, action, new TokenService());

    switch (action.type) {
        case UserActionConstant.SAVE_AUTH_USER: return obj.saveUser();
        case UserActionConstant.SET_AUTH_USER: return obj.setUser();
        case UserActionConstant.LOG_OUT: return obj.logout();
        case UserActionConstant.SET_PERSONAL_DETAILS: return obj.setPersonalDetails();
        // case PublicActionConstant.LOGIN_REGISTRATION: return obj.toggleLoginRegister();
        // case PublicActionConstant.LOGIN_PAGE: return obj.selectPublicPage();
        // case PublicActionConstant.SET_CONFIRM_EMAIL_FLAG: return obj.setConfirmEmail();
        default: return state;
    }
}




class UserActions {

    constructor(private state: IUserAppState, private action: any, private tokenService: TokenService) {

    }

    saveUser() {
        this.tokenService.save(this.action.token);

        // const data = {};

        // if (this.action.token) { data['token'] = this.action.token; }

        return tassign(this.state, { token: this.tokenService.getTokenAsObject() });
    }

    setUser() {
        return tassign(this.state, { token: this.tokenService.getTokenAsObject() });
    }

    logout() {
        this.tokenService.clear();
        return tassign(this.state, { token: null});
    }

    setPersonalDetails() {
        return tassign(this.state, { personalDetails: this.action.personalDetails });
    }

}
