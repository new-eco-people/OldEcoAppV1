import { ToasterService } from './../../../../shared/_services/toaster.service';
import { AuthService } from './../../../../shared/_services/auth.service';
import { PublicActionConstant } from './../../../../core/state-management/public-state/public-action-constant';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-verify-email',
  templateUrl: './verify-email.component.html',
  styleUrls: ['./verify-email.component.css']
})
export class VerifyEmailComponent implements OnInit {

  @select((s: IAppState) => s.service.authConfirmEmailLoading) loading: boolean;
  @select((s: IAppState) => s.public.confirmedEmail) confirmedEmail: boolean;

  sending = false;

  constructor(
    private route: ActivatedRoute,
    private redux: NgRedux<IAppState>,
    private authService: AuthService,
    private toasterService: ToasterService) { }

  ngOnInit() {

  }
  resendConfirmation() {

    const userId = this.route.snapshot.queryParamMap.get('userId');

    this.sending = true;
    this.authService.rquestEmailConfirmationLink(userId)

    .pipe(finalize(() => {
      this.sending = false;
      this.redux.dispatch({ type: PublicActionConstant.LOGIN_PAGE, page: 'default' });
    }))

    .subscribe((result: any) => {
      this.toasterService.success(`An email confirmation link has been sent to your email`);
    });
  }

  backToLoginPage() {
    this.redux.dispatch({ type: PublicActionConstant.LOGIN_PAGE, page: 'default' });
  }
}
