using System;

namespace dotnetWebApi.Models.BindingModel
{
    public class AddUpdateTasinmaz
    {
        public int id { get; set; }
        public int IlId { get; set; }
        public int IlceId { get; set; }
        public int MahalleId { get; set; }
        public string Adres { get; set; }
        public int Parsel { get; set; }
        public int Ada { get; set; }
        public string Nitelik { get; set; }
        public string XCoordinate { get; set; }
        public string YCoordinate { get; set; }
        public string ParselCoordinate { get; set; }
        public string AppUserId { get; set; }

    }
}