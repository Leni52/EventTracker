using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.BLL.Commands.Articles;
using EventTrackerBlog.DAL.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Handlers.Articles
{
    public class DeleteArticleByIdCommandHandler : IRequestHandler<DeleteArticleByIdCommand, Guid>
    {
        private readonly BlogDbContext _context;
        public DeleteArticleByIdCommandHandler(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(DeleteArticleByIdCommand command, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.Where(a => a.Id == command.ArticleId).FirstOrDefaultAsync();
            if (article == null)
            {
                throw new ItemDoesNotExistException();
            }
            _context.Articles.Remove(article);
            _context.SaveChanges();
            return article.Id;
        }
    }
}
