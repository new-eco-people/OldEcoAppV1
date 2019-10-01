import { Component, OnInit } from '@angular/core';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from './core/state-management/store';
import { UserActionConstant } from './core/state-management/user-state/user-action-constant';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Eco';

  constructor(private redux: NgRedux<IAppState>) {}

  ngOnInit() {
    this.redux.dispatch({type: UserActionConstant.SET_AUTH_USER});
  }
}
