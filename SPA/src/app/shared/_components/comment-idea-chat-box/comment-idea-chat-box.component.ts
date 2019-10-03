import { NgRedux, select } from '@angular-redux/store';
import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { MatDialog } from '@angular/material';
import * as _ from 'lodash';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { AppComment } from 'src/app/core/app-domain/app-comment';
import { AppProblem } from 'src/app/core/app-domain/app-problem';
import { IAppState } from 'src/app/core/state-management/store';
import { SendCommentViaEmailComponent } from '../../_modals/send-comment-via-email/send-comment-via-email.component';
import { CommentService } from '../../_services/comment.service';
import { AppToken } from './../../../core/app-domain/app-token';

@Component({
  selector: 'app-comment-idea-chat-box',
  templateUrl: './comment-idea-chat-box.component.html',
  styleUrls: ['./comment-idea-chat-box.component.css']
})
export class CommentIdeaChatBoxComponent implements OnInit {

  @select((s: IAppState) => s.problem.isComment) isComment: boolean

  @Input() problem: AppProblem;

  @ViewChild('commentMessage') commentMessage: ElementRef;
  @ViewChild('ideaMessage') ideaMessage: ElementRef;

  user: AppToken;

  subscription: Subscription;

  comment = {message: null};
  idea = {message: null};

  comments: AppComment[] = [];
  ideas = []

  inputField = false;

  totalComments = 1;
  totalIdeas = 1;

  currentCommentSize = 0;
  currentIdeaSize = 0;

  commentPage = 0;
  IdeaPage = 1;

  constructor(
    private redux: NgRedux<IAppState>,
    private dialog: MatDialog,
    private commentService: CommentService,
  ) { }

  ngOnInit() {
    this.getIdeas();
    this.initialIdeaData();
    this.getUser();

    this.totalComments = this.problem._.comments;
    this.totalIdeas = this.problem._.ideas;
  }

  getUser() {
    this.subscription = this.redux.select(s => s.user.token).subscribe(user => {
      this.user = user;
    });
  }

  // --------------------------------------- Message Generic
  sendMessage(f:NgForm, comment = true) {
    // console.log(this.problem);
    if (_.isEmpty(f.value)) { return;}


    if (_.isEmpty(this.user)) {
      this.sendMessageViaEmail(f, comment)
    } else {
      this.sendMessageViaId(f, comment);
    }
  }

  sendMessageViaEmail(f:NgForm, comment: boolean) {
    const ref = this.dialog.open(SendCommentViaEmailComponent, {
      data : { ...this.problem._, message: f.value.message, comment }
    });

    ref.afterClosed()
    // .pipe(finalize(() => f.resetForm()))
    .subscribe(result => {
      
      if(result) {

        // Push comment to top of comments
        if (comment) {this.addCommentToTop(result);}

        // Add idea to top
        else { 
          this.addIdeaToTop(result);
         }

         f.resetForm();
         this.resetTextareaHeight();
      }
    })
  }

  sendMessageViaId(f: NgForm, comment: boolean) {
    this.inputField = true;

    if (comment) { this.sendCommentViaId(f) }

    else { this.sendIdeaViaId(f) }
  }

  resetTextareaHeight() {
    if (this.commentMessage) { this.commentMessage.nativeElement.style.minHeight = '37px'; }

    if (this.ideaMessage) { this.ideaMessage.nativeElement.style.minHeight = '37px'; }
    
  }

  // --------------------------------------- Comment

  getComments() {
    if (this.noComments()) { return; }
    this.commentPage++;

    const data = {problemBetaId: this.problem._.id, page: this.commentPage }

    this.commentService.getComments(data).subscribe((result: any) => {
      this.totalComments = result.totalItems;
      this.currentCommentSize += result.items.length;
      const items: any[] = result.items as any[];
      console.log(items);
      items.forEach((value) => {
        this.addCommentsToBottom(value);
      })

    });
  }

  sendCommentViaId(f: NgForm) {
    this.commentService.sendCommentViaId({ problemId: this.problem._.id, message: f.value.message })
      .pipe(finalize(() => {
        this.inputField = false;
      }))
      .subscribe((result) => {
        // Push comment to top of comments
        this.addCommentToTop(result);
        f.resetForm();
        this.resetTextareaHeight();
      });
  }

  noComments() {
    return this.currentCommentSize >= this.totalComments
  }

  addCommentsToBottom(value: any) {
    this.comments.push(new AppComment(value));
  }

  addCommentToTop(value: any) {
    this.comments.unshift(new AppComment(value));
  }

  autoResizeComment() {
    this.commentMessage.nativeElement.style.minHeight = '0px';
    this.commentMessage.nativeElement.style.minHeight = this.commentMessage.nativeElement.scrollHeight + 4 + 'px';
  }

  // --------------------------------------- End of Comment

  // --------------------------------------- Idea

  initialIdeaData() {
    this.redux.select(s=> s.problem.isComment).subscribe(r => {
      // console.log(r);
      if (!r) {
        
        if (this.ideas.length > 0) { return; }
        this.getIdeas();
        // console.log('ideas');
      }
    });
  }

  getIdeas() {
    if (this.noIdeas()) { return; }

    const data = {problemBetaId: this.problem._.id, page: this.IdeaPage }
    this.IdeaPage++;

    this.commentService.getIdeas(data).subscribe((result: any) => {
      this.totalIdeas = result.totalItems;
      this.currentIdeaSize += result.items.length;
      const items: any[] = result.items as any[];
      items.forEach((value) => {
        this.addIdeaToBottom(value);
      })

    });
  }

  sendIdeaViaId(f: NgForm) {
    this.commentService.sendIdeaViaId({ problemId: this.problem._.id, message: f.value.message })
      .pipe(finalize(() => {
        this.inputField = false;
      }))
      .subscribe((result) => {
        // Push comment to top of comments
        this.addIdeaToTop(result);
        f.resetForm();
        this.resetTextareaHeight();
      });
  }


  noIdeas() {
    return this.currentIdeaSize >= this.totalIdeas
  }

  addIdeaToBottom(value: any) {
    this.ideas.push(new AppComment(value));
  }

  addIdeaToTop(value: any) {
    this.ideas.unshift(new AppComment(value));
  }

  autoResizeIdea() {
    this.ideaMessage.nativeElement.style.minHeight = '0px';
    this.ideaMessage.nativeElement.style.minHeight = this.ideaMessage.nativeElement.scrollHeight + 4 + 'px';
  }

  // --------------------------------------- End of Idea


}
