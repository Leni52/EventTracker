using System.Collections.Generic;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Queries.Articles
{
    public class GetAllArticlesQuery : IRequest<IEnumerable<Article>>
    {
        public GetAllArticlesQuery()
        {

        }
    }
}
