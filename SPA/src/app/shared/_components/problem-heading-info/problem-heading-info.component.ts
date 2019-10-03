import { AppProblem } from 'src/app/core/app-domain/app-problem';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-problem-heading-info',
  templateUrl: './problem-heading-info.component.html',
  styleUrls: ['./problem-heading-info.component.css']
})
export class ProblemHeadingInfoComponent implements OnInit {

  @Input() problem: AppProblem;
  constructor() { }

  ngOnInit() {
  }

}
