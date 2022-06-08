using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Domain.Features.Comments.Queries
{
    public class GetCommentsByArticle : IRequest<IEnumerable<CommentResponseModel>>
    {
        public Guid ArticleId { get; }

        public GetCommentsByArticle(Guid articleId)
        {
            ArticleId = articleId;
        }

        public class GetCommentsByArticleHandler : IRequestHandler<GetCommentsByArticle, IEnumerable<CommentResponseModel>>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetCommentsByArticleHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CommentResponseModel>> Handle(GetCommentsByArticle request, CancellationToken cancellationToken)
            {
                var article = await _context.Articles
                    .Include(a => a.Comments)
                    .FirstOrDefaultAsync(a => a.Id == request.ArticleId, cancellationToken: cancellationToken);

                if (article == null)
                {
                    throw new ItemDoesNotExistException();
                }

                var comments = article
                    .Comments.ToList();

                return _mapper.Map<IEnumerable<CommentResponseModel>>(comments);
            }
        }
    }
}