import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs/operators';
import { ResponseCode } from '../enums/responseCode';
import { Constants } from '../Helper/constants';
import { ResponseModel } from '../Models/responseModel';
import { Tasinmaz } from '../Models/tasinmaz';
import BingMaps from 'ol/source/BingMaps';

@Injectable({
  providedIn: 'root'
})
export class TasinmazService {
  tasinmaz:[];
  constructor(
    private httpClient:HttpClient,
    private formBuilder:FormBuilder,
    private toastr: ToastrService,
    ) { }

    deger:string;
    boslukKontrolHataMessage='Bu alan zorunludur.';

  // tasinmazAddForm=this.formBuilder.group({
  //     Cities: ['',Validators.required],
  //     Districts: [{value:'',disabled:false}],
  //     Neighbourhoods:[{value:'',disabled:false}],
  //     Adres:[null,Validators.required],
  //     Ada:[null,Validators.required],
  //     Parsel:[null,Validators.required],
  //     Nitelik:[null,Validators.required],
  //     xCoordinatesParsel:[null,Validators.required],
  //     yCoordinatesParsel:[null,Validators.required]
  //   });





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

  // BtnTasinmazEkle(){
  //   if(this.formModel.value.Ada!=null){
  //       var body={
  //       ilId:this.formModel.value.Cities,
  //       ilceId:this.formModel.value.Districts,
  //       mahalleId:this.formModel.value.Neighbourhoods,
  //       ada:this.formModel.value.Ada,
  //       parsel:this.formModel.value.Parsel,
  //       nitelik:this.formModel.value.Nitelik,
  //       xCoordinate:this.formModel.value.xCoordinatesParsel.toString(),
  //       yCoordinate:this.formModel.value.yCoordinatesParsel.toString()
  //     };
  //     console.log(body);
  //     return this.http.post(this.BaseURI+'/Tasinmaz/Add',body);
  //   }else{
  //     this.toastr.error("Lütfen parsel yanındaki butona basıp bir koordinak seçin","Hata")
  //   }
  // }

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
    userId:string) {

    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${userInfo?.token}`
    });
    const body = {
      // ilId:this.tasinmazAddForm.value.Cities,
      // ilceId:this.tasinmazAddForm.value.Districts,
      // mahalleId:this.tasinmazAddForm.value.Neighbourhoods,
      // ada:this.tasinmazAddForm.value.Ada,
      // parsel:this.tasinmazAddForm.value.Parsel,
      // nitelik:this.tasinmazAddForm.value.Nitelik,
      // xCoordinate:this.tasinmazAddForm.value.xCoordinatesParsel.toString(),
      // yCoordinate:this.tasinmazAddForm.value.yCoordinatesParsel.toString()
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

  GetCities(){
    return this.httpClient.get(Constants.BASE_URL+"Arsa/Cities").pipe( );
  }




  GetDistricts(cityId:Number){
    return this.httpClient.get(Constants.BASE_URL+"Arsa/Districts/"+cityId).pipe( );
  }

  GetSingleDistricts(distrtictId:Number){

  }

  GetNeighbourhood(districtsId:Number){
    return this.httpClient.get(Constants.BASE_URL+'Arsa/Neighbourhood/'+districtsId).pipe( );
  }
  GetSingleNeighbourhood(neighbourdId:Number){

  }



   GetParsel(wkt){
    return this.httpClient.get(Constants.BASE_URL+ '/Tasinmaz/Parsel/'+ wkt).pipe();
  }


}
