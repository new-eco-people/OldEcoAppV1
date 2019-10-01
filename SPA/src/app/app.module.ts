import { NgRedux, NgReduxModule } from '@angular-redux/store';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IAppState, INITIAL_STATE, rootReducer } from './core/state-management/store';
import { SharedModule } from './shared/shared.module';
import { UserModule } from './user/user.module';
import { NgxImgModule } from 'ngx-img';
import { Ng2OdometerModule } from 'ng2-odometer';
import { PublicHacksInfoComponent } from './public/components/public-home/public-hacks-info/public-hacks-info.component';

@NgModule({
  declarations: [
    AppComponent,
    PublicHacksInfoComponent,
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

