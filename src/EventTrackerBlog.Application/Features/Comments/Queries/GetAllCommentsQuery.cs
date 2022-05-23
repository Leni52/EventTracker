using System.Collections.Generic;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Queries
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<Comment>>
    {
    }
}