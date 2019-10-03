import { Component, OnInit } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { InterfaceActionConstant } from 'src/app/core/state-management/interface-state/interface-action-constant';

@Component({
  selector: 'app-user-left-sidebar',
  templateUrl: './user-left-sidebar.component.html',
  styleUrls: ['./user-left-sidebar.component.css']
})
export class UserLeftSidebarComponent implements OnInit {

  constructor(private redux: NgRedux<IAppState>) { }

  ngOnInit() {
  }

  closeMainMenu(e) {
    e.preventDefault();
    this.redux.dispatch({ type: InterfaceActionConstant.UPDATE_MAIN_MENU_SLIDE, slide: false });
  }

  openMainMenu() {
    this.redux.dispatch({ type: InterfaceActionConstant.UPDATE_MAIN_MENU_SLIDE, slide: true });
  }


}
