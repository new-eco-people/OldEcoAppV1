import { ProblemService } from './../../../shared/_services/problem.service';
import { LocationService } from './../../../shared/_services/location.service';
import { UserActionConstant } from './../../../core/state-management/user-state/user-action-constant';
import { AppToken } from './../../../core/app-domain/app-token';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { Subscription } from 'rxjs';
import { IAppState } from 'src/app/core/state-management/store';
import { select, NgRedux } from '@angular-redux/store';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { InterfaceActionConstant } from 'src/app/core/state-management/interface-state/interface-action-constant';
import { finalize } from 'rxjs/operators';
import { ProblemActionConstant } from 'src/app/core/state-management/problem-state/problem-action-constants';
import { ShareProblemFormStructure } from 'src/app/shared/form-data-structures/share-problem-form-structure';

@Component({
  selector: 'app-public-home',
  providers: [Location, {provide: LocationStrategy, useClass: PathLocationStrategy}],
  templateUrl: './public-home.component.html',
  styleUrls: ['./public-home.component.css']
})
export class PublicHomeComponent implements OnInit {

  myStyle: object = {};
  myParams: object = {};
  width = 100;
  height = 100;
  publicPages: Subscription;
  displaySearch = false;


  countries: {id: number, name: string}[] = [];

  states: {id: number, name: string}[] = [];
  loadingStates = false;

  searchFilter: any = {};
  previousFilter: any = {};


  @select((s: IAppState) => s.public.publicPage) page: string;
  @select((s: IAppState) => s.user.token) user: AppToken;
  @select((s: IAppState) => s.interface.mainSlideMenu) mainMenu: boolean;


  constructor(
    private redux: NgRedux<IAppState>,
    private router: Router,
    private location: Location,
    private locationService: LocationService,
    public ShareProblemFormData: ShareProblemFormStructure,
    private problemService: ProblemService) {

    this.myStyle = {
      'position': 'fixed',
      'width': '100%',
      'height': '100%',
      'z-index': -1,
      'top': 0,
      'left': 0,
      'right': 0,
      'bottom': 0,
  };

this.myParams = {
      particles: {
          number: {
              value: 100,
          },
          color: {
              value: '#00ff1d'
          },
          shape: {
              type: 'circle',
          },
          line_linked: {
            color: '#00ff1d'
          }
      }
    };
  }

  ngOnInit() {
    this.checkType();
    
  }

  checkType() {
    this.checkUrlForSearch(this.location.path());
    this.router.events.subscribe(event => {
      this.checkUrlForSearch(this.location.path());
    });
  }

  checkUrlForSearch(path: string) {
    if (path === '/problems') {
      this.displaySearch = true;
      this.getCountries();
    } else {
      this.displaySearch = false;
      this.previousFilter = {};
    }
  }

  forgetMe() {
    this.redux.dispatch({ type: UserActionConstant.LOG_OUT });
  }

  toggleMainMenu () {
    this.redux.dispatch({ type: InterfaceActionConstant.UPDATE_MAIN_MENU_SLIDE });
  }

  getCountries() {
    this.locationService.getCountries().subscribe((data) => {
      this.countries = data as {id: number, name: string}[];
    });
  }

  getState(countryId: any) {


    if (typeof(countryId) !== 'number') {
      this.states = [];
      return;
    }

    this.loadingStates = true;

    this.locationService.getStates(countryId)
    .pipe(finalize(() => this.loadingStates = false))
    .subscribe((data) => {
      this.states = data as {id: number, name: string}[];
    });
  }

  makeSearch() {
    this.previousFilter = {...this.searchFilter};
    this.redux.dispatch({ type: ProblemActionConstant.SEARCH_FOR_PROBLEM, searchFilter: this.searchFilter });
    this.searchFilter = {};
    // console.log(this.searchFilter);
  }

  getSearchCountry(countryId: number) {
    if (!countryId) { return ''; }

    const country = this.countries.find(c => c.id === countryId);

    if (country) { return ' /'+country.name; }

    return '';
  }

  getSearchState(stateId: number) {

    if (this.states.length <= 0) { return ''; }
    if (!stateId) { return ''; }

    const state = this.states.find(s => s.id === stateId);
    if (state) { return ' /'+state.name; }

    return '';

  }

  getSearchedEco(searchedEco: string) {
    if (!searchedEco) { return ''; }
    const eco = this.ShareProblemFormData.eco.find((e) => e.value === searchedEco);

    if (eco) { return ' /'+eco.name; }
    return '';
  }

  getSearchedIco(searchedico: string) {
    if (!searchedico) { return ''; }
    const ico = this.ShareProblemFormData.icos.find((e) => e.value === searchedico);

    if (ico) { return ' /'+ico.name; }
    return '';
  }

  getUnGoals(unGoal: string) {
    if (!unGoal) { return ''; }
    let ungoal = '';
    // const ico = this.ShareProblemFormData.ecoUn.find((e) => e.value === unGoal);

    this.ShareProblemFormData.ecoUn.forEach(eco => {
      const index = eco.unGoals.indexOf(unGoal);
      debugger;
      if (index > -1) { ungoal = ' /'+eco.unGoals[index]; return; }


    });
    return ungoal;
  }


}
