using System;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Commands
{
    public class EditCommentCommand : IRequest<CommentEditResponseModel>
    {
        public Guid Id { get; }
        public string Content { get; }

        public EditCommentCommand(Guid id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}