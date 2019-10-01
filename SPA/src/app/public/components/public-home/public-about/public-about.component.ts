import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-public-about',
  templateUrl: './public-about.component.html',
  styleUrls: ['./public-about.component.css']
})
export class PublicAboutComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


  getYear() {
    return moment().year();
  }

}
