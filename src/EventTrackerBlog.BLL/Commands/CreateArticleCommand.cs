using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Commands
{
    public class CreateArticleCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Guid>
        {
            private readonly IBlogDbContext _context;
            public CreateArticleCommandHandler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
            {
                var article = new Article();
                article.Title = command.Title;
                article.Content = command.Content;
                _context.Articles.Add(article);
                await _context.SaveChanges();
                return article.Id;
            }
        }
    }
}
