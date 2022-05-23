using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace EventTrackerBlog.Application.Features.Articles.Queries
{
    public class GetAllArticlesQuery : IRequest<IEnumerable<ArticleResponseModel>>
    {
        public GetAllArticlesQuery()
        {
        }
    }
}
