using System;

namespace dotnetWebApi.Models.BindingModel
{
    public class AddUpdateArticle
    {
         public int Id { get; set; }
         public string Title { get; set; }
         public string Body { get; set; }
         public bool Publish { get; set; }
         public string AppUserId { get; set; }
    }
}