using System;

namespace EventTrackerBlog.DAL.DTO.Comments.Response
{
    public class CommentResponseModel
    {
        public string Content { get; set; }
        public Guid ArticleId { get; set; }
    }
}