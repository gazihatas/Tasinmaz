import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'webAuth';

  constructor(private router:Router) {}

  onLogout()
  {
    localStorage.removeItem("userInfo");
  }

  get isUserlogin()
  {
    const user = localStorage.getItem("userInfo");
    return user && user.length > 0;
  }
}
