using System;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Commands
{
    public class EditCommentCommand : IRequest<CommentResponseModel>
    {
        public Guid ArticleId { get; set; }
        public Guid CommentId { get; }
        public string Content { get; }

        public EditCommentCommand(Guid articleId, Guid commentId, string content)
        {
            ArticleId = articleId;
            CommentId = commentId;
            Content = content;
        }
    }
}