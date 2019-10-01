import { ShareViewProblemOptionComponent } from './../../../../shared/_bottom_sheets/share-view-problem-option/share-view-problem-option.component';
import { NgRedux, select } from '@angular-redux/store';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { IAppState } from 'src/app/core/state-management/store';
import { ProblemService } from 'src/app/shared/_services/problem.service';
import { MatBottomSheet } from '@angular/material';

@Component({
  selector: 'app-public-default',
  templateUrl: './public-default.component.html',
  styleUrls: ['./public-default.component.css']
})
export class PublicDefaultComponent implements OnInit, OnDestroy {

  @select((s: IAppState) => s.public.showSlide) projectSlide: boolean;
  totalProblems = 0;
  currentValue = 0;
  checkProblemInterval: any;
  displayTotalProblem: any;

  constructor(private problemService: ProblemService, private bottomSheet: MatBottomSheet) { }

  ngOnInit() {
    this.checkTotalProblems();
  }

  checkTotalProblems() {
    this.currentValue = this.totalProblems;

    this.problemService.totalProblems().subscribe((totalProblems: number) => {
      this.totalProblems = totalProblems;
      // console.log(totalProblems);
    });
  }

  // problemCheker() {
  //   this.checkProblemInterval = setInterval(this.checkTotalProblems.bind(this), 5000);
  // }

  ngOnDestroy() {
    // clearInterval(this.checkProblemInterval);
  }

  changeValue(event: any) {
    this.displayTotalProblem = event;
  }

  openLetsDoIt() {
    this.bottomSheet.open(ShareViewProblemOptionComponent);
  }

  // Switch between login and default page. for the beta this will be removed
  // toggleProjectSlide() {
  //   this.redux.dispatch({ type: PublicActionConstant.PROJECT_SLIDE });
  // }


}
