using System.ComponentModel.DataAnnotations;

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