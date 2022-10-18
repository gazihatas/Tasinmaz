using System;

namespace dotnetWebApi.Models.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public bool Publish { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AppUserId { get; set; }
    }
}