using AutoMapper;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using ExceptionHandling.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Features.Articles.Queries
{
    public class GetArticleById : IRequest<ArticleResponseModel>
    {
        public Guid ArticleId { get; }
        public GetArticleById(Guid articleId)
        {
            ArticleId = articleId;
        }
        public class GetArticleByIdHandler : IRequestHandler<GetArticleById, ArticleResponseModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetArticleByIdHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ArticleResponseModel> Handle(GetArticleById query, CancellationToken cancellationToken)
            {
                var article = _context.Articles.Where(a => a.Id == query.ArticleId).FirstOrDefault();
                if (article == null)
                {
                    throw new ItemDoesNotExistException();
                }
                var articleResponse = _mapper.Map<ArticleResponseModel>(article);
                return articleResponse;
            }
        }
    }
}
