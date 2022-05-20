using System;
using MediatR;

namespace EventTrackerBlog.Application.Commands.Comments
{
    public class DeleteCommentCommand : IRequest<Guid>
    {
        public Guid CommentId { get; set; }
    }
}