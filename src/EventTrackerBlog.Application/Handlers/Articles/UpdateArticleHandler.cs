using AutoMapper;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Application.Features.Articles.Commands;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Handlers.Articles
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ArticleResponseModel>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public UpdateArticleCommandHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ArticleResponseModel> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
        {
            var articleToUpdate = await _context.Articles.Where(a => a.Id == command.ArticleId).FirstOrDefaultAsync();
            if (articleToUpdate == null)
            {
                throw new ItemDoesNotExistException();
            }
            articleToUpdate.Title = command.Title;
            articleToUpdate.Content = command.Content;
            articleToUpdate.LastModifiedAt = DateTime.Now;
            _context.SaveChanges();
            var articleResponse = _mapper.Map<ArticleResponseModel>(articleToUpdate);
            return articleResponse;
        }
    }
}
