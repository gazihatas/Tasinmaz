import { Component, ElementRef, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { Constants } from 'src/app/Helper/constants';
import { Tasinmaz } from 'src/app/Models/tasinmaz';
import { User } from 'src/app/Models/user';
import { Cities } from 'src/app/Models/cities';
import { TasinmazService } from 'src/app/services/tasinmaz.service';
import { Districts } from 'src/app/Models/districts';
import { Neighbourhoods } from 'src/app/Models/neighbourhoods';
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
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})



export class AddComponent implements OnInit {


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
  control: ControlScaleLine;
  map: Map;
  view: View;
  deger:string;
  boslukKontrolHataMessage='Bu alan zorunludur.';
  scaleType = "scaleline";
  scaleBarSteps = 4;
  scaleBarText = true;

  constructor(
    private formBuilder:FormBuilder,
    private tasinmazService:TasinmazService,
    private toastr:ToastrService,
    private elementRef: ElementRef
    ) { }
  public tasinmazAddForm;

  ngOnInit()
  {
    this.initilizeMap();
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

      this.tasinmazAddForm=this.formBuilder.group({
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

    // this.IntilazeMap();
    // this.IntilazeMapParsel();
  }

//   IntilazeMap(){
//     this.view = new View({
//         center: [3876682.9740679907, 4746346.604388495],
//         zoom: 6.5,
//         minZoom:5.8
//       });
//       console.log("mao")

//     this.map = new Map({
//      view:this.view,
//       layers: [
//         new Tile({
//           source: new XYZ({
//             url: 'http://mt0.google.com/vt/lyrs=y&hl=en&x={x}&y={y}&z={z}',
//         }),zIndex:-12312
//         }),
//       ],
//       target: 'ol-map'
//     });

//   }
//   IntilazeMapParsel(){
//     this.view = new View({
//         center: [3876682.9740679907, 4746346.604388495],
//         zoom: 6.5,
//         // minZoom:5.8
//       });
//       console.log("mao")
//     this.mapParsel = new Map({
//      view:this.view,
//       layers: [
//         new Tile({
//           source: new XYZ({
//             url: 'http://mt0.google.com/vt/lyrs=y&hl=en&x={x}&y={y}&z={z}',
//         }),zIndex:-5444
//         }),
//       ],
//       target: 'ol-map-parsel'
//     });
//   }
//   GetCoord(event: any){
//     if(confirm("Koordinat?? Almak istedi??inize eminmisiniz??")) {
//        var coordinate = this.map.getEventCoordinate(event);
//        this.xCoordinate=transform(coordinate, 'EPSG:3857', 'EPSG:4326')[1];
//        this.yCoordinate=transform(coordinate, 'EPSG:3857', 'EPSG:4326')[0];
//        let ref = document.getElementById('cancel')
//        ref.click();
//     }
//  }


//  parsel:any;
//  vector:any;
//   GetCoordParsel(event: any){

//     if(confirm("Parseli Se??mek istedi??inize eminmisiniz??")) {
//       var coordinate = this.mapParsel.getEventCoordinate(event);
//       var s =  transform(coordinate, 'EPSG:3857', 'EPSG:4326');


//       console.log(s);
//       var feature = new Feature({
//         labelPoint: new Point(s),
//         name: 'My Polygon'
//       });

//       feature.setGeometryName('labelPoint');
//       var point = feature.getGeometry();

//       var format = new WKT(),
//       wkt = format.writeGeometry(point);
//       console.log(wkt.toString());






//       this.tasinmazService.GetParsel(wkt.toString()).subscribe(data => this.parsel=data);

//      var formats = new WKT()

//     //  var featureGeo = formats.readFeature(this.parsel[0]["geomWkt"], {

//     //  });const styles = [
//   /* We are using two different styles for the polygons:
//    *  - The first style is for the polygons themselves.
//    *  - The second style is to draw the vertices of the polygons.
//    *    In a custom `geometry` function the vertices of a polygon are
//    *    returned as `MultiPoint` geometry, which will be used to render
//    *    the style.
//    */
//   var stt = [  new Style({
//     stroke: new Stroke({
//       color: 'red',
//       width: 3,
//     }),
//     fill: new Fill({
//       color: 'rgba(0, 0, 255, 0.1)',
//     }),
//   }),
//   new Style({
//     image: new CircleStyle({
//       radius: 5,
//       fill: new Fill({
//         color: 'orange',
//       }),
//     }),
//   }),
// ];
// try {


//   var featureGeo = formats.readFeature(this.parsel[0]["geomWkt"], {
//         dataProjection: 'EPSG:4326',
//         featureProjection: 'EPSG:3857',
//      });
//      this.mapParsel.removeLayer(this.vector);

//      var sourr = new VectorSource({
//       features: [featureGeo],
//     });
//       this.vector = new VectorLayer({
//        source: sourr,style:stt
//      });

//     this.mapParsel.addLayer(this.vector);
//     this.tasinmazService.tasinmazAddForm.controls['xCoordinatesParsel'].setValue(s[0]);
//     this.tasinmazService.tasinmazAddForm.controls['yCoordinatesParsel'].setValue(s[1]);
//     this.tasinmazService.tasinmazAddForm.controls['Parsel'].setValue(this.parsel[0]["parselNo"]);
//     this.tasinmazService.tasinmazAddForm.controls['Nitelik'].setValue(this.parsel[0]["cins"]);
//     this.tasinmazService.tasinmazAddForm.controls['Ada'].setValue(this.parsel[0]["adaNo"]);
//     console.log(this.parsel[0])
//     console.log(this.parsel[0]["parselNo"])
//     console.log(this.parsel[0]["adaNo"])

// } catch (error) {
//   console.error("hataaa")
//   this.toastr.error('L??tfen Tan??mlanan yerlerden parsel se??meyi deneyin!','Hata!');

// }

//     //   var pPath = {
//     //     'type': 'Polygon',
//     //     'coordinates': this.parsel[0]["geomWkt"]
//     // };

//     // var fPath = {
//     //     'type': 'Feature',
//     //     'geometry': pPath
//     // };

//     // var svPath = new Vector({
//     //     features: new GeoJSON().readFeatures(fPath, {featureProjection: this.map.getView().getProjection()})
//     // });

//     // var lvPath = new Vector({
//     //     source: svPath,
//     // });
//     // var geometry = new MultiPolygon([
//     //   this.parsel[0]["geomWkt"]
//     // ]);
//     // geometry.transform('EPSG:4326', 'EPSG:3857');

//     // var vectorLayer = new Vector({
//     //   source: new Vector({
//     //      features: [new Feature({
//     //          geometry: geometry
//     //      })]
//     //     })
//     // });

//     //this.mapParsel.getView().fit(sourr.getExtent());
//     //console.log(this.parsel[0]["geomWkt"]);
//     }
//   }


//   OnlyNumbersAlowed(event):boolean{
//     const charCode = (event.which)?event.which:event.keyCode;
//     if(charCode>31 && (charCode<48 || charCode>57)){
//       this.toastr.warning('L??tfen Numara girdi??inize emin olun','Uyar??!!')
//       return false;
//     }

//     return true;
//    }
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
  this.tasinmazAddForm.controls["xCoordinatesParsel"].setValue(
    this.xCoordinate.toString()
  );
  this.tasinmazAddForm.controls["yCoordinatesParsel"].setValue(
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
    //     this.tasinmazAddForm.controls.countryID.enable();
    //     this.country = data.data;
    //     this.nb = null;
    //   });
    // } else {
    //   console.log(provinceID);

    //   this.tasinmazAddForm.controls.countryID.disable();
    //   this.tasinmazAddForm.controls.nbID.disable();
    //   this.country = null;
    //   this.nb = null;
    // }




    console.log("cityid "+citiyId)
    if(citiyId){
    //   this.tasinmazService.GetDistricts(citiyId).subscribe(
    //       data => {
    //         this.tasinmazAddForm.controls.Districts.enable();
    //         this.districts = data;
    //         this.neighbourhoods = null;
    //       }
    //  );
      this.tasinmazService.GetDistricts(citiyId).subscribe((data:Districts[])=> {
        this.tasinmazAddForm.controls.Districts.enable();
        this.districts=data;
        this.neighbourhoods = null;
      })

    //  this.tasinmazService.GetDistricts(citiyId).subscribe(
    //   data => this.districts=data
    // );
    }else{
      this.tasinmazAddForm.controls.Districts.disable();
      this.tasinmazAddForm.controls.Neighbourhoods.disable();
      this.districts=null;
      this.neighbourhoods=null;
    }
 }

 OnChangeDistricts(cityId: Number){
  if(cityId){
    // this.tasinmazService.GetNeighbourhood(cityId).subscribe(
    //     data => {
    //       this.tasinmazAddForm.controls.Neighbourhoods.enable();
    //       this.neighbourhoods = data;
    //     }
    // );
    this.tasinmazService.GetNeighbourhood(cityId).subscribe((data:Neighbourhoods[])=> {
      this.tasinmazAddForm.controls.Neighbourhoods.enable();
      this.neighbourhoods=data;
    })
  }else{
    this.tasinmazAddForm.controls.Neighbourhoods.disable();
    this.neighbourhoods=null;
  }
}


onSubmit()
{
  let ilId = this.tasinmazAddForm.value.Cities;
  let ilceId  = this.tasinmazAddForm.value.Districts;
  let mahalleId = this.tasinmazAddForm.value.Neighbourhoods;
  let adres = this.tasinmazAddForm.value.Adres;
  let ada = this.tasinmazAddForm.value.Ada;
  let parsel = this.tasinmazAddForm.value.Parsel;
  let nitelik = this.tasinmazAddForm.value.Nitelik;
  let xCoordinate = this.tasinmazAddForm.value.xCoordinatesParsel.toString();
  let yCoordinate=this.tasinmazAddForm.value.yCoordinatesParsel.toString();

  this.tasinmazService.addUpdateTasinmaz(this.tasinmazId,ilId,ilceId,mahalleId,adres,ada,parsel,nitelik,xCoordinate,yCoordinate,this.user.userId).subscribe((res)=>{
    if(res.responseCode==ResponseCode.OK)
    {
      if(this.tasinmazId>0)
      {
        this.toastr.success("Tasinmaz bilgileri ba??ar??yla g??ncellendi.");
      }else {
        this.toastr.success("Tasinmaz ba??ar??yla kay??t edildi.");
      }

    }else{
      this.toastr.error("AABir??eyler ters gitti, l??tfen tekrar deneyin.");
    }
  },()=>{
    this.toastr.error("Sunucu ile ba??lant?? kurulumad??. API ba??lant??n??z?? kontrol ediniz.");
  })

}




  get user():User{
    return JSON.parse(localStorage.getItem(Constants.USER_KEY)) as User;
  }






}
