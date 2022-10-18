namespace Data.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Publish { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}