import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllUserManagementComponent } from './all-user-management/all-user-management.component';
import { ArticleManagementComponent } from './article-module/article-management/article-management.component';
import { AuthGuardService } from './guards/auth.service';
import { AdminLayoutComponent } from './layout/admin-layout/admin-layout.component';
import { PublicLayoutComponent } from './layout/public-layout/public-layout.component';
import { LoginComponent } from './login/login.component';
import { AllArticleListPublicComponent } from './public-feature-module/all-article-list-public/all-article-list-public.component';
import { ArticleDetailComponent } from './public-feature-module/article-detail/article-detail.component';
import { RegisterComponent } from './register/register.component';
import { AddComponent } from './tasinmaz/add/add.component';
import { ListComponent } from './tasinmaz/list/list.component';
import { TasinmazComponent } from './tasinmaz/tasinmaz.component';
import { UpdateComponent } from './tasinmaz/update/update.component';
import { UserManagementComponent } from './user-management/user-management.component';

const routes: Routes = [

  //{path:'', redirectTo:'/user/login',component:LoginComponent,pathMatch:'full'},
  {path:'',redirectTo:'',component:LoginComponent,pathMatch:'full'},
  {path:"login",component:LoginComponent},
  //{path:'', redirectTo:'/user/login',pathMatch:'full'},
  {path:'anasayfa',component:AdminLayoutComponent,canActivate:[AuthGuardService]},
  //{path:"add",component:RegisterComponent},
  //{path:"all-users",component:AllUserManagementComponent},

  {path:"user",
    children:[
        //{path:"login",component:LoginComponent},
        {path:"add",component:RegisterComponent,canActivate:[AuthGuardService]},
        {path:"listuser",component:UserManagementComponent,canActivate:[AuthGuardService]},
        {path:"listall",component:AllUserManagementComponent,canActivate:[AuthGuardService]},
        {path:"article-management",component:ArticleManagementComponent,canActivate:[AuthGuardService]}
    ]
  },
  // {
  //   path:"",component:PublicLayoutComponent,
  //   children: [
  //    // {path:"",component:AllArticleListPublicComponent},
  //    // {path:"article",component:ArticleDetailComponent},
  //    //   {path:"register",component:RegisterComponent},
  //       {path:"login", component: LoginComponent},
  //   ]
  // },
  {
    path:"tasinmaz",component:TasinmazComponent,
      children:[
        {path:'add',component:AddComponent},
        {path:'list',component:ListComponent},
        {path:'update',component:UpdateComponent}
      ]
  }



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
