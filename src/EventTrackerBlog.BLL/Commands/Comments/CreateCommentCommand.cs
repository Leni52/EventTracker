using System;
using EventTrackerBlog.DAL.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.BLL.Commands.Comments
{
    public class CreateCommentCommand : IRequest<CommentResponseModel>
    {
        public string Content { get; set; }
        public Guid ArticleId { get; set; }
    }
}