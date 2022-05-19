using EventTrackerBlog.DAL.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Commands
{
    public class UpdateArticleCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Guid>
        {
            private readonly IBlogDbContext _context;
            public UpdateArticleCommandHandler(IBlogDbContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
            {
                var article = await _context.Articles.Where(a => a.Id == command.ArticleId).FirstOrDefaultAsync();
                if (article == null)
                {
                    return default;
                }
                else
                {
                    article.Title = command.Title;
                    article.Content = command.Content;
                    await _context.SaveChanges();
                    return article.Id;
                }

            }
        }
    }
}
