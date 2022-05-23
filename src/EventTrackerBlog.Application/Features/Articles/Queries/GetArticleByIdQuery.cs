using EventTrackerBlog.Domain.DTO.Articles.Response;
using MediatR;
using System;

namespace EventTrackerBlog.Application.Features.Articles.Queries
{
    public class GetArticleByIdQuery : IRequest<ArticleResponseModel>
    {
        public Guid ArticleId { get; set; }
        public GetArticleByIdQuery(Guid articleId)
        {
            ArticleId = articleId;
        }
    }
}
