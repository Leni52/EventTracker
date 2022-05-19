using EventTrackerBlog.DAL.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Commands
{
    public class DeleteArticleByIdCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }

        public class DeleteArticleByIdCommandHandler : IRequestHandler<DeleteArticleByIdCommand, Guid>
        {
            private readonly IBlogDbContext _context;
            public DeleteArticleByIdCommandHandler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(DeleteArticleByIdCommand command, CancellationToken cancellationToken)
            {
                var article = await _context.Articles.Where(a => a.Id == command.ArticleId).FirstOrDefaultAsync();
                if(article == null)
                {
                    return default;
                }
                _context.Articles.Remove(article);
                await _context.SaveChanges();
                return article.Id;
            }
        }
    }
}
