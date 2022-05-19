using System;
using System.Collections.Generic;
using EventTrackerBlog.DAL.Entities;
using MediatR;

namespace EventTrackerBlog.BLL.Queries.Comments
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
