using EventTrackerBlog.Application.Features.Articles.Commands;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            var article = new Article()
            {
                Title = request.Title,
                Content = request.Content
            };

            _context.Articles.Add(article);
            _context.SaveChanges();

            return article;
        }
    }
}

