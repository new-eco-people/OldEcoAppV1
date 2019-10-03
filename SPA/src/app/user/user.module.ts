import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { UserLeftSidebarComponent } from './components/user-left-sidebar/user-left-sidebar.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { UserHeaderComponent } from './components/user-header/user-header.component';
import { UserRightSidebarComponent } from './components/user-right-sidebar/user-right-sidebar.component';
import { UserNewsfeedsComponent } from './components/user-newsfeeds/user-newsfeeds.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UserProfileDefaultComponent } from './components/user-profile/user-profile-default/user-profile-default.component';
import { UserProfileSettingsComponent } from './components/user-profile/user-profile-settings/user-profile-settings.component';

@NgModule({
  declarations: [
    UserHomeComponent,
    UserHeaderComponent,
    UserRightSidebarComponent,
    UserLeftSidebarComponent,
    UserNewsfeedsComponent,
    UserProfileComponent,
    UserProfileDefaultComponent,
    UserProfileSettingsComponent
  ],
  imports: [
    SharedModule
  ]
})
export class UserModule { }
