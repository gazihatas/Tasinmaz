using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entities;
using dotnetWebApi.Models.DTO;

namespace dotnetWebApi.IServices
{
    public interface IArticleService
    {
        Task<Article> AddUpdateArticle(int id, string title, string content, string authorId, bool publish);
        
        Task<bool> DeleteArticle(int id);

        //Task<List<ArticleDTO>> GetAllArticle(string authorId);

        Task<List<ArticleDTO>> GetAllArticle(Expression<Func<Article,bool>> query);
    }
}