using AutoMapper;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Domain.Features.Articles.Queries
{
    public class GetArticleByKeyword : IRequest<IEnumerable<ArticleResponseModel>>
    {
        public string Keyword { get; }
        public GetArticleByKeyword(string keyword)
        {
            Keyword = keyword;
        }
        public class GetArticleByKeywordHandler : IRequestHandler<GetArticleByKeyword, IEnumerable<ArticleResponseModel>>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetArticleByKeywordHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ArticleResponseModel>> Handle(GetArticleByKeyword request, CancellationToken cancellationToken)
            {
                var articlesByKeyword = await _context.Articles.Where(a => a.Title.Contains(request.Keyword)).ToListAsync(cancellationToken: cancellationToken);
                var articlesResponse = _mapper.Map<IEnumerable<ArticleResponseModel>>(articlesByKeyword);

                if (!articlesResponse.Any())
                {
                    throw new ItemDoesNotExistException();
                }
                return articlesResponse;
            }
        }
    }
}
