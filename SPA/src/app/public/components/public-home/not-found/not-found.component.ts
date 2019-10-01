import { Component, OnInit } from '@angular/core';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { UserActionConstant } from 'src/app/core/state-management/user-state/user-action-constant';
import { AppToken } from 'src/app/core/app-domain/app-token';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.css']
})
export class NotFoundComponent implements OnInit {

  myStyle: object = {};
  myParams: object = {};
  width = 100;
  height = 100;

  @select((s: IAppState) => s.user.token) user: AppToken;

  constructor(private redux: NgRedux<IAppState>) {

    this.myStyle = {
      'position': 'fixed',
      'width': '100%',
      'height': '100%',
      'z-index': -1,
      'top': 0,
      'left': 0,
      'right': 0,
      'bottom': 0,
  };

this.myParams = {
      particles: {
          number: {
              value: 100,
          },
          color: {
              value: '#00ff1d'
          },
          shape: {
              type: 'circle',
          },
          line_linked: {
            value: '#00ff1d',
            color: '#00ff1d'
          }
      }
    };

  }


  ngOnInit() {
  }

  forgetMe() {
    this.redux.dispatch({ type: UserActionConstant.LOG_OUT });
  }

}
