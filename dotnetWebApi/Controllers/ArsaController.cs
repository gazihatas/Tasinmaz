using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Entities;
using dotnetWebApi.Data;
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
using Newtonsoft.Json;

namespace dotnetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArsaController : ControllerBase
    {
        private string url = "http://apps.odakgis.com.tr:8282/api/megsis/GetParselWithGeomWktAsync/";
        HttpClientHandler _clientHandler = new HttpClientHandler();
        private readonly IArsaService _arsaService;
        private readonly AppDBContext _context;
        private readonly ILoggerService _logger;
        public ArsaController(IArsaService arsaService, ILoggerService logger, AppDBContext context)
        {
            _arsaService = arsaService;
            _logger = logger;
            _context = context;
        }


        // [HttpPost("AddUpdateTasinmaz")]
        // public async Task<object> AddUpdateTasinmaz([FromBody] AddUpdateTasinmaz model)
        // {
        //     try
        //     {
        //         if(model == null || model.IlId <1)
        //         {
        //             return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
        //         }

        //         var result = await _arsaService.AddUpdateTasinmaz(model.id, 
        //                                                         model.IlId,
        //                                                         model.IlceId,
        //                                                         model.MahalleId,
        //                                                         model.Adres,
        //                                                         model.Parsel,
        //                                                         model.Nitelik,
        //                                                         model.XCoordinate,
        //                                                         model.YCoordinate,
        //                                                         model.ParselCoordinate,
        //                                                         model.AppUserId
        //                                                         );

        //         return await Task.FromResult(new ResponseModel(ResponseCode.OK,("Yeni kayıt eklendi"),result));
        //     }
        //     catch (Exception ex)
        //     {
        //         return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
        //     }

        // }


        [HttpPost("AddUpdateTasinmaz")]
        public async Task<object> AddUpdateTasinmaz([FromBody] AddUpdateTasinmaz model)
        {
            try
            {
                
                if(model == null || model.IlId <1)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }

                 var result = await _arsaService.AddUpdateTasinmaz(model.id, 
                                                                model.IlId,
                                                                model.IlceId,
                                                                model.MahalleId,
                                                                model.Adres,
                                                                model.Parsel,
                                                                model.Ada,
                                                                model.Nitelik,
                                                                model.XCoordinate,
                                                                model.YCoordinate,
                                                                model.ParselCoordinate,
                                                                model.AppUserId
                                                                );

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,(model.id >0?"Kayıt Edilen Güncelleme":"Yeni kayıt eklendi"),result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }


        [Authorize(Roles="Admin,User")]    
        [HttpPost("DeleteTasinmaz")]
        public async Task<object> DeleteTasinmaz([FromBody] DeleteArticleBindingModel model)
        {
            try
            {
                if(model.Id < 1)  
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }

                var result=await _arsaService.DeleteTasinmaz(model.Id);
                var user = await _context.Kullanicilar.FindAsync(x => x.);
                await _logger.Add(
                      new Log{
                          UserId= userId,
                          Durum="Başarılı",
                          Aciklama ="Taşınmaz Silme Başarılı bir şekilde gerçekleşti",
                          IslemTipi="Taşınmaz Silme",
                          DateTime= DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                          UserIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        }
                  );    

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"Kayıt silindi.",result));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }
    

        [AllowAnonymous]    
        [HttpGet("GetTasinmazList")]
        public async Task<object> GetTasinmazList([FromQuery] string AuthorId)
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

                List<TasinmazDTO> tasinmazList  = new List<TasinmazDTO>();
                if(AuthorId.Length >3)
                {
                    //articleList = await _articleService.GetAllArticle(AuthorId);
                     tasinmazList = await _arsaService.GetAllTasinmaz(AuthorId);
                }
                // else
                // {
                //     tasinmazList = await _arsaService.GetAllTasinmaz(AuthorId);
                //     //tasinmazList = await _arsaService.GetAllTasinmaz(x=>x.Publish);
                // }

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",tasinmazList));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }

        [Authorize(Roles="Admin,User")]    
        [HttpGet("GetTasinmazListForAdmin")]
        public async Task<object> GetTasinmazListForAdmin([FromQuery] string AuthorId)
        {
            try
            {
                if(AuthorId.Length < 3)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }

                //var result=await _articleService.GetAllArticle(AuthorId);
                var result=await _arsaService.GetAllTasinmaz(AuthorId);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",result));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }


        [HttpGet("Sehirler")]
        public async Task<object> GetSehirler()
        {
            try
            {
                var sehirler = await _arsaService.GetAllSehir();
                // return Ok(sehirler);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",sehirler));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }

        }

        [HttpGet("Cities")]
        public async Task<Object> GetCities()
        {
            var cities = await _arsaService.GetAllSehir();
            return Ok(cities);
        }
      
      
        [HttpGet("Ilceler/{id}")]
        public async Task<object> GetIlceler(int id)
        {
            try
            {
                 var ilceler = await _arsaService.GetAllIlce(id);
                // return Ok(ilceler);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",ilceler));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
        }

        [HttpGet("Districts/{id}")]
        public async Task<object> GetDistricts(int id)
        {
            var districts = await _arsaService.GetAllIlce(id);
            return Ok(districts);
        }
        

        [HttpGet("Mahalleler/{id}")]
        public async Task<object> GetMahalleler( int id)
        {
             try
            {
                var mahalleler = await _arsaService.GetAllMahalle(id);
                //return Ok(neighbourhood);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",mahalleler));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
        }

         [HttpGet("Neighbourhood/{id}")]
        public async Task<object> GetNeighbourhood(int id)
        {
            
            var neighbourhood = await _arsaService.GetAllMahalle(id);
            return Ok(neighbourhood);
        }


        public List<Object> list = new List<Object>();
         [HttpGet("Parsel/{geomwkt}")]
         public async Task<ActionResult<String>> GetParsel(string geomwkt){
             List<KadastroParselModel> donus = null;
            // DbGeography dbg = DbGeography.FromText(geomwkt);
            using (var httpClient = new HttpClient(_clientHandler)){
               var response = httpClient.GetAsync(url + geomwkt).Result;
               if(response.StatusCode==System.Net.HttpStatusCode.OK){
                
                 donus = JsonConvert.DeserializeObject<List<KadastroParselModel>>(response.Content.ReadAsStringAsync().Result);
               }
            }
            System.Console.WriteLine(list.ToArray());
             return Ok(donus);
        }



    }
}