import { Component, OnInit } from '@angular/core';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from '../enums/responseCode';
import { ConfirmModalComponent } from '../modal-components/confirm-modal/confirm-modal.component';
import { User } from '../Models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-all-user-management',
  templateUrl: './all-user-management.component.html',
  styleUrls: ['./all-user-management.component.scss']
})
export class AllUserManagementComponent implements OnInit {

  public userList:User[] = [];
  constructor(
    private userService:UserService,
    private modalService:BsModalService,
    private toastr: ToastrService
    ) {

  }

  totalLength:any;
  page:number = 1;

  ngOnInit(): void {
    console.log("all-user-management run");
    this.getAllUser();
  }

  getAllUser()
  {
    this.userService.getAllUser().subscribe((data:User[]) => {
      this.userList=data;

    })
  }

  // onEdit(tempArticle:Article)
  // {
  //   const initialState: ModalOptions = {
  //     initialState: {
  //       headerTitle:'İçerik Güncelle',
  //       confirmBtnTitle:'Güncelle',
  //       articleId:tempArticle.id,
  //       articleTitle:tempArticle.title,
  //       articleBody:tempArticle.body,
  //       articleStatus:tempArticle.status
  //     },
  //     ignoreBackdropClick: true,
  //     backdrop:true,
  //     class:'modal-lg'
  //   };

  //   const bsModalRef = this.modalService.show(AddUpdateArticleComponent, initialState);
  //   bsModalRef.content.modalResponse.subscribe((result)=>{
  //     if(result)
  //     {
  //       this.getAllArticle();
  //     }

  //   })
  // }

  onDelete(userId)
  {
    console.log("on Delete User");
    // const initialState: ModalOptions = {
    //   initialState: {
    //       message:"Kullanıcıyı Silmek istediğinizden eminmisiniz?",
    //       confirmTitle:"Evet",
    //       declineTitle:"Hayır"
    //   }
    // };


   // const bsModalRef = this.modalService.show(ConfirmModalComponent, initialState);
    // bsModalRef.content.modalResponse.subscribe((result)=>{
    //   if(result)
    //   {
    //     //console.log("Silme durumunu onayla: Evet");
    //     this.userService.deleteUser(userId).subscribe((res)=>{

    //       if(res.responseCode==ResponseCode.OK)
    //       {
    //         this.toastr.success("Kullanıcı başarıyla silindi.");
    //         this.getAllUser();
    //       } else{
    //         this.toastr.error("aaaBirşeyler ters gitti, lütfen tekrar deneyin.");
    //       }

    //     },()=>{
    //       this.toastr.error("CCBirşeyler ters gitti, lütfen tekrar deneyin.");
    //     })
    //   }

    // })

    this.userService.deleteUser(userId).subscribe(data => {

          if(data.responseCode==ResponseCode.OK)
          {
            this.toastr.success("Kullanıcı başarıyla silindi.");
            this.getAllUser();
          } else{
            this.toastr.error("aaaBirşeyler ters gitti, lütfen tekrar deneyin.");
          }

        },()=>{
          this.toastr.error("CCBirşeyler ters gitti, lütfen tekrar deneyin.");
        })
    }




}
