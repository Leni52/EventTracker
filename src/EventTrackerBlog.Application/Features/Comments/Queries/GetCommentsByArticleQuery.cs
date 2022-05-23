
using System;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace EventTrackerBlog.Application.Features.Comments.Queries
{
    public class GetCommentsByArticleQuery : IRequest<IEnumerable<Comment>>
    {
        public Guid ArticleId { get; set; }

        public GetCommentsByArticleQuery(Guid articleId)
        {
            ArticleId = articleId;
        }
    }
}
