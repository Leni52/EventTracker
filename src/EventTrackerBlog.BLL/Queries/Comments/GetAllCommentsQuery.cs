using System;
using System.Collections.Generic;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Queries.Comments
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<Comment>>
    {
        public GetAllCommentsQuery(Guid articleId)
        {
            ArticleId = articleId;
        }

        public Guid ArticleId { get; }
    }
}
