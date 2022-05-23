using System;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Queries
{
    public class GetCommentByIdQuery : IRequest<Comment>
    {
        public Guid Id { get; }

        public GetCommentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}