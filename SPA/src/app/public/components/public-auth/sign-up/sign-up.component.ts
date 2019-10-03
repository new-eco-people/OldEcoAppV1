import { ToasterService } from './../../../../shared/_services/toaster.service';
import { AuthService } from './../../../../shared/_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { TokenService } from 'src/app/shared/_services/token.service';
import { CommonValidations } from 'src/app/shared/_validations/common-validations';
import { select } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  @select((s: IAppState) => s.service.emailAvailableLoader) emailAvailabileLoader: boolean;

  signUpForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private tokenService: TokenService,
    private commonValidations: CommonValidations,
    private toasterService: ToasterService
    ) { }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.signUpForm = this.fb.group({
      // username: ['', [Validators.required], [this.commonValidations.shouldBeUnique.bind(this.commonValidations)]],
      email: ['', [Validators.required, Validators.email], [this.commonValidations.shouldBeUnique.bind(this.commonValidations)]],
      password: ['', [Validators.required, Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$')]],
      confirmPassword: ['', [Validators.required, this.commonValidations.confirmWith('password')]],
      terms: ['', [Validators.required, Validators.pattern('true')]]
    });
  }

  signUpUser(form: NgForm) {
    // console.log(this.signUpForm.value);
    this.authService.registerUser(this.signUpForm.value).subscribe((result: any) => {

      this.toasterService.success('Account Created successfully, kindly verify the link sent your email');
      form.resetForm();
      this.signUpForm.reset();

    });
  }

}
