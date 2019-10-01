import { AuthService } from './../../../shared/_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { InterfaceActionConstant } from 'src/app/core/state-management/interface-state/interface-action-constant';

@Component({
  selector: 'app-user-header',
  templateUrl: './user-header.component.html',
  styleUrls: ['./user-header.component.css']
})
export class UserHeaderComponent implements OnInit {

  mobileMenu = false;
  userMenu = false;

  constructor(private authService: AuthService, private redux: NgRedux<IAppState>) { }

  ngOnInit() {
  }

  logOut() {
    this.authService.logOut();
  }

  toggleMobileMenu() {
    this.mobileMenu = !this.mobileMenu;
  }

  toggleUserMenu() {
    this.userMenu = !this.userMenu;
    //console.log(this.userMenu);
  }
}
