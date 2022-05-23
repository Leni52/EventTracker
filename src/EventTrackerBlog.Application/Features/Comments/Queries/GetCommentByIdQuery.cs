using System;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Queries
{
    public class GetCommentByIdQuery : IRequest<CommentResponseModel>
    {
        public Guid Id { get; }

        public GetCommentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}