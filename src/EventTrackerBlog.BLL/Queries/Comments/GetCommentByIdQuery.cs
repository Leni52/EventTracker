using System;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Queries.Comments
{
    public class GetCommentByIdQuery : IRequest<Comment>
    {
        public Guid Id { get; set; }

        public GetCommentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}