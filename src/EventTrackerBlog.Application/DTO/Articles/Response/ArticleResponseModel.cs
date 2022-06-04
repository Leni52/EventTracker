using System;

namespace EventTrackerBlog.Domain.DTO.Articles.Response
{
    public class ArticleResponseModel
    {
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
