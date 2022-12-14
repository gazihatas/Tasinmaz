import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Constants } from '../Helper/constants';
import { ResponseModel } from '../Models/responseModel';
import { User } from '../Models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm=this.formBuilder.group({
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]
  });

  constructor(
    private formBuilder:FormBuilder,
    private userService:UserService,
    private router:Router,
    private toastr: ToastrService
     ) { }

  ngOnInit(): void {

  }

  onSubmit()
  {

    console.log("Login | on submit");
    //formdan gelen değişkenler
    let email = this.loginForm.controls['email'].value;
    let password = this.loginForm.controls['password'].value;
    //userService mize değişkenlerimizi yolluyoruz.
    this.userService.login(email, password).subscribe((data:ResponseModel)=>{
      if(data.responseCode==1)
      {
        localStorage.setItem(Constants.USER_KEY,JSON.stringify(data.dateSet));
        let user = data.dateSet as User;
          //this.router.navigate(["user/article-management"]);
        if(user.role=='Admin')
        {
          this.toastr.success("Admin Rolü ile  başarıyla giriş yaptınız.");
          //this.router.navigate(["/all-user-management"]);
          //this.router.navigate(["portal/all-user-management"]);

          //this.router.navigate(["user/article-management"]);
          this.router.navigate(["anasayfa"]);
        }else{
          this.toastr.success("User Rolü ile  başarıyla giriş yaptınız.");
          //this.router.navigate(["portal/user-management"]);
          this.router.navigate(["anasayfa"]);
        }

      }

      if(data.responseCode==2)
      {
        this.toastr.error("'Mail veya şifre hatalı!','Hatalı Giriş'");
      }

      console.log("response",data);

    },error=>{
      console.log("error",error);
      this.toastr.error("Sunucu ile bağlantı kurulamadı. Lütfen API bağlantınızı kontrol edin.");

    })
  }



}
