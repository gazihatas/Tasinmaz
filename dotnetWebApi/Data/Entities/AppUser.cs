using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class AppUser : IdentityUser
    {
        
      
        public string FullName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        
        public List<Article> Articles { get; set; }
        public List<Tasinmaz> Tasinmazs { get; set; }

     
   // public Guid User { get; set; }
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public int UserId { get; set; }
    }
}