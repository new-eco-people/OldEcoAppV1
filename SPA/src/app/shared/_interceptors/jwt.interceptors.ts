import { NgRedux } from '@angular-redux/store';
import { HTTP_INTERCEPTORS, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as _ from 'lodash';

import { TokenService } from '../_services/token.service';
import { IAppState } from 'src/app/core/state-management/store';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private tokenService: TokenService, private redux: NgRedux<IAppState>) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const token = this.tokenService.getTokenAsObject();

        // console.log(this.redux.getState());

        if (!_.isEmpty(this.redux.getState().user.token) && !this.redux.getState().user.token.isExpired) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.redux.getState().user.token.asString}`
                }
            });
        }

        return next.handle(request);
    }
}

export const JwtInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi: true,
    deps: [TokenService, NgRedux]
};
