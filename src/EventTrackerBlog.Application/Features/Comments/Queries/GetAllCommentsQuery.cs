
using EventTrackerBlog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace EventTrackerBlog.Application.Features.Comments.Queries
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<Comment>>
    {
        public GetAllCommentsQuery()
        {

        }
    }
}
