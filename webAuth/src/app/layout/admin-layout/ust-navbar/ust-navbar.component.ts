import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/Helper/constants';
import { User } from 'src/app/Models/user';

@Component({
  selector: 'app-ust-navbar',
  templateUrl: './ust-navbar.component.html',
  styleUrls: ['./ust-navbar.component.scss']
})
export class UstNavbarComponent implements OnInit {

  constructor(private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onLogout()
  {
    localStorage.removeItem(Constants.USER_KEY);
    this.toastr.success("Başarıyla çıkış yaptınız.");
  }

  get isUserlogin()
  {
    const user = localStorage.getItem(Constants.USER_KEY);
    return user && user.length > 0;
  }

  get user():User{
    return JSON.parse(localStorage.getItem(Constants.USER_KEY)) as User;
  }

  get isAdmin():boolean
  {
    return this.user.role=='Admin';
  }

  get isUser():boolean
  {
    return this.user.role=='User';
  }

}
