using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Il
    {
        [Key]
        public int IlId { get; set; }
        public string IlName { get; set; }
    }
}