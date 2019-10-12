import { NgRedux, NgReduxModule } from '@angular-redux/store';
import { NgModule } from '@angular/core';
import { NgxImgModule } from 'ngx-img';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IAppState, INITIAL_STATE, rootReducer } from './core/state-management/store';
import { PublicHacksInfoComponent } from './public/components/public-home/public-hacks-info/public-hacks-info.component';
import { SharedModule } from './shared/shared.module';
import { UserModule } from './user/user.module';

@NgModule({
  declarations: [
    AppComponent,
    PublicHacksInfoComponent
  ],
  imports: [
    SharedModule,
    UserModule,
    AppRoutingModule,
    NgReduxModule,
    NgxImgModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {

  constructor(ngRedux: NgRedux<IAppState>) {
    ngRedux.configureStore(rootReducer, INITIAL_STATE);
  }

 }

