using dotnetWebApi.Data.Entities;
using dotnetWebApi.Enums;
using dotnetWebApi.Model.DTO;
using dotnetWebApi.Models;
using dotnetWebApi.Models.BindingModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
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
        private readonly JWTConfig _jWTConfig;


        public UserController(
            ILogger<UserController> logger,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IOptions<JWTConfig> jwtConfig
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTConfig = jwtConfig.Value;
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
                     //return await Task.FromResult("Kullanıcı zaten kayıtlı.");
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK,"Kullanıcı zaten kayıtlı.",null));
                }

                //return await Task.FromResult(string.Join(",",result.Errors.Select(x => x.Description).ToArray()));
                return await Task.FromResult(new ResponseModel(ResponseCode.Error,"",result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                
                //return await Task.FromResult(ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUser()
        {
            try
            {
                var users = _userManager.Users.Select(x => new UserDTO(x.FullName,x.Email,x.UserName,x.DateCreated));
                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",users));

            }
            catch (Exception ex)
            {
                
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] loginBindingModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false,false);
                    if(result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var user = new UserDTO(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated);
                        user.Token= GenerateToken(appUser);
                        //return await Task.FromResult(user);
                        return await Task.FromResult(new ResponseModel(ResponseCode.OK, "",user));

                    }
                }

                
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "mail adresi veya şifre geçersiz.",null));
            }
            catch (Exception ex)
            {
               return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
        }


        private string GenerateToken(AppUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new []{
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId,user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience=_jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };

            var token=jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }



    }
}