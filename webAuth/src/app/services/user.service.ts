import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pipe } from 'rxjs';
import { ResponseModel } from '../Models/responseModel';
import {map} from 'rxjs/operators';
import { User } from '../Models/user';
import { ResponseCode } from '../enums/responseCode';
import { Constants } from '../Helper/constants';
import { Role } from '../Models/role';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly baseURL:string="https://localhost:5001/api/user/"
  constructor(private httpClient:HttpClient) { }

  public login(email:string, password:string)
  {
    const body = {
      Email:email,
      Password:password
    }

    return this.httpClient.post<ResponseModel>(this.baseURL + "Login", body);
  }


  public register(fullname:string,email:string, password:string,role:string)
  {
    const body = {
      FullName:fullname,
      Email:email,
      Password:password,
      Role:role
    }

    return this.httpClient.post<ResponseModel>(this.baseURL + "RegisterUser", body);
  }

  public getAllUser()
  {

    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
    const headers= new HttpHeaders({
      'Authorization':`Bearer ${userInfo?.token}`
    });

    return this.httpClient.get<ResponseModel>(this.baseURL + "GetAllUser",{headers:headers}).pipe(map(res=>{
      let userList = new Array<User>();
      if(res.responseCode==ResponseCode.OK)
      {
        if(res.dateSet)
        {
          res.dateSet.map((x:User)=>{
            userList.push(new User(x.fullName, x.email, x.userName, x.role));
          })
        }
      }
      return userList;
    }));
  }

  public getAllRole()
  {

    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
    const headers= new HttpHeaders({
      'Authorization':`Bearer ${userInfo?.token}`
    });

    return this.httpClient.get<ResponseModel>(this.baseURL + "GetRoles",{headers:headers}).pipe(map(res=>{
      let roleList = new Array<Role>();
      if(res.responseCode==ResponseCode.OK)
      {
        if(res.dateSet)
        {
          res.dateSet.map((x:string)=>{
            roleList.push(new Role(x));
          })
        }
      }
      return roleList;
    }));
  }

}
