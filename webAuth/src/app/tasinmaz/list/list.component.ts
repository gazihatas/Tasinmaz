import { Component, OnInit } from '@angular/core';
import { Constants } from 'src/app/Helper/constants';
import { Tasinmaz } from 'src/app/Models/tasinmaz';
import { User } from 'src/app/Models/user';
import { TasinmazService } from 'src/app/services/tasinmaz.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  public tasinmazList:Tasinmaz[] = [];
  totalLength:any;
  page:number = 1;
  constructor(private tasinmazService:TasinmazService) { }

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

}
