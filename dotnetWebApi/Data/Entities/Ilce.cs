using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Ilce
    {
       [Key]
        public int Ilceid { get; set; }
        public string Ilcename { get; set; }
       
        
        public int IlId { get; set; }
        public Il Il { get; set; }
    }
}