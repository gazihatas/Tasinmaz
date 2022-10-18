import { Component, OnInit } from '@angular/core';
import { Constants } from 'src/app/Helper/constants';
import { User } from 'src/app/Models/user';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor() { }

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
