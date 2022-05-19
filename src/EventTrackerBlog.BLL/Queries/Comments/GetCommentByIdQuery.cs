using System;
using EventTrackerBlog.DAL.Entities;
using MediatR;

namespace EventTrackerBlog.BLL.Queries.Comments
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