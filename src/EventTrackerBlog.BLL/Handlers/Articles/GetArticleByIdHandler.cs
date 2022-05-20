using EventTrackerBlog.BLL.Queries.Articles;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Handlers.Articles
{

    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, Article>
    {
        private readonly BlogDbContext _context;
        public GetArticleByIdHandler(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<Article> Handle(GetArticleByIdQuery query, CancellationToken cancellationToken)
        {
            var article = _context.Articles.Where(a => a.Id == query.ArticleId).FirstOrDefault();
            if (article == null)
            {
                return null;
            }
            return article;
        }
    }
}


