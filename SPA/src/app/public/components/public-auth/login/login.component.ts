import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { ForgottenPasswordComponent } from 'src/app/shared/_modals/forgotten-password/forgotten-password.component';
import { AuthService } from 'src/app/shared/_services/auth.service';
import { EmailConfirmationComponent } from 'src/app/shared/_modals/email-confirmation/email-confirmation.component';
import { UserActionConstant } from 'src/app/core/state-management/user-state/user-action-constant';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loading = false;

  constructor(
    private dialog: MatDialog,
    private authService: AuthService,
    private redux: NgRedux<IAppState>,
    private router: Router
    ) { }

  ngOnInit() {
    this.isAuth();
  }

  loginUser(f: NgForm) {
    this.loading = true;

    this.authService.login(f.value)
      .pipe(finalize(() =>
        this.loading = false
      ))

      .subscribe((dataFromServer: any) => {
        this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, token: dataFromServer.token });
        this.router.navigate(['']);
      });

  }

  forgotton() {
    this.dialog.open(ForgottenPasswordComponent);
  }

  emailConfirmation() {
    this.dialog.open(EmailConfirmationComponent);
  }

  isAuth() {
    if (!_.isEmpty(this.redux.getState().user.token) && !this.redux.getState().user.token.isExpired) {
      this.router.navigate(['']);
    }
  }

}
