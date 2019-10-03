import { NgRedux } from '@angular-redux/store';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material';
import * as _ from 'lodash';
import { Subscription } from 'rxjs';
import { IAppState } from 'src/app/core/state-management/store';
import { AppToken } from './../../../core/app-domain/app-token';
import { ProblemService } from './../../_services/problem.service';
import { UserActionConstant } from 'src/app/core/state-management/user-state/user-action-constant';
import { finalize } from 'rxjs/operators';
import { TermsAndConditionComponent } from '../terms-and-condition/terms-and-condition.component';

@Component({
  selector: 'app-add-email-to-like',
  templateUrl: './add-email-to-like.component.html',
  styleUrls: ['./add-email-to-like.component.css']
})
export class AddEmailToLikeComponent implements OnInit {
  result: any = 'klsmkmlgkdf';
  formData = {
    emailAddress : null,
    rememberMe: false,
    agree: false
  };

  likeLoader = false;

  constructor(
    private problemService: ProblemService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef <AddEmailToLikeComponent>,
    private redux: NgRedux<IAppState>,
    private dialog: MatDialog
    ) { }

  ngOnInit() {
  }

  sendLike(f: NgForm) {
    this.likeLoader = true;
    this.problemService.likeProblemUsingEmail({problemId: this.data.problemId, ...this.formData})

    .pipe(finalize(() => this.likeLoader = false))

    .subscribe((data: any) => {

      if (data.token) {
        this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, token: data.token });
      }
      this.dialogRef.close(data.data);
    });
    // console.log(this.formData);
  }

  closeDialog() {
    this.dialogRef.close(null);
  }

  displayTerms() {
    const dialogRef = this.dialog.open(TermsAndConditionComponent);
  }



}
