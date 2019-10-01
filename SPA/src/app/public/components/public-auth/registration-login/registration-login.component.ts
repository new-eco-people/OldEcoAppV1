import { Component, OnInit } from '@angular/core';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { PublicActionConstant } from 'src/app/core/state-management/public-state/public-action-constant';

@Component({
  selector: 'app-registration-login',
  templateUrl: './registration-login.component.html',
  styleUrls: ['./registration-login.component.css']
})
export class RegistrationLoginComponent implements OnInit {

  @select((s: IAppState) => s.public.loginRegisterToggle) loginReg: boolean;

  constructor(private redux: NgRedux<IAppState>) { }

  ngOnInit() {
  }

  loginRegToggle() {
    this.redux.dispatch({ type: PublicActionConstant.LOGIN_REGISTRATION });
  }

}
