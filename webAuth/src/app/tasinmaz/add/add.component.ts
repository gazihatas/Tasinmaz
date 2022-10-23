import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { Constants } from 'src/app/Helper/constants';
import { Tasinmaz } from 'src/app/Models/tasinmaz';
import { User } from 'src/app/Models/user';
import { TasinmazService } from 'src/app/services/tasinmaz.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {

  public tasinmazList:Tasinmaz[] = [];
  public sehirList:any;
  public tasinmazId:number=0;
  public cities:number=0;
  public districts:number=0;
  public neighbourhoods:number=0;
  public adres:string='';
  public parsel:number=0;
  public ada:number=0;
  public nitelik:string='';
  // cities:{};
  // districts:{};
  // neighbourhoods:{};
 // map:Map;
 // mapParsel:Map;
  public xCoordinate:number=0;
  public yCoordinate:number=0;
  //view:View;
  xCoordinateParsel:number;
  yCoordinateParsel:number;

  constructor(
    private formBuilder:FormBuilder,
    private tasinmazService:TasinmazService,
    private toastr:ToastrService
    ) { }
  public tasinmazAddForm;

  ngOnInit()
  {

  }

  getAllSehir()
  {
    this.tasinmazService.GetCities().subscribe((data:any) => {
      this.sehirList=data;
    })

  }


//  ngOnInit(): void {


//     this.tasinmazAddForm=this.formBuilder.group({
//       Cities: ['',Validators.required],
//       Districts: [{value:'',disabled:false}],
//       Neighbourhoods:[{value:'',disabled:false}],
//       Adres:[null,Validators.required],
//       Ada:[null,Validators.required],
//       Parsel:[null,Validators.required],
//       Nitelik:[null,Validators.required],
//       xCoordinatesParsel:[null,Validators.required],
//       yCoordinatesParsel:[null,Validators.required]
//     });


//     this.tasinmazService.GetCities().subscribe(
//       data => this.cities=data
//     );


//     this.getAllTasinmaz()


//   }


//   confirm()
//   {
//     console.log("on Confirm title");

//     this.tasinmazService.addUpdateTasinmaz(this.tasinmazId,this.cities,this.districts,this.neighbourhoods,this.adres,this.ada,this.parsel,this.nitelik,this.xCoordinate,this.yCoordinate,this.user.userId).subscribe((res)=>{
//       if(res.responseCode==ResponseCode.OK)
//       {
//         if(this.articleId>0)
//         {
//           this.toastr.success("İçerik başarıyla güncellendi.");
//         }else {
//           this.toastr.success("İçerik başarıyla kayıt edildi.");
//         }

//         this.bsModalRef.hide();
//         this.modalResponse.next(true);
//       }else{
//         this.toastr.error("AABirşeyler ters gitti, lütfen tekrar deneyin.");
//       }
//     },()=>{
//       this.toastr.error("Sunucu ile bağlantı kurulumadı. API bağlantınızı kontrol ediniz.");
//     })
//   }

  get user():User{
    return JSON.parse(localStorage.getItem(Constants.USER_KEY)) as User;
  }

  // deger:string;
  // boslukKontrolHataMessage='Bu alan zorunludur.';


    // BtnTasinmazEkle(){
    //     if(this.formModel.value.Ada!=null){
    //         var body={
    //         ilId:this.formModel.value.Cities,
    //         ilceId:this.formModel.value.Districts,
    //         mahalleId:this.formModel.value.Neighbourhoods,
    //         ada:this.formModel.value.Ada,
    //         parsel:this.formModel.value.Parsel,
    //         nitelik:this.formModel.value.Nitelik,
    //         xCoordinate:this.formModel.value.xCoordinatesParsel.toString(),
    //         yCoordinate:this.formModel.value.yCoordinatesParsel.toString()
    //       };
    //       console.log(body);
    //       return this.httpClient.post(Constants.BASE_URL+'/Tasinmaz/Add',body);
    //     }else{
    //       this.toastr.error("Lütfen parsel yanındaki butona basıp bir koordinak seçin","Hata")
    //     }
    // }

    // onAdd()
    // {
    //   console.log("tasinmaz add");
    //   //formdan gelen değişkenler
    //     let cities = this.tasinmazAddForm.controls['Cities'].value;
    //     let districts = this.tasinmazAddForm.controls['Districts'].value;
    //     let neighbourhoods = this.tasinmazAddForm.controls['Neighbourhoods'].value;
    //     let adres = this.tasinmazAddForm.controls['Adres'].value;
    //     let ada = this.tasinmazAddForm.controls['Ada'].value;
    //     let parsel = this.tasinmazAddForm.controls['Parsel'].value;
    //     let nitelik = this.tasinmazAddForm.controls['Nitelik'].value;
    //     let xCoordinatesParsel = this.tasinmazAddForm.controls['xCoordinatesParsel'].value;
    //     let yCoordinatesParsel = this.tasinmazAddForm.controls['yCoordinatesParsel'].value;


    //   //userService mize değişkenlerimizi yolluyoruz.
    //   this.tasinmazService.addUpdateTasinmaz(this.tasinmazId,cities,districts,neighbourhoods,adres,ada,parsel,nitelik,xCoordinatesParsel,yCoordinatesParsel,this.user.userId).subscribe((res)=>{
    //     if(res.responseCode==ResponseCode.OK)
    //     {
    //       if(this.tasinmazId>0)
    //       {
    //         this.toastr.success("İçerik başarıyla güncellendi.");
    //       }else {
    //         this.toastr.success("İçerik başarıyla kayıt edildi.");
    //       }

    //       // this.bsModalRef.hide();
    //       // this.modalResponse.next(true);
    //     }else{
    //       this.toastr.error("AABirşeyler ters gitti, lütfen tekrar deneyin.");
    //     }
    //     this.toastr.success(" adlı kullanıcı başarıyla oluşturuldu.");
    //     //this.router.navigate(["/login"]);

    //     console.log("response",res);
    //   },error=>{
    //     console.log("error",error);
    //     this.toastr.error("Bir şeyler ters gitti, lütfen daha sonra tekrar deneyin.");
    //   })


    // }

    // getAllTasinmaz()
    // {
    //   this.tasinmazService.getTasinmazsByAuthorId(this.user.userId).subscribe((data:Tasinmaz[]) => {
    //     this.tasinmazList=data;
    //   })
    // }



    // OnSubmit(){
    //   this.tasinmazService.BtnTasinmazEkle().subscribe(
    //     (res: any)=>{
    //         this.tasinmazService.formModel.reset()
    //         this.toastr.success('Yeni Taşınmaz başarılı bir şekilde oluşturuldu!','Kayıt Başarılı.'),
    //         this.tasinmazService.formModel.controls.Districts.disable(),
    //         this.tasinmazService.formModel.controls.Neighbourhoods.disable(),
    //         this.neighbourhoods=null,this.districts=null
    //         this.router.navigate(['/tasinmazhome/listtasinmaz']);

    //       });
    // }



    // onSubmit()
    // {
    //   console.log("on submit",this.roles);

    //   //formdan gelen değişkenler
    //   let fullName = this.registerForm.controls['fullName'].value;
    //   let email = this.registerForm.controls['email'].value;
    //   let password = this.registerForm.controls['password'].value;
    //   //userService mize değişkenlerimizi yolluyoruz.
    //   this.userService.register(fullName, email, password,this.roles.filter(x=>x.isSelected)[0].role).subscribe((data)=>{
    //     this.registerForm.controls['fullName'].setValue("");
    //     this.registerForm.controls['email'].setValue("");
    //     this.registerForm.controls['password'].setValue("");
    //     this.roles.forEach(x=>x.isSelected=false);
    //     this.toastr.success(fullName+" adlı kullanıcı başarıyla oluşturuldu.");
    //     //this.router.navigate(["/login"]);

    //     console.log("response",data);
    //   },error=>{
    //     console.log("error",error);
    //     this.toastr.error("Bir şeyler ters gitti, lütfen daha sonra tekrar deneyin.");
    //   })
    // }




}
