using System;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Commands
{
    public class DeleteCommentCommand : IRequest<Guid>
    {
        public Guid CommentId { get; }

        public DeleteCommentCommand(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}