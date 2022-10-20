using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Mahalle
    {
        [Key]
        public int MahalleId { get; set; }
        public string MahalleName { get; set; }
        
        public int IlceId { get; set; }
        public  Ilce Ilce { get; set; }

       
    }
}