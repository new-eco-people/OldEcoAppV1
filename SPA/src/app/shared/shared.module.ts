import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { ParticlesModule } from 'angular-particle';
import { Ng2OdometerModule } from 'ng2-odometer';
import { CustomFormsModule } from 'ng2-validation';
import { NgxImgModule } from 'ngx-img';
import { LoginComponent } from '../public/components/public-auth/login/login.component';
import { PublicProblemCardComponent } from '../public/components/public-auth/project-slide/public-problem-card/public-problem-card.component';
import { PublicAuthComponent } from '../public/components/public-auth/public-auth.component';
import { PublicDefaultComponent } from '../public/components/public-auth/public-default/public-default.component';
import { RegistrationLoginComponent } from '../public/components/public-auth/registration-login/registration-login.component';
import { ResetPasswordComponent } from '../public/components/public-auth/reset-password/reset-password.component';
import { SignUpComponent } from '../public/components/public-auth/sign-up/sign-up.component';
import { VerifyEmailComponent } from '../public/components/public-auth/verify-email/verify-email.component';
import { NotFoundComponent } from '../public/components/public-home/not-found/not-found.component';
import { PublicAboutComponent } from '../public/components/public-home/public-about/public-about.component';
import { PublicHomeComponent } from '../public/components/public-home/public-home.component';
import { PublicProblemsComponent } from '../public/components/public-home/public-problems/public-problems.component';
import { PublicShareProblemsComponent } from '../public/components/public-home/public-share-problems/public-share-problems.component';
import { ProjectSlideComponent } from './../public/components/public-auth/project-slide/project-slide.component';
import { AppMaterialsModule } from './app-materials/app-ng-materials.modules';
import { ShareProblemFormStructure } from './form-data-structures/share-problem-form-structure';
import { ConfirmEmailResolver } from './guards/confirm-email.resolver';
import { ShareViewProblemOptionComponent } from './_bottom_sheets/share-view-problem-option/share-view-problem-option.component';
import { CommentIdeaChatBoxComponent } from './_components/comment-idea-chat-box/comment-idea-chat-box.component';
import { ProblemHeadingInfoComponent } from './_components/problem-heading-info/problem-heading-info.component';
import { ViewPicturesComponent } from './_components/view-pictures/view-pictures.component';
import { AppErrorHandler } from './_interceptors/error.handler';
import { ErrorInterceptorProvider } from './_interceptors/error.interceptor';
import { JwtInterceptorProvider } from './_interceptors/jwt.interceptors';
import { AddEmailToLikeComponent } from './_modals/add-email-to-like/add-email-to-like.component';
import { EmailConfirmationComponent } from './_modals/email-confirmation/email-confirmation.component';
import { ForgottenPasswordComponent } from './_modals/forgotten-password/forgotten-password.component';
import { SendCommentViaEmailComponent } from './_modals/send-comment-via-email/send-comment-via-email.component';
import { ShowPictureComponent } from './_modals/show-picture/show-picture.component';
import { TermsAndConditionComponent } from './_modals/terms-and-condition/terms-and-condition.component';
import { ViewCardIconsComponent } from './_modals/view-card/view-card-icons/view-card-icons.component';
import { ViewCardComponent } from './_modals/view-card/view-card.component';
import { AuthService } from './_services/auth.service';
import { CommentService } from './_services/comment.service';
import { LocationService } from './_services/location.service';
import { ProblemService } from './_services/problem.service';
import { ToasterService } from './_services/toaster.service';
import { TokenService } from './_services/token.service';
import { UserService } from './_services/user.service';
import { DefaultTemplateComponent } from './_toasters/default-template/default-template.component';
import { CommonValidations } from './_validations/common-validations';
import { PublicShareIdeaComponent } from '../public/components/public-home/public-share-idea/public-share-idea.component';
import { PublicIdeasComponent } from '../public/components/public-home/public-ideas/public-ideas.component';
import { ShareIdeaFormStructure } from './form-data-structures/share-idea-form-structure';
import { NavigateFormComponent } from '../public/components/navigate-form/navigate-form.component';




@NgModule({
    declarations: [
        PublicAuthComponent,
        LoginComponent,
        SignUpComponent,
        PublicHomeComponent,
        RegistrationLoginComponent,
        ProjectSlideComponent,
        PublicProblemCardComponent,
        PublicDefaultComponent,
        VerifyEmailComponent,
        ResetPasswordComponent,
        ForgottenPasswordComponent,
        DefaultTemplateComponent,
        EmailConfirmationComponent,
        NotFoundComponent,
        PublicAboutComponent,
        PublicShareProblemsComponent,
        PublicProblemsComponent,
        AddEmailToLikeComponent,
        ViewCardComponent,
        ShowPictureComponent,
        TermsAndConditionComponent,
        ShareViewProblemOptionComponent,
        SendCommentViaEmailComponent,
        ViewPicturesComponent,
        ProblemHeadingInfoComponent,
        ViewCardIconsComponent,
        CommentIdeaChatBoxComponent,
        PublicShareIdeaComponent,
        PublicIdeasComponent,
        NavigateFormComponent
    ],

    providers: [
        CommonValidations,
        AuthService,
        TokenService,
        ToasterService,
        UserService,
        ProblemService,
        ConfirmEmailResolver,
        ErrorInterceptorProvider,
        JwtInterceptorProvider,
        { provide: ErrorHandler, useClass: AppErrorHandler },
        LocationService,
        ShareProblemFormStructure,
        ShareIdeaFormStructure,
        CommentService
    ],

    imports: [
        CommonModule,
        HttpClientModule,
        BrowserModule,
        AppMaterialsModule,
        FlexLayoutModule,
        FormsModule,
        ReactiveFormsModule,
        ParticlesModule,
        RouterModule,
        CustomFormsModule,
        NgxImgModule,
        BrowserAnimationsModule,
        Ng2OdometerModule.forRoot()
    ],

    exports : [
        CommonModule,
        HttpClientModule,
        BrowserModule,
        AppMaterialsModule,
        FlexLayoutModule,
        FormsModule,
        ReactiveFormsModule,
        ParticlesModule,
        RouterModule,
        CustomFormsModule,
        PublicShareIdeaComponent,
        PublicIdeasComponent,
        NavigateFormComponent
    ],
    entryComponents : [
        ForgottenPasswordComponent,
        EmailConfirmationComponent,
        AddEmailToLikeComponent,
        ViewCardComponent,
        ShowPictureComponent,
        TermsAndConditionComponent,
        ShareViewProblemOptionComponent,
        SendCommentViaEmailComponent
    ]
})

export class SharedModule {}
