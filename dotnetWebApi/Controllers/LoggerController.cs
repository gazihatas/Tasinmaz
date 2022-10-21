using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using dotnetWebApi.IServices;
using dotnetWebApi.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotnetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        public LoggerController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> Get()
        {
            var logger = await _loggerService.GetAll();
            return Ok(logger);
        }

        [HttpPost("Add")]
        public async Task<ActionResult> CreateTasinmaz(CreateLogDTO createLogDto)
        {
            var log = new Log
            {
                UserId = createLogDto.UserId,
                Durum = createLogDto.Durum,
                IslemTipi = createLogDto.IslemTipi,
                Aciklama = createLogDto.Aciklama,
                DateTime = createLogDto.DateTime,
                UserIp = createLogDto.UserIp               
            };
            await _loggerService.Add(log);
            return Ok();
        }
    }
}