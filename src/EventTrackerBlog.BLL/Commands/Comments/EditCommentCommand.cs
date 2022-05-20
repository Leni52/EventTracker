using System;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Commands.Comments
{
    public class EditCommentCommand : IRequest<CommentEditResponseModel>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}