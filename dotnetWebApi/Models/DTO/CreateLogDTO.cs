using System;

namespace dotnetWebApi.Model.DTO
{
    public class CreateLogDTO
    {
        public int UserId { get; set; }     
        public string Durum { get; set; }
        public string IslemTipi { get; set; }
        public string Aciklama { get; set; }
        public string DateTime { get; set; }
        public string UserIp { get; set; }
    }
}