using dotnetWebApi.Data.Entities;
using dotnetWebApi.Models.BindingModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
    
        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model)
        {
            try
            {
                 var user = new AppUser()
                {
                    FullName=model.FullName,
                    Email=model.Email,
                    UserName=model.Email,
                    DateCreated=DateTime.UtcNow,
                    DateModified=DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    return await Task.FromResult("Kullanıcı zaten kayıtlı.");
                }

                return await Task.FromResult(string.Join(",",result.Errors.Select(x => x.Description).ToArray()));

            }
            catch (Exception ex)
            {
                
                return await Task.FromResult(ex.Message);
            }
           
        }

    }
}