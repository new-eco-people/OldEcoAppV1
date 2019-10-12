import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublicDefaultComponent } from './public/components/public-auth/public-default/public-default.component';
import { NotFoundComponent } from './public/components/public-home/not-found/not-found.component';
import { PublicAboutComponent } from './public/components/public-home/public-about/public-about.component';
import { PublicHomeComponent } from './public/components/public-home/public-home.component';
import { PublicIdeasComponent } from './public/components/public-home/public-ideas/public-ideas.component';
import { PublicProblemsComponent } from './public/components/public-home/public-problems/public-problems.component';
import { PublicShareIdeaComponent } from './public/components/public-home/public-share-idea/public-share-idea.component';
import { PublicShareProblemsComponent } from './public/components/public-home/public-share-problems/public-share-problems.component';

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
      {path: 'share-idea', component: PublicShareIdeaComponent},
      {path: 'ideas', component: PublicIdeasComponent},
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
