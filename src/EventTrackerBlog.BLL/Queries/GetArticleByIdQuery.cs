using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Queries
{
    public class GetArticleByIdQuery : IRequest<Article>
    {
        public Guid ArticleId { get; set; }
        public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Article>
        {
            private readonly IBlogDbContext _context;
            public GetArticleByIdQueryHandler(IBlogDbContext context)
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
}
