import { tassign } from 'tassign';
import { InterfaceActionConstant } from './interface-action-constant';


export interface IInterfaceAppState {
    mainSlideMenu: boolean;
}

export const INTERFACE_INITIAL_STATE: IInterfaceAppState = {
    mainSlideMenu : false
};




export function interfaceReducer(state: IInterfaceAppState = INTERFACE_INITIAL_STATE, action): IInterfaceAppState {

    const obj = new InterfaceActions(state, action);

    switch (action.type) {
        case InterfaceActionConstant.UPDATE_MAIN_MENU_SLIDE: return obj.updateMainMenuSlide();
        // case PublicActionConstant.PROJECT_SLIDE: return obj.toggleSlide();
        // case PublicActionConstant.LOGIN_REGISTRATION: return obj.toggleLoginRegister();
        // case PublicActionConstant.LOGIN_PAGE: return obj.selectPublicPage();
        // case PublicActionConstant.SET_CONFIRM_EMAIL_FLAG: return obj.setConfirmEmail();
        default: return state;
    }
}




class InterfaceActions {

    constructor(private state: IInterfaceAppState, private action: any) {
    }

    updateMainMenuSlide() {
        return tassign(this.state, { mainSlideMenu: !this.state.mainSlideMenu });
    }

}
