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
    public class ArsaController : ControllerBase
    {
        private readonly IArsaService _arsaService;
        public ArsaController(IArsaService arsaService)
        {
            _arsaService = arsaService;
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
        public async Task<object> AddUpdateArticle([FromBody] AddUpdateTasinmaz model)
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



    }
}