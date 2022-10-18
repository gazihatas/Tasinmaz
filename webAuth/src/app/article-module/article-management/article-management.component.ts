import { Component, OnInit } from '@angular/core';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { Constants } from 'src/app/Helper/constants';
import { AddUpdateArticleComponent } from 'src/app/modal-components/add-update-article/add-update-article.component';
import { ConfirmModalComponent } from 'src/app/modal-components/confirm-modal/confirm-modal.component';
import { Article } from 'src/app/Models/article';
import { User } from 'src/app/Models/user';
import { ArticleService } from 'src/app/services/article.service';
@Component({
  selector: 'app-article-management',
  templateUrl: './article-management.component.html',
  styleUrls: ['./article-management.component.scss']
})
export class ArticleManagementComponent implements OnInit {

  public articleList:Article[] = [];
  constructor(
    private articleService:ArticleService,
    private modalService:BsModalService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    console.log("all-user-management run");
    this.getAllArticle();
  }

  getAllArticle()
  {
    this.articleService.getArticlesByAuthorId(this.user.userId).subscribe((data:Article[]) => {
      this.articleList=data;
    })
  }

  get user():User{
    return JSON.parse(localStorage.getItem(Constants.USER_KEY)) as User;
  }

  onAddNew()
  {
    const initialState: ModalOptions = {
      initialState: {

      },
      ignoreBackdropClick: true,
      backdrop:true,
      class:'modal-lg'
    };

    const bsModalRef = this.modalService.show(AddUpdateArticleComponent, initialState);
    bsModalRef.content.modalResponse.subscribe((result)=>{
      if(result)
      {
        this.getAllArticle();
      }

    })
  }

  onEdit(tempArticle:Article)
  {
    const initialState: ModalOptions = {
      initialState: {
        headerTitle:'İçerik Güncelle',
        confirmBtnTitle:'Güncelle',
        articleId:tempArticle.id,
        articleTitle:tempArticle.title,
        articleBody:tempArticle.body,
        articleStatus:tempArticle.status
      },
      ignoreBackdropClick: true,
      backdrop:true,
      class:'modal-lg'
    };

    const bsModalRef = this.modalService.show(AddUpdateArticleComponent, initialState);
    bsModalRef.content.modalResponse.subscribe((result)=>{
      if(result)
      {
        this.getAllArticle();
      }

    })
  }

  onDelete(articleId:number)
  {
    console.log("on Delete");
    const initialState: ModalOptions = {
      initialState: {
          message:"Silmek istediğinizden eminmisiniz?",
          confirmTitle:"Evet",
          declineTitle:"Hayır"
      }
    };

    const bsModalRef = this.modalService.show(ConfirmModalComponent, initialState);
    bsModalRef.content.modalResponse.subscribe((result)=>{
      if(result)
      {
        //console.log("Silme durumunu onayla: Evet");
        this.articleService.deleteArticle(articleId).subscribe((res)=>{

          if(res.responseCode==ResponseCode.OK)
          {
            this.toastr.success("İçerik başarıyla silindi.");
            this.getAllArticle();
          } else{
            this.toastr.error("Birşeyler ters gitti, lütfen tekrar deneyin.");
          }

        },()=>{
          this.toastr.error("Birşeyler ters gitti, lütfen tekrar deneyin.");
        })
      }

    })

  }

  onStatusChange(article){
    article.status=!article.status;
    //console.log("status",article.status);

    const initialState: ModalOptions = {
      initialState: {
          message:article.status?"Yayınlamak mı istiyorsunuz??":"Yayından kaldırmak istiyor musunuz?",
          confirmTitle:"Evet",
          declineTitle:"Hayır"
      }
    };

    const bsModalRef = this.modalService.show(ConfirmModalComponent, initialState);
    bsModalRef.content.modalResponse.subscribe((result)=>{
      if(result)
      {
        //console.log("Silme durumunu onayla: Evet");
        this.articleService.addUpdateArticle(article.id,article.title,article.body,article.status,this.user.userId).subscribe((res)=>{

          if(res.responseCode==ResponseCode.OK)
          {
            this.toastr.success(article.status?"İçerik yayınlandı":"İçerik yayından kaldırıldı");
           // this.getAllArticle();
          } else{
            this.toastr.error("Birşeyler ters gitti, lütfen tekrar deneyin.");
          }

        },()=>{
          this.toastr.error("Birşeyler ters gitti, lütfen tekrar deneyin.");
        })
      }else{
        article.status=!article.status;
      }

    })
  }

}
