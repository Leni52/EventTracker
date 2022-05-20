using System;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Queries.Articles
{
    public class GetArticleByIdQuery : IRequest<Article>
    {
        public Guid ArticleId { get; set; }
       
    }
}
