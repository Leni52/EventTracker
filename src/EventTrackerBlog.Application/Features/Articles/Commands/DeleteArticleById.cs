using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Data.Data;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Domain.Features.Articles.Commands
{
    public class DeleteArticleById : IRequest<Guid>
    {
        public Guid ArticleId { get; }
        public DeleteArticleById(Guid articleId)
        {
            ArticleId = articleId;
        }

        public class DeleteArticleByIdHandler : IRequestHandler<DeleteArticleById, Guid>
        {
            private readonly BlogDbContext _context;

            public DeleteArticleByIdHandler(BlogDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(DeleteArticleById command, CancellationToken cancellationToken)
            {
                var article = await _context.Articles.Where(a => a.Id == command.ArticleId).FirstOrDefaultAsync();
                if (article == null)
                {
                    throw new ItemDoesNotExistException();
                }
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return article.Id;
            }
        }
    }
}
