import { Component, OnInit } from '@angular/core';
import { User } from '../Models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-all-user-management',
  templateUrl: './all-user-management.component.html',
  styleUrls: ['./all-user-management.component.scss']
})
export class AllUserManagementComponent implements OnInit {

  public userList:User[] = [];
  constructor(private userService:UserService) { }

  ngOnInit(): void {
    console.log("all-user-management run");
    this.getAllUser();
  }

  getAllUser()
  {
    this.userService.getAllUser().subscribe((data:User[]) => {
      this.userList=data;
    })
  }
}
