import { AuthService } from './../_services/auth.service';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { ToasterService } from '../_services/toaster.service';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { PublicActionConstant } from 'src/app/core/state-management/public-state/public-action-constant';
import { UserActionConstant } from 'src/app/core/state-management/user-state/user-action-constant';

@Injectable()

export class ConfirmEmailResolver implements Resolve<{}> {

    constructor(
        private router: Router,
        private toaster: ToasterService,
        private authService: AuthService,
        private redux: NgRedux<IAppState>
    ) {}

    resolve(route: ActivatedRouteSnapshot): Observable<{}> {

        const token: string = route.queryParams['token'] || null;
        const userId: string = route.queryParams['userId'] || null;

        if (!token || !userId) {
            this.router.navigate(['auth']);

            return of(null);
        }

        this.authService.confirmEmail(userId, token)

            .subscribe((data: any) => {

                    this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, token: data.token });

                    this.redux.dispatch({ type: PublicActionConstant.SET_CONFIRM_EMAIL_FLAG, state: true } );

                    this.toaster.success('Your email has been verified successfully');

                    this.router.navigate(['']);

                    return of(null);
                },

                () => this.redux.dispatch({ type: PublicActionConstant.SET_CONFIRM_EMAIL_FLAG, state: false } )

            );

    }
}
