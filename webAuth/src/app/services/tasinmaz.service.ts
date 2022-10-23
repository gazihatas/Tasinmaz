import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ResponseCode } from '../enums/responseCode';
import { Constants } from '../Helper/constants';
import { ResponseModel } from '../Models/responseModel';
import { Tasinmaz } from '../Models/tasinmaz';

@Injectable({
  providedIn: 'root'
})
export class TasinmazService {

  constructor(private httpClient:HttpClient) { }

  public deleteTasinmaz(tasinmazId:number)
  {
    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
    const headers= new HttpHeaders({
      'Authorization':`Bearer ${userInfo?.token}`
    });

    const body = {
      Id:tasinmazId
    }

    return this.httpClient.post<ResponseModel>(Constants.BASE_URL + "Arsa/DeleteTasinmaz", body,{headers:headers});
  }

  public addUpdateTasinmaz(
    tasinmazId: number,
    ilId:number,
    ilceId:number,
    mahalleId:number,
    adres:string,
    parsel:number,
    ada:number,
    nitelik:string,
    xCoordinate:string,
    yCoordinate:string,
    parselCoordinate:string,
    userId:string) {

    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${userInfo?.token}`
    });
    const body = {
      Id: tasinmazId,
      IlId:ilId,
      IlceId:ilceId,
      MahalleId:mahalleId,
      Adres:adres,
      Parsel:parsel,
      Ada:ada,
      Nitelik:nitelik,
      XCoordinate:xCoordinate,
      YCoordinate:yCoordinate,
      ParselCoordinate:parselCoordinate,
      AppUserId:userId

    }
    return this.httpClient.post<ResponseModel>(Constants.BASE_URL + "Arsa/AddUpdateTasinmaz", body, { headers: headers });

  }


  public getTasinmazsByAuthorId(authorId:string)
  {

    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
    const headers= new HttpHeaders({
      'Authorization':`Bearer ${userInfo?.token}`
    });

    return this.httpClient.get<ResponseModel>(Constants.BASE_URL + "Arsa/GetTasinmazList?AuthorId="+authorId,{headers:headers}).pipe(map(res=>{
      let tasinmazList = new Array<Tasinmaz>();
      if(res.responseCode==ResponseCode.OK)
      {
        if(res.dateSet)
        {
          console.log(res.dateSet);
          res.dateSet.map((x:any)=>{
            tasinmazList.push(new Tasinmaz(x.id,x.ilId, x.ilAdi, x.ilceId, x.ilceAdi, x.mahalleId,x.mahalleAdi,x.adres,x.parsel,x.ada,x.nitelik,x.xCoordinate,x.yCoordinate));
          })
        }
      }
      return tasinmazList;
    }));
  }


  // public getAllUser()
  // {

  //   let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
  //   const headers= new HttpHeaders({
  //     'Authorization':`Bearer ${userInfo?.token}`
  //   });

  //   return this.httpClient.get<ResponseModel>(Constants.BASE_URL + "user/GetAllUser",{headers:headers}).pipe(map(res=>{
  //     let userList = new Array<User>();
  //     if(res.responseCode==ResponseCode.OK)
  //     {
  //       if(res.dateSet)
  //       {
  //         res.dateSet.map((x:User)=>{
  //           userList.push(new User(x.userId,x.fullName, x.email, x.userName, x.role));
  //         })
  //       }
  //     }
  //     return userList;
  //   }));
  // }



}
