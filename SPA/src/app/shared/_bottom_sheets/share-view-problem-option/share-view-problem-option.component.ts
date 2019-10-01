import { Component, OnInit } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material';

@Component({
  selector: 'app-share-view-problem-option',
  templateUrl: './share-view-problem-option.component.html',
  styleUrls: ['./share-view-problem-option.component.css']
})
export class ShareViewProblemOptionComponent implements OnInit {

  constructor(private bottomRef: MatBottomSheetRef<ShareViewProblemOptionComponent>) { }

  ngOnInit() {
  }

  closeBottomSheet() {
    this.bottomRef.dismiss();
  }

}
