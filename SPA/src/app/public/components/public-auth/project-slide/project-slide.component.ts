import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProblemService } from 'src/app/shared/_services/problem.service';
import { AppProblem } from 'src/app/core/app-domain/app-problem';
@Component({
  selector: 'app-project-slide',
  templateUrl: './project-slide.component.html',
  styleUrls: ['./project-slide.component.css']
})
export class ProjectSlideComponent implements OnInit, OnDestroy {

  count = 0;
  problems: any[] = [];
  problem: AppProblem = new AppProblem();
  slideState: any;

  constructor(private problemService: ProblemService) { }

  ngOnInit() {
    this.getProblems();
  }

  projectSlider() {
    this.slideState = setInterval(() => {
      this.problem._ = false;

      setTimeout(() => {
        this.nextProblem();
      }, 1500);

    }, 5000);
  }

  getProblems() {
    // console.log('getting Problems');
    this.problemService.getPublic().subscribe((data: any) => {
      // console.log(data);
      if (data) {
        console.log(data);
        this.problems = data;
        this.problem.props = {...this.problems[0]};
        this.projectSlider();
      }
    });
  }

  nextProblem() {
    this.count = this.count + 1;
    this.count = this.count >= this.problems.length ? 0 : this.count;
    this.problem.props = {...this.problems[this.count]};
  }

  checkSlide(event) {
    if (event) {
      this.stopSlide();
    } else { this.startSlide(); }

  }

  stopSlide() {
    clearInterval(this.slideState);
  }

  startSlide() {
    this.projectSlider();
  }

  ngOnDestroy() {
    this.stopSlide();
  }

}
