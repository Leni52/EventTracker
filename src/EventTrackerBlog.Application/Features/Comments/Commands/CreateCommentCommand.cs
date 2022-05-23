using System;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Commands
{
    public class CreateCommentCommand : IRequest<CommentResponseModel>
    {
        public string Content { get; }
        public Guid ArticleId { get; }

        public CreateCommentCommand(string content, Guid articleId)
        {
            Content = content;
            ArticleId = articleId;
        }
    }
}