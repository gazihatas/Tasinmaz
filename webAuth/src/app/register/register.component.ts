import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Role } from '../Models/role';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public roles:Role[]= [];

  public registerForm;

  constructor(
    private formBuilder:FormBuilder,
     private userService:UserService,
     private toastr: ToastrService,
    private router:Router,
     ) { }

  ngOnInit(): void {
    this.registerForm=this.formBuilder.group({
      fullName:['',[Validators.required]],
      email:['',[Validators.email,Validators.required]],
      password:['',Validators.required]
    });

    this.getAllRoles();
  }

  onSubmit()
  {
    console.log("on submit",this.roles);

    //formdan gelen değişkenler
    let fullName = this.registerForm.controls['fullName'].value;
    let email = this.registerForm.controls['email'].value;
    let password = this.registerForm.controls['password'].value;
    //userService mize değişkenlerimizi yolluyoruz.
    this.userService.register(fullName, email, password,this.roles.filter(x=>x.isSelected)[0].role).subscribe((data)=>{
      this.registerForm.controls['fullName'].setValue("");
      this.registerForm.controls['email'].setValue("");
      this.registerForm.controls['password'].setValue("");
      this.roles.forEach(x=>x.isSelected=false);
      this.toastr.success(fullName+" adlı kullanıcı başarıyla oluşturuldu.");
      //this.router.navigate(["/login"]);

      console.log("response",data);
    },error=>{
      console.log("error",error);
      this.toastr.error("Bir şeyler ters gitti, lütfen daha sonra tekrar deneyin.");
    })
  }

  getAllRoles()
  {
    this.userService.getAllRole().subscribe(roles=>{
      this.roles=roles;
    });
  }

  onRoleChange(role:string)
  {
    this.roles.forEach(x=>{
      if(x.role == role)
      {
        x.isSelected=true;
      }else{
        x.isSelected=false;
      }
    })
  }

}
