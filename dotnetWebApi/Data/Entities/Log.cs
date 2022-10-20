using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Log
    {
        [Key]
        public int logid { get; set; }
        public Boolean durum { get; set; }
        public string  islemtipi { get; set; }
        public string acıklama { get; set; }
        public DateTime tarih { get; set; }

        public string logIp { get; set; }

        public int UserId { get; set; }
    }
}