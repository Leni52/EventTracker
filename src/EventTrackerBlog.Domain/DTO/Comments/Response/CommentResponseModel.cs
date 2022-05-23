using System;

namespace EventTrackerBlog.Domain.DTO.Comments.Response
{
    public class CommentResponseModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid ArticleId { get; set; }
    }
}