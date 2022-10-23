export class Tasinmaz
{
  id:number;
  ilId:number;
  ilAdi:string;
  ilceId:number;
  ilceAdi:string;
  mahalleId:number;
  mahalleAdi:string;
  adres:string;
  parsel:number;
  ada:number;
  nitelik:string;
  xCoordinate:string;
  yCoordinate:string;
  parselCoordinate:string;
  //appUserId:string;

  constructor(id = 0, ilId=0,ilAdi='',ilceId=0,ilceAdi='',mahalleId=0,mahalleAdi='',adres='',parsel=0,ada=0,nitelik='',xCoordinate='',yCoordinate='',parselCoordinate='')
  {
    this.id = id;
    this.ilId = ilId;
    this.mahalleAdi=mahalleAdi;
    this.ilAdi=ilAdi;
    this.ilceAdi=ilceAdi;
    this.ilceId = ilceId;
    this.mahalleId = mahalleId;
    this.adres = adres;
    this.parsel = parsel;
    this.ada = ada;
    this.nitelik = nitelik;
    this.xCoordinate = xCoordinate;
    this.yCoordinate = yCoordinate;
    this.parselCoordinate = parselCoordinate;
  }


}
