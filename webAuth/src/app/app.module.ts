import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { AllUserManagementComponent } from './all-user-management/all-user-management.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import {ModalModule} from 'ngx-bootstrap/modal';
import { ArticleManagementComponent } from './article-module/article-management/article-management.component';
import { ConfirmModalComponent } from './modal-components/confirm-modal/confirm-modal.component';
import { AddUpdateArticleComponent } from './modal-components/add-update-article/add-update-article.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MyCkEditorComponent } from './sharedModule/ck-editor-module/my-ck-editor/my-ck-editor.component';
import { StripHtmlPipe } from './sharedModule/pipes/strip-html.pipe';
import { TruncatePipe } from './sharedModule/pipes/truncate.pipe';
import { AdminLayoutComponent } from './layout/admin-layout/admin-layout.component';
import { PublicLayoutComponent } from './layout/public-layout/public-layout.component';
import { AllArticleListPublicComponent } from './public-feature-module/all-article-list-public/all-article-list-public.component';
import { ArticleDetailComponent } from './public-feature-module/article-detail/article-detail.component';
import { TasinmazComponent } from './tasinmaz/tasinmaz.component';
import { AddComponent } from './tasinmaz/add/add.component';
import { ListComponent } from './tasinmaz/list/list.component';
import { LogComponent } from './log/log.component';
import { HeaderComponent } from './layout/admin-layout/header/header.component';
import { FooterComponent } from './layout/admin-layout/footer/footer.component';
import { NavbarComponent } from './layout/admin-layout/navbar/navbar.component';
import { UstNavbarComponent } from './layout/admin-layout/ust-navbar/ust-navbar.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { UpdateComponent } from './tasinmaz/update/update.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserManagementComponent,
    AllUserManagementComponent,
    ArticleManagementComponent,
    ConfirmModalComponent,
    AddUpdateArticleComponent,
    MyCkEditorComponent,
    StripHtmlPipe,
    TruncatePipe,
    AdminLayoutComponent,
    PublicLayoutComponent,
    AllArticleListPublicComponent,
    ArticleDetailComponent,
    TasinmazComponent,
    AddComponent,
    ListComponent,
    UpdateComponent,
    LogComponent,
    HeaderComponent,
    FooterComponent,
    NavbarComponent,
    UstNavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ModalModule.forRoot(),
    CKEditorModule,
    NgxPaginationModule

  ],
  entryComponents: [ConfirmModalComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
