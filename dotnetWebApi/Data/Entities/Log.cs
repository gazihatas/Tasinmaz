using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Log
    {
        [Key]
        public int logid { get; set; }
        public int UserId { get; set; }
        public string Durum { get; set; }
        public string  IslemTipi { get; set; }
        public string Aciklama { get; set; }
        public string DateTime { get; set; }
        public string UserIp { get; set; }

    }
}