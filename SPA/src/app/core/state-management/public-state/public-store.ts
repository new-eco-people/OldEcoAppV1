import { tassign } from 'tassign';
import { PublicActionConstant } from './public-action-constant';


export interface IPublicAppState {
    showSlide: boolean;
    loginRegisterToggle: boolean;
    publicPage: string;
    confirmedEmail: boolean;
}

export const PUBLIC_INITIAL_STATE: IPublicAppState = {
    showSlide : true,
    loginRegisterToggle: true,
    publicPage: 'default',
    confirmedEmail: false
};




export function publicReducer(state: IPublicAppState = PUBLIC_INITIAL_STATE, action): IPublicAppState {

    const obj = new PublicActions(state, action);

    switch (action.type) {
        case PublicActionConstant.PROJECT_SLIDE: return obj.toggleSlide();
        case PublicActionConstant.LOGIN_REGISTRATION: return obj.toggleLoginRegister();
        case PublicActionConstant.LOGIN_PAGE: return obj.selectPublicPage();
        case PublicActionConstant.SET_CONFIRM_EMAIL_FLAG: return obj.setConfirmEmail();
        default: return state;
    }
}




class PublicActions {

    constructor(private state: IPublicAppState, private action: any) {
    }

    toggleSlide() {
        const state = !this.state.showSlide;
        return tassign(this.state, { showSlide: state });
    }

    toggleLoginRegister() {
        const state = !this.state.loginRegisterToggle;
        return tassign(this.state, { loginRegisterToggle: state });
    }

    selectPublicPage() {
       const types = ['password', 'email'];
       const searchType = types.indexOf(this.action.page);

       const page = searchType > -1 ? types[searchType] : 'default';
       return tassign(this.state, { publicPage : page });
    }

    setConfirmEmail() {
        return tassign(this.state, { confirmedEmail: this.action.state });
    }

}
