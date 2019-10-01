import { CommentService } from './../../_services/comment.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material';
import { AddEmailToLikeComponent } from '../add-email-to-like/add-email-to-like.component';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { TermsAndConditionComponent } from '../terms-and-condition/terms-and-condition.component';
import { NgForm } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { UserActionConstant } from 'src/app/core/state-management/user-state/user-action-constant';

@Component({
  selector: 'app-send-comment-via-email',
  templateUrl: './send-comment-via-email.component.html',
  styleUrls: ['./send-comment-via-email.component.css']
})
export class SendCommentViaEmailComponent implements OnInit {

  commentLoader = false;
  formData = {
    email : null,
    rememberMe: false,
    agree: false
  };

  constructor(
    private commentService: CommentService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef <AddEmailToLikeComponent>,
    private redux: NgRedux<IAppState>,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
  }

  send(f: NgForm) {

    const data = {...f.value, problemId: this.data.id, message: this.data.message};
    if (this.data.comment) {
      this.sendComment(f, data)
    } else {
      this.sendIdea(f, data)
    }
    // console.log(data);
    
      
  }

  sendComment(f: NgForm, data: any) {
    this.commentService.sendCommentViaEmail(data)

      .pipe(finalize(() => {
        this.commentLoader = false;
        f.resetForm();
      }))

      .subscribe((result: any) => { 

        if (result.token) {
          this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, token: result.token });
        }

        this.dialogRef.close(result.data);
      });
  }

  sendIdea(f: NgForm, data: any) {
    this.commentService.sendIdeaViaEmail(data)

      .pipe(finalize(() => {
        this.commentLoader = false;
        f.resetForm();
      }))

      .subscribe((result: any) => { 

        if (result.token) {
          this.redux.dispatch({ type: UserActionConstant.SAVE_AUTH_USER, token: result.token });
        }

        this.dialogRef.close(result.data);
      });
  }



  closeDialog() {
    this.dialogRef.close(null);
  }

  displayTerms() {
    this.dialog.open(TermsAndConditionComponent);
  }

}
