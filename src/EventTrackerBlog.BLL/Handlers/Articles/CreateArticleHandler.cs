using EventTrackerBlog.BLL.Commands.Articles;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Commands.Articles;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Handlers.Articles
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Article>
    {
        private readonly BlogDbContext _context;
        public CreateArticleCommandHandler(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<Article> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = new Article();
            article.Title = request.Title;
            article.Content = request.Content;
            _context.Articles.Add(article);
            _context.SaveChanges();
            return article;
        }
    }
}

