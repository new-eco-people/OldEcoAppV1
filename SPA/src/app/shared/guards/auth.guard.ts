import { NgRedux, select } from '@angular-redux/store';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { IAppState } from 'src/app/core/state-management/store';
import { AuthService } from '../_services/auth.service';
import { ToasterService } from '../_services/toaster.service';
import * as _ from 'lodash';
import { UserActionConstant } from 'src/app/core/state-management/user-state/user-action-constant';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    // @select((s: IAppState) => s.user.token) token;

  constructor(
    private authService: AuthService,
    private router: Router,
    private toaster: ToasterService,
    private redux: NgRedux<IAppState>
    ) {}

  canActivate(): boolean {
    if (_.isEmpty(this.redux.getState().user.token) || this.redux.getState().user.token.isExpired) {

      this.redux.dispatch({ type: UserActionConstant.LOG_OUT });

      this.router.navigate(['auth']);
      return false;
    }

    return true;
  }
}
