import { Component, OnInit } from '@angular/core';
import { Constants } from 'src/app/Helper/constants';
import { Tasinmaz } from 'src/app/Models/tasinmaz';
import { User } from 'src/app/Models/user';
import { TasinmazService } from 'src/app/services/tasinmaz.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  constructor(private tasinmazService:TasinmazService) { }
  fileName = 'TasinmazKayitTablosu.xlsx';
  public tasinmazList:Tasinmaz[] = [];
  totalLength:any;
  page:number = 1;
  ngOnInit(): void {
    console.log("on list Tasinmaz")
    this.getAllTasinmaz()
  }

  getAllTasinmaz()
  {
    this.tasinmazService.getTasinmazsByAuthorId(this.user.userId).subscribe((data:Tasinmaz[]) => {
      this.tasinmazList=data;
    })

  }

  // getAllUser()
  // {
  //   this.tasinmazService.getAllTasinmaz().subscribe((data:Tasinmaz[]) => {
  //     this.tasinmazList=data;

  //   })
  // }

  get user():User{
    return JSON.parse(localStorage.getItem(Constants.USER_KEY)) as User;
  }

  Exportexcel(){
    let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);

    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Tasinmaz-Listesi-Sheet1');

    XLSX.writeFile(wb, this.fileName);
  }

}
