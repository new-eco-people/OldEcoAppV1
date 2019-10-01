import { AppProblem } from 'src/app/core/app-domain/app-problem';
import { Component, OnInit, HostListener } from '@angular/core';
import { ProblemService } from 'src/app/shared/_services/problem.service';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { ServiceActionConstant } from 'src/app/core/state-management/services-state/service-action-constant';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';

@Component({
  selector: 'app-public-problems',
  templateUrl: './public-problems.component.html',
  styleUrls: ['./public-problems.component.css']
})
export class PublicProblemsComponent implements OnInit {

  // @select((s: IAppState) => s.problem.searchFilter) searchFilter: {};
  @select((s: IAppState) => s.service.searchProblemLoader) loadingProblems: boolean;

  problems: AppProblem[] = [];

  totalItems = 1;
  currentItemSize = 0;
  page = 1;
  filter: any = {};

  constructor(private problemService: ProblemService, private redux: NgRedux<IAppState>) { }

  ngOnInit() {
    this.getInitial();
    this.watchFilterChange();
  }

  getInitial() {
    this.makeSearch({});
  }

  watchFilterChange() {
    this.redux.select(s => s.problem.searchFilter).subscribe(data => {
      // console.log(_.isEmpty(data));
      if (!_.isEmpty(data)) {
        console.log(data);
        this.clearSearchData();
        this.makeSearch(data);
      }
    });
  }

  makeSearch(searchData: any ) {

    if (this.currentItemSize >= this.totalItems) { return; }

    searchData = this.setFilterBeforeLoad(searchData);


    this.redux.dispatch({ type: ServiceActionConstant.SET_SEARCH_FOR_PROBLEM_LOADER, state: true });

    this.problemService.search(searchData)

    .pipe(finalize(() => this.redux.dispatch({ type: ServiceActionConstant.SET_SEARCH_FOR_PROBLEM_LOADER, state: false }) ))

    .subscribe((data: any) => {

      if (!data.items.length) { return; }

      // console.log(data);

      this.setFilterAfterLoad(data);

      data.items.forEach((appProb) => {
        // console.log(appProb);
        this.problems.push(new AppProblem(appProb));
      });
    });
  }

  @HostListener('window:scroll', [])
  onScroll(): void {

  if ((window.innerHeight + window.scrollY) >= (document.body.offsetHeight - 200) && this.totalItems > 9) {
      this.makeSearch(this.filter);
    }
  }

  setFilterAfterLoad (data: any) {
    this.totalItems = data.totalItems;
    this.currentItemSize += data.items.length;
    this.page++;
  }

  setFilterBeforeLoad(data: any) {
    this.filter = {...data};
    return {...this.filter, page: this.page};
  }

  clearSearchData () {
    this.problems = [];
    this.totalItems = 1;
    this.currentItemSize  = 0;
    this.page = 1;
  }

}
