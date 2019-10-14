import { Component, OnInit } from '@angular/core';
import { ImageFormStructure } from 'src/app/shared/form-data-structures/image-form-structure';
import { AppToken } from 'src/app/core/app-domain/app-token';
import { Subscription } from 'rxjs';
import { ToasterService } from 'src/app/shared/_services/toaster.service';
import { IAppState } from 'src/app/core/state-management/store';
import { LocationService } from 'src/app/shared/_services/location.service';
import { Router } from '@angular/router';
import { NgRedux } from '@angular-redux/store';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { TermsAndConditionComponent } from 'src/app/shared/_modals/terms-and-condition/terms-and-condition.component';
import { ShareIdeaFormStructure } from 'src/app/shared/form-data-structures/share-idea-form-structure';
import { IdeaService } from 'src/app/shared/_services/idea.service';

@Component({
  selector: 'app-public-share-idea',
  templateUrl: './public-share-idea.component.html',
  styleUrls: ['./public-share-idea.component.css']
})
export class PublicShareIdeaComponent implements OnInit {

  uploadingProblem = false;
  loadingCountry = false;
  loadingState = false;

  imageData: ImageFormStructure = new ImageFormStructure();

  position = 0;
  formSurvey = [true, false, false, false, false, false, false];

  user: AppToken; defaultUser: AppToken;
  userSubScription: Subscription;

  constructor(
    private ideaService: IdeaService,
    private toasterService: ToasterService,
    private router: Router,
    private redux: NgRedux<IAppState>,
    private locationService: LocationService,
    public ShareIdeaFormData: ShareIdeaFormStructure,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.setUser();
    this.getCountries();
  }

  createIdea() {

  this.uploadingProblem = true;

  this.ideaService.save(this.ShareIdeaFormData.data)
    .pipe(finalize(() => this.uploadingProblem = false))
    .subscribe((token: any) => {
      this.toasterService.success('Thanks, Your idea has been shared. You can also view other ideas');
      console.log(token);

      // if (token) {
      //   this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, token: token.token });
      // }

      // this.ShareIdeaFormData.data = Object.assign({});
      // this.router.navigate(['ideas']);
    });
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
    this.userSubScription = this.redux.select(s => s.user.token).subscribe(token => {
      console.log(token)
      if (!token) {
        this.defaultUser = {} as AppToken;
        return;
      }

      this.defaultUser = token;
      Object.assign(this.ShareIdeaFormData.data, _.pick(this.defaultUser, ['firstName', 'lastName', 'email']));

    });
  }

  getCountries() {
    this.loadingCountry = true;
    this.locationService.getCountries()

      .pipe(finalize(() => this.loadingCountry = false))

      .subscribe((countries: any) => {
        this.ShareIdeaFormData.countries = countries;
      });
  }

  getState(country: number) {
    if (!country) {
      this.ShareIdeaFormData.states = [];
      return;
    }

    this.loadingState = true;

    this.locationService.getStates(country)

      .pipe(finalize(() => this.loadingState = false))

      .subscribe((states: any) => {
        this.ShareIdeaFormData.states = states;
      });

  }

  empty() {
    return _.isEmpty(this.defaultUser);
  }

  displayTerms() {
    this.dialog.open(TermsAndConditionComponent);
  }

  ngOnDestroy() {
    this.userSubScription.unsubscribe();
  }

}
