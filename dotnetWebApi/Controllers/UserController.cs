using Data.Entities;
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
using Models.BindingModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTConfig _jWTConfig;


        public UserController(
            ILogger<UserController> logger,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IOptions<JWTConfig> jwtConfig,
            RoleManager<IdentityRole> roleManager
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTConfig = jwtConfig.Value;
            _roleManager = roleManager;
        }
    
        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model)
        {
            try
            {
                if(!await _roleManager.RoleExistsAsync(model.Role))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Role does not exist",null));
                }

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
                    var tempUser = await _userManager.FindByEmailAsync(model.Email);
                    await _userManager.AddToRoleAsync(tempUser,model.Role);

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

        /// <summary>
        ///Admin Rolu ile Tüm kullanıcıları veritabanından getirir.
        /// </summary>
        [Authorize(Roles="Admin")]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUser()
        {
            try
            {
                List<UserDTO> allUserDTO=new List<UserDTO>();
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    // allUserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role));
                    allUserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role,user.Id));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",allUserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
        }

        /// <summary>
        ///Sadece User rölüne sahip kullanıcıları veritabanından getirir.
        /// </summary>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles ="User,Admin")]
        [HttpGet("GetUserList")]
        public async Task<object> GetUserList()
        {
            try
            {
                //Rolleme olmadan önce kullanıcıları listeleme
                //var users = _userManager.Users.Select(x => new UserDTO(x.FullName,x.Email,x.UserName,x.DateCreated));

                List<UserDTO> allUserDTO = new List<UserDTO>();
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    if(role=="User")
                    {
                       //allUserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role)); 
                        allUserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role,user.Id));  
                    }
                     
                }            

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",allUserDTO));

            }
            catch (Exception ex)
            {
                
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
        }




        /// <summary>
        /// Giriş 
        /// </summary>
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
                        var role = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();

                        //var user = new UserDTO(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated,role);
                        var user = new UserDTO(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated,role,appUser.Id);
                        user.Token= GenerateToken(appUser,role);
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

        /// <summary>
        ///Mevcut Rolleri Listeler.
        /// </summary>
        //[Authorize(Roles="Admin")]
        [HttpGet("GetRoles")]
        public async Task<object> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Select(x=>x.Name).ToList();

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"",roles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
        }

        /// <summary>
        ///Mevcut rol kayıtlarını getirir.
        /// </summary>
        // [Authorize(Roles="Admin")]
        [HttpPost("AddRole")]
        public async Task<object> AddRole([FromBody] AddRoleBindingModel model)
        {
            try
            {
                if(model == null || model.Role =="")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor",null));
                }

                if(await _roleManager.RoleExistsAsync(model.Role))
                {
                     return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Rol zaten var.",null));
                }

                var role = new IdentityRole();
                role.Name = model.Role;
                var result = await _roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Rol başarıyla eklendi.",null));
                }

                return Task.FromResult(new ResponseModel(ResponseCode.Error, "Birşeyler ters gitti, lütfen daha sonra tekrar deneyiniz.",null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
            
        }

        /// <summary>
        ///Admin Rolu ile Tüm kullanıcıları veritabanından getirir.
        /// </summary>
        [HttpPut("EditUser")]
        public async Task<object> EditUser([FromBody] EditUserViewModel model)
        {
            try
            {    //var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    // allUserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role));
                var user = await _userManager.FindByIdAsync(model.Id);
               // var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                
                if(user == null)  
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }
                else
                {
                    // List<UserDTO> allUserDTO=new List<UserDTO>();
                    // var users = _userManager.Users.ToList();
                    // foreach (var user in users)
                    // {
                    //     var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    //     // allUserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role));
                    //     allUserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role,user.Id));
                    // }




                    //allUserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, role,user.Id));
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    user.FullName = model.FullName;
                    user.Email = model.Email;
                    user.PasswordHash = model.Password;
                    role=model.Role;
                    // var resultRole = await _roleManager.CreateAsync(role);
                    var result = await _userManager.UpdateAsync(user);
                    
                    if(result.Succeeded)
                    {
                     return await Task.FromResult(new ResponseModel(ResponseCode.OK,"Kayıt güncellendi.",user));
                    }
                    
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error,"Kayıt güncellendi.",user));
                }


            }
             catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
        }


        /// <summary>
        ///Kullanıcı Silme.
        /// </summary>
        [Authorize(Roles="Admin")]    
        [HttpPost("DeleteUser")]
        public async Task<object> DeleteUser([FromBody] string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);    

                if(user == null)  
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Parametre girmeniz bekleniyor.",null));
                }
                //var book = _context.Books.Include(x => x.Author).SingleOrDefault(x => x.Author.Id == AuthorId)
                

                 var result=await _userManager.DeleteAsync(user);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK,"Kayıt silindi.",user));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message,null));
            }
           
        }




        private string GenerateToken(AppUser user, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new []{
                    //new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId,user.Id),
                    //otomatik olarak artan int UserId 
                    //new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId,user.UserId.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId,user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Role,role),

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