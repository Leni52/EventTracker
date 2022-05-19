using EventTrackerBlog.BLL.Queries;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Handlers
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
