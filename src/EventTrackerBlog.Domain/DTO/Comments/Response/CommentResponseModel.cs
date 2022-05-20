using System;

namespace EventTrackerBlog.Domain.DTO.Comments.Response
{
    public class CommentResponseModel
    {
        public string Content { get; set; }
        public Guid ArticleId { get; set; }
    }
}