using System;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Commands
{
    public class DeleteCommentCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
        public Guid CommentId { get; }

        public DeleteCommentCommand(Guid articleId, Guid commentId)
        {
            ArticleId = articleId;
            CommentId = commentId;
        }
    }
}