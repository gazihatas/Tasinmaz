using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entities;
using dotnetWebApi.Models.DTO;

namespace dotnetWebApi.IServices
{
    public interface IArsaService
    {
        //ilId
        Task<Tasinmaz> AddUpdateTasinmaz(
                                        int id, 
                                        int IlId,
                                        int IlceId,
                                        int MahalleId,
                                        string Adres,
                                        int Parsel,
                                        int Ada,
                                        string Nitelik,
                                        string XCoordinate,
                                        string YCoordinate,
                                        string ParselCoordinate,
                                        string authorId
                                        );
        
        Task<bool> DeleteTasinmaz(int id);

        //Task<List<ArticleDTO>> GetAllArticle(string authorId);

        Task<List<TasinmazDTO>> GetAllTasinmaz(string authorId);
        Task<List<Il>> GetAllSehir();
        Task<List<Ilce>> GetAllIlce(int id);
        Task<List<Mahalle>> GetAllMahalle(int id);
        
    }
}
    // public interface IArticleService
    // {
    //     Task<Article> AddUpdateArticle(int id, string title, string content, string authorId, bool publish);
        
    //     Task<bool> DeleteArticle(int id);

    //     //Task<List<ArticleDTO>> GetAllArticle(string authorId);

    //     Task<List<ArticleDTO>> GetAllArticle(Expression<Func<Article,bool>> query);
    // }
