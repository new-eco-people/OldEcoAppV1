import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';
import { AuthService } from 'src/app/shared/_services/auth.service';
import { TokenService } from 'src/app/shared/_services/token.service';
import { CommonValidations } from 'src/app/shared/_validations/common-validations';
import { ToasterService } from 'src/app/shared/_services/toaster.service';
import { finalize } from 'rxjs/operators';
import { PublicActionConstant } from 'src/app/core/state-management/public-state/public-action-constant';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  signUpForm: FormGroup;
  resetting = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private tokenService: TokenService,
    private commonValidations: CommonValidations,
    private toasterService: ToasterService,
    private route: ActivatedRoute,
    private redux: NgRedux<IAppState>

    ) { }

    ngOnInit() {
      this.initForm();
    }

    initForm() {
      this.signUpForm = this.fb.group({
        // username: ['', [Validators.required], [this.commonValidations.shouldBeUnique.bind(this.commonValidations)]],
        password: ['', [Validators.required, Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$')]],
        confirmPassword: ['', [Validators.required, this.commonValidations.confirmWith('password')]]
      });
    }

    resetPassword(f: NgForm) {
      const userEmail = this.route.snapshot.queryParamMap.get('userEmail');
      const token = this.route.snapshot.queryParamMap.get('token');

      this.resetting = true;
      this.authService.resetPassword({userEmail, token, ...f.value})

      .pipe(finalize(() => {
        this.resetting = false;
        f.resetForm();
      }))

      .subscribe((data) => {
        console.log(data);
        this.toasterService.success('Your password has been changed');
        this.redux.dispatch({ type: PublicActionConstant.LOGIN_PAGE, page: 'default' });
      });
    }

}
