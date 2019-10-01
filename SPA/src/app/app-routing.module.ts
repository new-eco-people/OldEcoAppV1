import { UserProfileDefaultComponent } from './user/components/user-profile/user-profile-default/user-profile-default.component';
import { UserProfileComponent } from './user/components/user-profile/user-profile.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublicAuthComponent } from './public/components/public-auth/public-auth.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { ConfirmEmailResolver } from './shared/guards/confirm-email.resolver';
import { UserHomeComponent } from './user/components/user-home/user-home.component';
import { UserNewsfeedsComponent } from './user/components/user-newsfeeds/user-newsfeeds.component';
import { NotFoundComponent } from './public/components/public-home/not-found/not-found.component';
import { UserProfileSettingsComponent } from './user/components/user-profile/user-profile-settings/user-profile-settings.component';
import { PublicHomeComponent } from './public/components/public-home/public-home.component';
import { PublicDefaultComponent } from './public/components/public-auth/public-default/public-default.component';
import { PublicAboutComponent } from './public/components/public-home/public-about/public-about.component';
import { PublicShareProblemsComponent } from './public/components/public-home/public-share-problems/public-share-problems.component';
import { PublicProblemsComponent } from './public/components/public-home/public-problems/public-problems.component';
import { PublicHacksInfoComponent } from './public/components/public-home/public-hacks-info/public-hacks-info.component';

const routes: Routes = [
  // {path: 'p', component: PublicHomeComponent },
  // {path: 'auth', component: PublicAuthComponent, resolve : {confirm: ConfirmEmailResolver} },
  // {
  //   path: '',
  //   runGuardsAndResolvers: 'always',
  //   canActivate: [AuthGuard],
  //   component: UserHomeComponent,
  //   children: [
  //     {path: '', component: UserNewsfeedsComponent},
  //     {
  //       path: 'profile',
  //       component: UserProfileComponent,
  //       children: [
  //         {path: '', component: UserProfileDefaultComponent},
  //         {path: 'settings', component: UserProfileSettingsComponent}
  //       ]
  //     }
  //   ]
  // },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    component: PublicHomeComponent,
    children: [
      {path: '', component: PublicDefaultComponent},
      {path: 'about', component: PublicAboutComponent},
      {path: 'share-problems', component: PublicShareProblemsComponent},
      {path: 'problems', component: PublicProblemsComponent},
      // {path: 'hacks', component: PublicHacksInfoComponent},
    ]
  },

  {path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
