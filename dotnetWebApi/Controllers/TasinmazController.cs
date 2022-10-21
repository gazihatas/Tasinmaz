using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using dotnetWebApi.Data;
using dotnetWebApi.IServices;
using dotnetWebApi.Model.DTO;
using IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasinmazController : ControllerBase
    {
         private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoggerService _logger;
        private readonly ITasinmazService _tasinmazRepository;
        public TasinmazController(ITasinmazService tasinmazRepository, ILoggerService logger, UserManager<AppUser> userManager, AppDBContext context)
        {
            _tasinmazRepository = tasinmazRepository;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tasinmaz>> GetUser(int id)
        {
            var tasinmaz = await _tasinmazRepository.Get(id);
            if (tasinmaz == null)
                return NotFound("Bu bilgilerde bir kullanıcı yok");

            return Ok(tasinmaz);
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasinmaz>>> GetTasinmaz()
        {
            var tasinmaz = await _tasinmazRepository.GetAll();
            return Ok(tasinmaz);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> CreateTasinmaz(CreateTasinmazDTO createTasinmazDto)
        {

            // var claimsIdentity = this.User.Identity as ClaimsIdentity;
            // var userId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserMenuId").Value);
            
            
            
            
            await _logger.Add(
                new Log{
                            UserId= createTasinmazDto.UserId,
                            Durum="Başarılı",
                            Aciklama ="Taşınmaz Ekleme Başarılı bir şekilde gerçekleşti",
                            IslemTipi="Taşınmaz Ekleme",
                            DateTime= DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                            UserIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        }
                );        
            var tasinmaz = new Tasinmaz
            {
                IlId = createTasinmazDto.Il,
                IlceId = createTasinmazDto.Ilce,
                MahalleId = createTasinmazDto.MahalleId,
                Ada = createTasinmazDto.Ada,
                Parsel = createTasinmazDto.Parsel,
                Nitelik = createTasinmazDto.Nitelik,
                XCoordinate = (string)createTasinmazDto.XCoordinate,
                YCoordinate = (string)createTasinmazDto.YCoordinate,
                // Coordinates =createTasinmazDto.Coordinates
                ParselCoordinate = createTasinmazDto.ParselCoordinate
            };
            await _tasinmazRepository.Add(tasinmaz);
            return Ok();
        }

          [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            // var claimsIdentity = this.User.Identity as ClaimsIdentity;
            // var userId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserMenuId").Value);
            await _tasinmazRepository.Delete(id);
            await _logger.Add(
                      new Log{
                          Durum="Başarılı",
                          Aciklama ="Taşınmaz Silme Başarılı bir şekilde gerçekleşti",
                          IslemTipi="Taşınmaz Silme",
                          DateTime= DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                          UserIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        }
                  );    
            return Ok();
        }


        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateTasinmaz(int id, UpdateTasinmazDTO updateTasinmazDto)
        {
            // var userId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserMenuId").Value);

            Tasinmaz tasinmaz = new()
            {
                id = id,
                IlId = updateTasinmazDto.IlId,
                IlceId = updateTasinmazDto.IlceId,
                MahalleId = updateTasinmazDto.MahalleId,
                Ada = updateTasinmazDto.Ada,
                Parsel = updateTasinmazDto.Parsel,
                Nitelik = updateTasinmazDto.Nitelik
            };
            await _logger.Add(
                      new Log{
                          Durum="Başarılı",
                          Aciklama ="Taşınmaz Güncelleme Başarılı bir şekilde gerçekleşti",
                          IslemTipi="Taşınmaz Güncelleme",
                          DateTime= DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"),
                          UserIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        }
                  );  
            await _tasinmazRepository.Update(tasinmaz);
            return Ok();
        }

        [HttpGet("Cities")]
        public async Task<ActionResult<IEnumerable<Il>>> GetCities()
        {
            var cities = await _tasinmazRepository.GetAllCities();
            return Ok(cities);
        }
      
        [HttpGet("Districts/{id}")]
        public async Task<ActionResult<IEnumerable<Ilce>>> GetDistricts(int id)
        {
            var districts = await _tasinmazRepository.GetAllDistricts(id);
            return Ok(districts);
        }
        

        [HttpGet("Neighbourhood/{id}")]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetNeighbourhood(int id)
        {
            
            var neighbourhood = await _tasinmazRepository.GetAllNeighbourhood(id);
            return Ok(neighbourhood);
        }

        // public List<Object> list = new List<Object>();
        //  [HttpGet("Parsel/{geomwkt}")]
        //  public async Task<ActionResult<String>> GetParsel(string geomwkt){
        //      List<KadastroParselModel> donus = null;
        //     // DbGeography dbg = DbGeography.FromText(geomwkt);
        //     using (var httpClient = new HttpClient(_clientHandler)){
        //        var response = httpClient.GetAsync(url + geomwkt).Result;
        //        if(response.StatusCode==System.Net.HttpStatusCode.OK){
                
        //          donus = JsonConvert.DeserializeObject<List<KadastroParselModel>>(response.Content.ReadAsStringAsync().Result);
        //        }
        //     }
        //     System.Console.WriteLine(list.ToArray());
        //      return Ok(donus);
        // }

        

    }

}