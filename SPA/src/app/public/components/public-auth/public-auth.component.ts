import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from 'src/app/core/state-management/store';
import { PublicActionConstant } from 'src/app/core/state-management/public-state/public-action-constant';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-public-auth',
  templateUrl: './public-auth.component.html',
  styleUrls: ['./public-auth.component.css']
})
export class PublicAuthComponent implements OnInit, OnDestroy {

  myStyle: object = {};
  myParams: object = {};
  width = 100;
  height = 100;
  publicPages: Subscription;

  @select((s: IAppState) => s.public.publicPage) page: string;


  constructor(private route: ActivatedRoute, private redux: NgRedux<IAppState>, private router: Router) {

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
              value: '#fff'
          },
          shape: {
              type: 'circle',
          },
      }
    };
  }

  ngOnInit() {
    this.checkType();
  }

  checkType() {
    this.publicPages = this.route.url.subscribe((e: any) => {
      const defaultPage = [{path: 'default'}];
      const page = e[0] || defaultPage[0];

      this.redux.dispatch({ type: PublicActionConstant.LOGIN_PAGE, page: page.path });
    });
  }

  ngOnDestroy() {
    this.publicPages.unsubscribe();
  }



}
