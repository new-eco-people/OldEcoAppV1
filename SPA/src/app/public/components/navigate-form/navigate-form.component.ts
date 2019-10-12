import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-navigate-form',
  templateUrl: './navigate-form.component.html',
  styleUrls: ['./navigate-form.component.css']
})
export class NavigateFormComponent implements OnInit {

  @Output() goBack = new EventEmitter<{}>();
  @Output() goNext = new EventEmitter<{}>();

  @Input() beginning: boolean;
  @Input() end: boolean;
  @Input() position: number;

  constructor() { }

  ngOnInit() {
  }

  clickedGoBack() {
    this.goBack.emit();
  }

  clickedGoNext() {
    this.goNext.emit();
  }
}
