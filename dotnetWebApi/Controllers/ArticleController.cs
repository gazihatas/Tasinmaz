using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dotnetWebApi.Enums;
using dotnetWebApi.IServices;
using dotnetWebApi.Models;
using dotnetWebApi.Models.BindingModel;
using dotnetWebApi.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.BindingModel;

namespace dotnetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService, IWebHostEnvironment appEnvironment)
        {
            _articleService = articleService;
            _appEnvironment = appEnvironment;
        }


        [HttpPost("AddUpdateArticle")]
        public async Task<object> AddUpdateArticle([FromBody] AddUpdateArticle model)
        {
            try
            {
                if(model == null || model.Title.Length<1)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }

                var result = await _articleService.AddUpdateArticle(model.Id, model.Title, model.Body, model.AppUserId, model.Publish);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,(model.Id >0?"Kayıt Edilen Güncelleme":"Yeni kayıt eklendi"),result));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }


        [Authorize(Roles="Admin,User")]    
        [HttpPost("DeleteArticle")]
        public async Task<object> DeleteArticle([FromBody] DeleteArticleBindingModel model)
        {
            try
            {
                if(model.Id < 1)  
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }

                var result=await _articleService.DeleteArticle(model.Id);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"Kayıt silindi.",result));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }

        [AllowAnonymous]    
        [HttpGet("GetArticleList")]
        public async Task<object> GetArticleList([FromQuery] string AuthorId)
        {
            try
            {
                /*
                if(AuthorId.Length < 3)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }

                var result=await _articleService.GetAllArticle(AuthorId);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",result));
            
                */

                List<ArticleDTO> articleList  = new List<ArticleDTO>();
                if(AuthorId.Length >3)
                {
                    //articleList = await _articleService.GetAllArticle(AuthorId);
                     articleList = await _articleService.GetAllArticle(x=>x.AppUserId==AuthorId && x.Publish);
                }
                else
                {
                    //articleList = await _articleService.GetAllArticle(AuthorId);
                     articleList = await _articleService.GetAllArticle(x=>x.Publish);
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",articleList));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }

        [Authorize(Roles="Admin,User")]    
        [HttpGet("GetArticleListForAdmin")]
        public async Task<object> GetArticleListForAdmin([FromQuery] string AuthorId)
        {
            try
            {
                if(AuthorId.Length < 3)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }

                //var result=await _articleService.GetAllArticle(AuthorId);
                var result=await _articleService.GetAllArticle(x=>x.AppUserId==AuthorId);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",result));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }



 
        [Authorize(Roles = "Admin,User")]
        [HttpPost("ImageUpload")]
        public async Task<object> ImageUpload()
        {
            try
            {
                IFormFile file = HttpContext.Request.Form.Files[0];
                string folderPath="/wwwroot/articleImages/";
                var baseUrl= Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                int total;
                try
                {
                    total = HttpContext.Request.Form.Files.Count;
                }
                catch (Exception)
                {
                    return  await Task.FromResult(new{ error = new {message = "Dosya yükleme işlemi başarısız."}});
                }

                if(total==0)
                {
                    return await Task.FromResult(new{ message = "Hiçbir dosya gönderilmedi."});
                }
                if(!Directory.Exists(baseUrl))
                {
                    return await Task.FromResult(new{ message = "Klasör yok." });
                }

                string fileName= file.FileName;
                if(fileName=="")
                {
                    return  await Task.FromResult(new{ error = new {message = "Dosya yükleme işlemi başarısız."}});
                }
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string newPath = baseUrl + newFileName;
                
                using(var stream = System.IO.File.Create(newPath))
                {
                    await file.CopyToAsync(stream);
                }

                string imageUrl = "https://localhost:5001/articleImages/"+newFileName;

                return await Task.FromResult(new {url = imageUrl});
                

            }
            catch (Exception exception)
            {
                return await Task.FromResult(new { error = new { message = exception.Message} });
            }
        }


    }
}