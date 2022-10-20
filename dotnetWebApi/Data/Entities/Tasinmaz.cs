namespace Data.Entities
{
    public class Tasinmaz
    {
        public int TasinmazId { get; set; }
        public int Il { get; set; }
        public  Mahalle Mahalle{ get; set; }
        public int Ilce { get; set; }
        public int MahalleId { get; set; }
        public string Ada { get; set; }
        public string Parsel { get; set; }
        public int Kordinat { get; set; }
        public string Nitelik { get; set; }

        public string Adres { get; set; }

        public int UserId { get; set; }
        public string coorX { get; set; }
        public string  coorY { get; set; }
    }
}