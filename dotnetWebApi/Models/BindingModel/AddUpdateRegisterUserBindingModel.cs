using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetWebApi.Models.BindingModel
{
    public class AddUpdateRegisterUserBindingModel
    {
      
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  
        public string  Role { get; set; }
    }
}