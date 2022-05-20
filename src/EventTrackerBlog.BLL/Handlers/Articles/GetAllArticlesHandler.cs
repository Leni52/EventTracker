using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Queries.Articles;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Application.Handlers.Articles
{
    public class GetAllArticlesHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<Article>>
    {
        private readonly BlogDbContext _context;
        public GetAllArticlesHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
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
