export class Tasinmaz
{
  id:number;
  ilId:number;
  ilceId:number;
  mahalleId:number;
  adres:string;
  parsel:number;
  ada:number;
  nitelik:string;
  xCoordinate:string;
  yCoordinate:string;
  parselCoordinate:string;
  //appUserId:string;

  constructor(id = 0, ilId=0,ilceId=0,mahalleId=0,adres='',parsel=0,ada=0,nitelik='',xCoordinate='',yCoordinate='',parselCoordinate='')
  {
    this.id = id;
    this.ilId = ilId;
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
