import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

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

    return this.httpClient.post(this.baseURL + "Login", body);
  }


  public register(fullname:string,email:string, password:string)
  {
    const body = {
      FullName:fullname,
      Email:email,
      Password:password
    }

    return this.httpClient.post(this.baseURL + "RegisterUser", body);
  }
}
