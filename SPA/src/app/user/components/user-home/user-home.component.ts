import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/_services/user.service';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import * as _ from 'lodash';
import { UserActionConstant } from 'src/app/core/state-management/user-state/user-action-constant';
import { InterfaceActionConstant } from 'src/app/core/state-management/interface-state/interface-action-constant';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css']
})
export class UserHomeComponent implements OnInit {

  @select((s: IAppState) => s.interface.mainSlideMenu) mainMenu: boolean;

  constructor(
    private userService: UserService,
    private redux: NgRedux<IAppState>
    ) { }

  ngOnInit() {

    this.getUser();

  }

  getUser() {
    const personalDetails = this.redux.getState().user.personalDetails;

    if (_.isEmpty(personalDetails)) {
      this.userService.get().subscribe(result => {
        this.redux.dispatch({ type: UserActionConstant.SET_PERSONAL_DETAILS, personalDetails: result });
      });

    }
  }

}
