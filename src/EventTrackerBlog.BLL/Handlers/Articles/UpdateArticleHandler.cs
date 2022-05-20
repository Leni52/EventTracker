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
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Guid>
    {
        private readonly BlogDbContext _context;
        public UpdateArticleCommandHandler(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.Where(a => a.Id == command.ArticleId).FirstOrDefaultAsync();
            if (article == null)
            {
                throw new ItemDoesNotExistException();
            }
            else
            {
                article.Title = command.Title;
                article.Content = command.Content;
                _context.SaveChanges();
                return article.Id;
            }
        }
    }
}
