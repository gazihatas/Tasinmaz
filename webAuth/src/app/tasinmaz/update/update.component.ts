import { Component, ElementRef, OnInit } from '@angular/core';
import Map from "ol/Map";
import View from "ol/View";
import Tile from "ol/layer/Tile";
import OSM from "ol/source/OSM";
import ControlScaleLine from "ol/control/ScaleLine";

//import Map from 'ol/Map';
import WKT from 'ol/format/WKT';
//import View from 'ol/View';
import Stroke from 'ol/style/Stroke';
import Style from 'ol/style/Style';
import Fill from 'ol/style/Fill';
import Text from 'ol/style/Text';


//import Tile from 'ol/layer/Tile';
import Vector from 'ol/source/Vector';
//import OSM from 'ol/source/OSM';
import XYZ from 'ol/source/XYZ';
import Point from 'ol/geom/Point';
import MultiPoint from 'ol/geom/MultiPoint';
import Feature from 'ol/Feature';
import Polygon from 'ol/geom/Polygon';
import {fromLonLat, transform}from 'ol/proj.js'
import VectorLayer from 'ol/layer/Vector';
import VectorSource from 'ol/source/Vector';
import MultiPolygon from 'ol/geom/MultiPolygon';
import {Circle as CircleStyle} from 'ol/style';
import { Tasinmaz } from 'src/app/Models/tasinmaz';
import { Cities } from 'src/app/Models/cities';
import { Neighbourhoods } from 'src/app/Models/neighbourhoods';
import { Districts } from 'src/app/Models/districts';
import { ToastrService } from 'ngx-toastr';
import { TasinmazService } from 'src/app/services/tasinmaz.service';
import { FormBuilder, Validators } from '@angular/forms';
import { ResponseCode } from 'src/app/enums/responseCode';
import { Constants } from 'src/app/Helper/constants';
import { User } from 'src/app/Models/user';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {

  constructor( private formBuilder:FormBuilder,
    private tasinmazService:TasinmazService,
    private toastr:ToastrService,
    private elementRef: ElementRef,
    private router: ActivatedRoute) { }


  public tasinmazList:Tasinmaz[] = [];
  public cities: Cities[]=[];
  public districts: Districts[]= [];
  public neighbourhoods: Neighbourhoods[]=[];
  public adres:string='';
  // public map:Map;
  // public mapParsel:Map;
  public xCoordinate:number;
  public yCoordinate:number;
  // public view:View;
  public xCoordinateParsel:number;
  public yCoordinateParsel:number;
  public tasinmazId:number=0;
  public tasinmazUpdateForm;
  control: ControlScaleLine;
  map: Map;
  view: View;
  deger:string;
  boslukKontrolHataMessage='Bu alan zorunludur.';
  scaleType = "scaleline";
  scaleBarSteps = 4;
  scaleBarText = true;

  ngOnInit(): void {
    this.initilizeMap();
    console.log("tasinmaz id=> ",this.router.snapshot.params.id)
    // getAllTasinmaz()
    // {
    //   this.tasinmazService.getTasinmazsByAuthorId(this.user.userId).subscribe((data:Tasinmaz[]) => {
    //     this.tasinmazList=data;
    //   })

    // }
    console.log("sehirList")
    this.tasinmazService.GetCities().subscribe((data:Cities[])=>{
      this.cities=data;
    })
    // this.tasinmazService.GetCities().subscribe(
    //   data:this.cities=data
    // );

      this.tasinmazUpdateForm=this.formBuilder.group({
        Cities: ['',Validators.required],
        Districts: [{value:'',disabled:false}],
        Neighbourhoods:[{value:'',disabled:false}],
        Adres:[null,Validators.required],
        Ada:[null,Validators.required],
        Parsel:[null,Validators.required],
        Nitelik:[null,Validators.required],
        xCoordinatesParsel:[null,Validators.required],
        yCoordinatesParsel:[null,Validators.required]
      });
  }

  initilizeMap() {
    this.map = new Map({
      // controls: defaultControls().extend([this.scaleControl()]),
      target: "map",
      layers: [new Tile({ source: new OSM() })],
      view: new View({
        center: [3800000.0, 4700000.0],
        zoom: 6.5,
        minZoom: 5.8,
      }),
    });
    this.control = new ControlScaleLine({
      target: this.elementRef.nativeElement,
    });
    this.map.addControl(this.control);
  }

  getCoord(event) {
    var coordinate = this.map.getEventCoordinate(event);
    this.xCoordinate = coordinate[1];
    this.yCoordinate = coordinate[0];
    console.log(coordinate);
    console.log(this.xCoordinate);
    console.log(this.yCoordinate);
    this.tasinmazUpdateForm.controls["xCoordinatesParsel"].setValue(
      this.xCoordinate.toString()
    );
    this.tasinmazUpdateForm.controls["yCoordinatesParsel"].setValue(
      this.yCoordinate.toString()
    );
    // document.getElementById("closeModalButton").click();
    //this.closeModal.nativeElement.click();

    // ref.click();
  }


    OnChangeCitiy(citiyId: Number){
      // if (provinceID) {
      //   console.log(provinceID);
      //   this.countryService.getCountryById(provinceID).subscribe((data) => {
      //     console.log(data);
      //     this.tasinmazUpdateForm.controls.countryID.enable();
      //     this.country = data.data;
      //     this.nb = null;
      //   });
      // } else {
      //   console.log(provinceID);

      //   this.tasinmazUpdateForm.controls.countryID.disable();
      //   this.tasinmazUpdateForm.controls.nbID.disable();
      //   this.country = null;
      //   this.nb = null;
      // }




      console.log("cityid "+citiyId)
      if(citiyId){
      //   this.tasinmazService.GetDistricts(citiyId).subscribe(
      //       data => {
      //         this.tasinmazUpdateForm.controls.Districts.enable();
      //         this.districts = data;
      //         this.neighbourhoods = null;
      //       }
      //  );
        this.tasinmazService.GetDistricts(citiyId).subscribe((data:Districts[])=> {
          this.tasinmazUpdateForm.controls.Districts.enable();
          this.districts=data;
          this.neighbourhoods = null;
        })

      //  this.tasinmazService.GetDistricts(citiyId).subscribe(
      //   data => this.districts=data
      // );
      }else{
        this.tasinmazUpdateForm.controls.Districts.disable();
        this.tasinmazUpdateForm.controls.Neighbourhoods.disable();
        this.districts=null;
        this.neighbourhoods=null;
      }
   }

   OnChangeDistricts(cityId: Number){
    if(cityId){
      // this.tasinmazService.GetNeighbourhood(cityId).subscribe(
      //     data => {
      //       this.tasinmazUpdateForm.controls.Neighbourhoods.enable();
      //       this.neighbourhoods = data;
      //     }
      // );
      this.tasinmazService.GetNeighbourhood(cityId).subscribe((data:Neighbourhoods[])=> {
        this.tasinmazUpdateForm.controls.Neighbourhoods.enable();
        this.neighbourhoods=data;
      })
    }else{
      this.tasinmazUpdateForm.controls.Neighbourhoods.disable();
      this.neighbourhoods=null;
    }
  }


  onSubmit()
  {
    let ilId = this.tasinmazUpdateForm.value.Cities;
    let ilceId  = this.tasinmazUpdateForm.value.Districts;
    let mahalleId = this.tasinmazUpdateForm.value.Neighbourhoods;
    let adres = this.tasinmazUpdateForm.value.Adres;
    let ada = this.tasinmazUpdateForm.value.Ada;
    let parsel = this.tasinmazUpdateForm.value.Parsel;
    let nitelik = this.tasinmazUpdateForm.value.Nitelik;
    let xCoordinate = this.tasinmazUpdateForm.value.xCoordinatesParsel.toString();
    let yCoordinate=this.tasinmazUpdateForm.value.yCoordinatesParsel.toString();

    this.tasinmazService.addUpdateTasinmaz(this.router.snapshot.params.id,ilId,ilceId,mahalleId,adres,ada,parsel,nitelik,xCoordinate,yCoordinate,this.user.userId).subscribe((res)=>{
      if(res.responseCode==ResponseCode.OK)
      {
        if(this.router.snapshot.params.id>0)
        {
          this.toastr.success("Tasinmaz bilgileri başarıyla güncellendi.");
        }else {
          this.toastr.success("Tasinmaz başarıyla kayıt edildi.");
        }

      }else{
        this.toastr.error("AABirşeyler ters gitti, lütfen tekrar deneyin.");
      }
    },()=>{
      this.toastr.error("Sunucu ile bağlantı kurulumadı. API bağlantınızı kontrol ediniz.");
    })

  }




    get user():User{
      return JSON.parse(localStorage.getItem(Constants.USER_KEY)) as User;
    }


}
