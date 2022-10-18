import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/Helper/constants';
import { Article } from 'src/app/Models/article';
import { User } from 'src/app/Models/user';
import { ArticleService } from 'src/app/services/article.service';

@Component({
  selector: 'app-all-article-list-public',
  templateUrl: './all-article-list-public.component.html',
  styleUrls: ['./all-article-list-public.component.scss']
})
export class AllArticleListPublicComponent implements OnInit {

  public articleList:Article[] = [];
  constructor(
    private articleService:ArticleService,
    private modalService:BsModalService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    console.log("all-user-management run");
    //this.getAllArticle();
  }

  // getAllArticle()
  // {
  //   let userId=this.isUserlogin?this.user.userId:"0";
  //   this.articleService.getPublishedArticles(this.user?.userId).subscribe((data:Article[]) => {
  //     this.articleList=data;
  //   })
  // }

  get user():User{
    return JSON.parse(localStorage.getItem(Constants.USER_KEY)) as User;
  }

  get isUserlogin()
  {
    const user = localStorage.getItem(Constants.USER_KEY);
    return user && user.length > 0;
  }

}
