using System;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Commands
{
    public class CreateCommentCommand : IRequest<CommentResponseModel>
    {
        public string Content { get; set; }
        public Guid ArticleId { get; set; }
    }
}