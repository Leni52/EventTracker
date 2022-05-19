using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Queries
{
    public class GetArticleListsQuery : IRequest<IEnumerable<Article>>
    {
        public class GetArticleListsQueryHandler : IRequestHandler<GetArticleListsQuery, IEnumerable<Article>>
        {
            private readonly IBlogDbContext _context;
            public GetArticleListsQueryHandler(IBlogDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Article>> Handle(GetArticleListsQuery query, CancellationToken cancellationToken)
            {
                var articlesList = await _context.Articles.ToListAsync();
                if (articlesList == null)
                {
                    return null;
                }
                return articlesList.AsReadOnly();
            }

        }
    }
}
