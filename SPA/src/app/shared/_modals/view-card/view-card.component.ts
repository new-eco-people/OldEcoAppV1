import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { AppComment } from './../../../core/app-domain/app-comment';
import { CommentService } from './../../_services/comment.service';
import { SendCommentViaEmailComponent } from './../send-comment-via-email/send-comment-via-email.component';

@Component({
  selector: 'app-view-card',
  templateUrl: './view-card.component.html',
  styleUrls: ['./view-card.component.css']
})
export class ViewCardComponent implements OnInit {



  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<ViewCardComponent>,
    private commentService: CommentService,
  ) { }

  ngOnInit() {
  }


  closeDialog() {
    this.dialogRef.close();
  }





}
