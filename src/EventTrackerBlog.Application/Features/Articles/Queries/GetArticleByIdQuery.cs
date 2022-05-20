using System;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Features.Articles.Queries
{
    public class GetArticleByIdQuery : IRequest<Article>
    {
        public Guid ArticleId { get; set; }

    }
}
