import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { ResponseCode } from 'src/app/enums/responseCode';
import { Constants } from 'src/app/Helper/constants';
import { User } from 'src/app/Models/user';
import { ArticleService } from 'src/app/services/article.service';

@Component({
  selector: 'app-add-update-article',
  templateUrl: './add-update-article.component.html',
  styleUrls: ['./add-update-article.component.scss']
})
export class AddUpdateArticleComponent implements OnInit {

  public headerTitle:string ='İçerik Ekle';
  public confirmBtnTitle:string ='Ekle';
  public articleTitle:string='';
  public articleStatus:boolean=false;
  public articleBody:string='';
  public modalResponse:Subject<boolean>;
  public addUpdateArticleForm:FormGroup;
  public articleId:number=0;

  constructor(
    private bsModalRef:BsModalRef,
    private formBuilder:FormBuilder,
    private articleService:ArticleService,
    private toastr:ToastrService
    ) { }

  ngOnInit(): void {
    this.modalResponse = new Subject();

    this.addUpdateArticleForm=this.formBuilder.group({
      title:['',[Validators.required]],
      body:['',[Validators.required]]
    });
  }

  confirm()
  {
    console.log("on Confirm title", this.articleTitle, "body =>",this.articleBody);

    this.articleService.addUpdateArticle(this.articleId,this.articleTitle, this.articleBody,this.articleStatus,this.user.userId).subscribe((res)=>{
      if(res.responseCode==ResponseCode.OK)
      {
        if(this.articleId>0)
        {
          this.toastr.success("İçerik başarıyla güncellendi.");
        }else {
          this.toastr.success("İçerik başarıyla kayıt edildi.");
        }

        this.bsModalRef.hide();
        this.modalResponse.next(true);
      }else{
        this.toastr.error("AABirşeyler ters gitti, lütfen tekrar deneyin.");
      }
    },()=>{
      this.toastr.error("Sunucu ile bağlantı kurulumadı. API bağlantınızı kontrol ediniz.");
    })

  }

  decline()
  {
    this.bsModalRef.hide();
    this.modalResponse.next(false);
  }

  get user():User{
    return JSON.parse(localStorage.getItem(Constants.USER_KEY)) as User;
  }

  getFormControl(controlName){
    return this.addUpdateArticleForm.controls[controlName];
  }

}
