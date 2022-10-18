import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleService } from 'src/app/services/article.service';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.scss']
})
export class ArticleDetailComponent implements OnInit {

  public articleId:number=0;
  constructor(private activatedRoute:ActivatedRoute, private articleService:ArticleService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params)=>{
      this.articleId=params['id'];
      if(this.articleId=params['id'])
      {
       // this.getArticleById(this.articleId);

      }
    })
  }

  // getArticleById(id:number){
  //   this.articleService.getArticleById(id).subscribe((res)=>{
  // }

}
