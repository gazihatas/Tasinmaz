import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from 'src/app/Helper/constants';
import { User } from 'src/app/Models/user';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  title = 'webAuth';

  constructor(private router:Router) {}


  ngOnInit(): void {
  }

  onLogout()
  {
    localStorage.removeItem(Constants.USER_KEY);

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
