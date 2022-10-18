import { Component, OnInit } from '@angular/core';
import { User } from '../Models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent implements OnInit {
  totalLength:any;
  page:number = 1;
  public userList:User[] = [];
  constructor(private userService:UserService) { }

  ngOnInit(): void {
    console.log("user-management running");
    this.getAllUser();
  }

  getAllUser()
  {
    this.userService.getUserList().subscribe((data:User[]) => {
      this.userList=data;
    })
  }

}
