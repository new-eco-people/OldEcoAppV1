import { AppToken } from 'src/app/core/app-domain/app-token';
import { IAppState } from 'src/app/core/state-management/store';
import { NgRedux, select } from '@angular-redux/store';
import { AppProblem } from 'src/app/core/app-domain/app-problem';
import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { ProblemActionConstant } from 'src/app/core/state-management/problem-state/problem-action-constants';
import * as _ from 'lodash';
import { AddEmailToLikeComponent } from '../../add-email-to-like/add-email-to-like.component';
import { finalize } from 'rxjs/operators';
import { MatDialog, MatDialogRef } from '@angular/material';
import { ViewCardComponent } from '../view-card.component';
import { ProblemService } from 'src/app/shared/_services/problem.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-view-card-icons',
  templateUrl: './view-card-icons.component.html',
  styleUrls: ['./view-card-icons.component.css']
})
export class ViewCardIconsComponent implements OnInit, OnDestroy {

  @select((s: IAppState) => s.problem.isComment) isComment: boolean

  @Input() problem: AppProblem;

  VoteLoader = false;
  subscription: Subscription;

  user: AppToken;

  constructor(
    private redux: NgRedux<IAppState>,
    private dialog: MatDialog,
    private problemService: ProblemService,
  ) { }

  ngOnInit() {
    this.getUser();
  }

  toggleComment(ev: any) {
    ev.preventDefault();
    this.redux.dispatch({ type: ProblemActionConstant.TOGGLE_COMMENT_IDEA });
  }


  getUser() {
    this.subscription = this.redux.select(s => s.user.token).subscribe(user => {
      this.user = user;
    });
  }


  VoteProblem(ev: any) {
    ev.preventDefault();
    const id = this.problem._.id;

    if (!this.VoteLoader) {

      if (_.isEmpty(this.user)) {
        this.VoteViaEmail(id);

      } else {
        this.VoteViaId(id);
      }
    }
  }

  VoteViaEmail(id: string) {
    const dialogRef = this.dialog.open(AddEmailToLikeComponent, {
      data: {
        problemId: id
      }
    });

    dialogRef.afterClosed()
    .subscribe(result => {
      if (result) {
        this.problem._.likes += result;
      } 
    });
  }

  VoteViaId(id: string) {
    this.VoteLoader = true;
    this.problemService.likeProblemUsingId(id)

    .pipe(finalize(() => this.VoteLoader = false))

    .subscribe((result: any) => {
      this.problem._.likes += result.data;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
