using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.BLL.Queries.Articles;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Queries.Articles;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Handlers.Articles
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
                throw new ItemDoesNotExistException();
            }
            return article;
        }
    }
}


