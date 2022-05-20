using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Application.Features.Articles.Commands;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Handlers.Articles
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
            var articleToUpdate = await _context.Articles.Where(a => a.Id == command.ArticleId).FirstOrDefaultAsync();
            if (articleToUpdate == null)
            {
                throw new ItemDoesNotExistException();
            }
            articleToUpdate.Title = command.Title;
            articleToUpdate.Content = command.Content;
            articleToUpdate.LastModifiedAt = DateTime.Now;
            return articleToUpdate.Id;
        }
    }
}
