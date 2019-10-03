import { AppProblem } from './../../../../../core/app-domain/app-problem';
import { ViewCardComponent } from './../../../../../shared/_modals/view-card/view-card.component';
import { ProblemService } from 'src/app/shared/_services/problem.service';
import { AddEmailToLikeComponent } from './../../../../../shared/_modals/add-email-to-like/add-email-to-like.component';
import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
import { MatDialog } from '@angular/material';
import { AppToken } from 'src/app/core/app-domain/app-token';
import { Subscription } from 'rxjs';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { trigger, transition, style, animate, state } from '@angular/animations';

@Component({
  selector: 'app-public-problem-card',
  templateUrl: './public-problem-card.component.html',
  styleUrls: ['./public-problem-card.component.css'],
  animations: [
    trigger('fade', [

      state('void', style({opacity: 0})),

      transition(':enter, :leave', [
        animate(1000)
      ])
    ])
  ]
})
export class PublicProblemCardComponent implements OnInit, OnDestroy {

  @Output() toggleSlider = new EventEmitter<boolean>();

  user: AppToken;
  subscription: Subscription;

  likeLoader = false;

  readLess = true;

  @Input() problem: AppProblem;


  constructor(private dialog: MatDialog, private redux: NgRedux<IAppState>, private problemService: ProblemService) { }

  ngOnInit() {
    this.getUser();
  }


  likeProblem(e) {
    e.preventDefault();
    const id = this.problem._.id;

    if (!this.likeLoader) {

      if (_.isEmpty(this.user)) {
        this.likeViaEmail(id);

      } else {
        this.likeViaId(id);
      }
    }

  }

  getUser() {
    this.subscription = this.redux.select(s => s.user.token).subscribe(user => {
      this.user = user;
    });
  }

  likeViaEmail(id: string) {
    this.toggleSlider.emit(true);
    const dialogRef = this.dialog.open(AddEmailToLikeComponent, {
      data: {
        problemId: id
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.toggleSlider.emit(false);
      if (result) {
        this.problem._.likes += result;
      }
    });
  }

  likeViaId(id: string) {
    this.likeLoader = true;
    this.problemService.likeProblemUsingId(id)

    .pipe(finalize(() => this.likeLoader = false))

    .subscribe((result: any) => {
      this.problem._.likes += result.data;
    });
  }

  viewProblem(ev: any) {
    ev.preventDefault();
    this.toggleSlider.emit(true);
    const dialogRef = this.dialog.open(ViewCardComponent, {
      data: {
        problem : this.problem
      },
      panelClass: 'full-width-dialog'
    });

    dialogRef.afterClosed().subscribe(() => {
      this.toggleSlider.emit(false);
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  truncate(desc: string, length: number) {
    return _.truncate(desc, {
      length
    });
  }

  replaceSpace(str: string) {
    return str.replace(/ /g, '_').toLowerCase();
  }

}
