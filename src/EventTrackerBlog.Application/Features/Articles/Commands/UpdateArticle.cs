using AutoMapper;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Features.Articles.Commands
{
    public class UpdateArticle : IRequest<ArticleResponseModel>
    {
        public Guid ArticleId { get; }
        public string Title { get; }
        public string Content { get; }
        public UpdateArticle(string title, string content, Guid articleId)
        {
            Title = title;
            Content = content;
            ArticleId = articleId;
        }

        public class UpdateArticleHandler : IRequestHandler<UpdateArticle, ArticleResponseModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public UpdateArticleHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ArticleResponseModel> Handle(UpdateArticle command, CancellationToken cancellationToken)
            {
                var articleToUpdate = await _context.Articles.Where(a => a.Id == command.ArticleId).FirstOrDefaultAsync();
                if (articleToUpdate == null)
                {
                    throw new ItemDoesNotExistException();
                }
                articleToUpdate.Title = command.Title;
                articleToUpdate.Content = command.Content;
                articleToUpdate.LastModifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                var articleResponse = _mapper.Map<ArticleResponseModel>(articleToUpdate);
                return articleResponse;
            }
        }
    }
}