import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
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

  constructor(private formBuilder:FormBuilder, private userService:UserService) { }

  ngOnInit(): void {
  }

  onSubmit()
  {

    console.log("Login | on submit");
    //formdan gelen değişkenler
    let email = this.loginForm.controls['email'].value;
    let password = this.loginForm.controls['password'].value;
    //userService mize değişkenlerimizi yolluyoruz.
    this.userService.login(email, password).subscribe((data)=>{
      console.log("response",data);
    },error=>{
      console.log("error",error);
    })
  }

}
