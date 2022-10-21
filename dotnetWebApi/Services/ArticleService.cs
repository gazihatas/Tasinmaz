using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Data;
using Data.Entities;
using dotnetWebApi.IServices;
using dotnetWebApi.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace dotnetWebApi.Services
{
    public class ArticleService : IArticleService
    {
        private readonly AppDBContext _context;
        public ArticleService(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Article> AddUpdateArticle(int id, string title, string body, string authorId, bool publish)
        {
            var tempArticle = _context.Articles.FirstOrDefault(x =>x.Id == id);

            if(tempArticle==null)
            {
                tempArticle = new Article(){
                    Title = title,
                    Body = body,
                    AppUserId= authorId,
                    Created=DateTime.UtcNow,
                    Modified = new DateTime(1900),
                    Publish = publish,
                };

                await _context.Articles.AddAsync(tempArticle);
                await _context.SaveChangesAsync();
                return tempArticle;
            }
            tempArticle.Title = title;
            tempArticle.Body= body;
            tempArticle.Publish = publish;
            tempArticle.Modified = DateTime.UtcNow;

            _context.Update(tempArticle);
            await _context.SaveChangesAsync();
            return tempArticle;
        }

        public async Task<bool> DeleteArticle(int id)
        {
             var tempArticle = _context.Articles.FirstOrDefault(x =>x.Id == id);
             if(tempArticle==null)
             {
                return await Task.FromResult(true);
             }

             _context.Articles.Remove(tempArticle);
             await _context.SaveChangesAsync();
             return await Task.FromResult(true);
        }

       // public async Task<List<ArticleDTO>> GetAllArticle(string authorId)
       public async Task<List<ArticleDTO>> GetAllArticle(Expression<Func<Article,bool>> query)
        {
           return await(
               // from article in _context.Articles 
                from article in _context.Articles.Where(query)
               // where article.AppUserId==authorId
                select new ArticleDTO() {
                    Id=article.Id,
                    Title = article.Title,
                    Body=article.Body,
                    Publish=article.Publish,
                    AppUserId=article.AppUserId,
                    AuthorName=article.AppUser.FullName,
                    CreatedDate=article.Created,
                    ModifiedDate=article.Modified,

                }
           ).ToListAsync();
        }

        
    }
}