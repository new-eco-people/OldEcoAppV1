import { NgRedux } from '@angular-redux/store';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { ServiceActionConstant } from 'src/app/core/state-management/services-state/service-action-constant';
import { IAppState } from 'src/app/core/state-management/store';
import { UserActionConstant } from 'src/app/core/state-management/user-state/user-action-constant';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly url = environment.baseUrlApi + 'auth';

  constructor(
    private http: HttpClient,
    private redux: NgRedux<IAppState>,
    private router: Router) { }

  login(data: any) {
    return this.http.post(`${this.url}/login`, data);
  }

  logOut() {
    this.redux.dispatch({ type: UserActionConstant.LOG_OUT} );
    this.router.navigate(['auth']);
  }

  registerUser(user: any) {
    return this.http.post(`${this.url}/register`, user);
  }

  available(name: string) {
    return this.http.get(`${this.url}/identity-available/${name}`);
  }

  confirmEmail(userId: string, token: string) {

    this.redux.dispatch({ type: ServiceActionConstant.CONFIRM_EMAIL_LOADING} );

    return this.http.get(`${this.url}/confirmemail?userId=${userId}&token=${token}`)

    .pipe(finalize(() => {
      this.redux.dispatch({ type: ServiceActionConstant.CONFIRM_EMAIL_LOADING_FINISHED} );
    }));
  }

  sendResetPasswordLink(email: string) {

    // console.log('started searching');

    return this.http.get(`${this.url}/generateresetpassword/${email}`);
    // .pipe(finalize(() => {
    //   console.log('finished searching');
    // }));
  }

  resetPassword(data: any) {

    return this.http.post(`${this.url}/resetpassword`, data);

  }

  rquestEmailConfirmationLink(emailOrId: string) {
    return this.http.get(`${this.url}/resendemailconfirmation/${emailOrId}`);
  }

}
