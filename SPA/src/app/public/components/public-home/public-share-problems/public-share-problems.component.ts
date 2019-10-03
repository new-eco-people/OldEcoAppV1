import { TermsAndConditionComponent } from './../../../../shared/_modals/terms-and-condition/terms-and-condition.component';
import { ImageFormStructure } from '../../../../shared/form-data-structures/image-form-structure';
import { LocationService } from './../../../../shared/_services/location.service';
import { AppToken } from './../../../../core/app-domain/app-token';
import { UserActionConstant } from './../../../../core/state-management/user-state/user-action-constant';
import { ToasterService } from './../../../../shared/_services/toaster.service';
import { ProblemService } from './../../../../shared/_services/problem.service';
import { Component, OnInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { Subscription } from 'rxjs';
import { ShareProblemFormStructure } from 'src/app/shared/form-data-structures/share-problem-form-structure';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-public-share-problems',
  templateUrl: './public-share-problems.component.html',
  styleUrls: ['./public-share-problems.component.css']
})
export class PublicShareProblemsComponent implements OnInit, OnDestroy {

  uploadingProblem = false;
  loadingCountry = false;
  loadingState = false;

  imageData: ImageFormStructure = new ImageFormStructure();
  // ShareProblemFormData: ShareProblemFormStructure;


  position = 0;
  formSurvey = [true, false, false, false, false, false, false];

  user: AppToken; defaultUser: AppToken;
  userSubScription: Subscription;

  constructor(
    private problemService: ProblemService,
    private toasterService: ToasterService,
    private router: Router,
    private redux: NgRedux<IAppState>,
    private locationService: LocationService,
    public ShareProblemFormData: ShareProblemFormStructure,
    private dialog: MatDialog
    ) {
    }

  ngOnInit() {
    this.setUser();
    this.getCountries();
  }


  createProblem() {

    // console.log(this.ShareProblemFormData.data);

    this.uploadingProblem = true;

    this.problemService.save(this.ShareProblemFormData.data)
    .pipe(finalize(() => this.uploadingProblem = false))
    .subscribe((token: any) => {
      this.toasterService.success('Thanks, Your problem has been shared. You can also view other problems');

      if (token) {
        this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, token: token.token });
      }

      this.ShareProblemFormData.data = Object.assign({});
      this.router.navigate(['problems']);
    });
    // console.log(f.value);
  }

  goNext() {
    this.position++;
    this.position = this.position >= this.formSurvey.length ? this.formSurvey.length - 1 : this.position ;
    this.clearForm();
    this.formSurvey[this.position] = true;
  }

  goBack() {
    this.position--;
    this.position = this.position < 0 ? 0 : this.position;
    this.clearForm();
    this.formSurvey[this.position] = true;

  }

  clearForm() {
    this.formSurvey.forEach((value, index, arr) => {
      arr[index] = false;
    });
  }

  setUser() {
    // this.user = this.redux.getState().user.token;
    this.userSubScription = this.redux.select(s => s.user.token).subscribe(token => {
      if (!token) {
        this.defaultUser = {} as AppToken;
        return;
      }

      this.defaultUser = token;
      Object.assign(this.ShareProblemFormData.data, _.pick(this.defaultUser, ['firstName', 'lastName', 'email']));

    });
  }

  getCountries() {
    this.loadingCountry = true;
    this.locationService.getCountries()

      .pipe(finalize(() => this.loadingCountry = false))

      .subscribe((countries: any) => {
        this.ShareProblemFormData.countries = countries;
      });
  }

  getState(country: number) {
    if (!country) {
      this.ShareProblemFormData.states = [];
      return;
    }
    console.log(country);

    this.loadingState = true;

    this.locationService.getStates(country)

      .pipe(finalize(() => this.loadingState = false))

      .subscribe((states: any) => {
        this.ShareProblemFormData.states = states;
      });

  }

  empty() {
    // console.log(_.isEmpty(this.user));
    return _.isEmpty(this.defaultUser);
  }

  displayTerms() {
    this.dialog.open(TermsAndConditionComponent);
  }

  ngOnDestroy() {
    this.userSubScription.unsubscribe();
  }
}
