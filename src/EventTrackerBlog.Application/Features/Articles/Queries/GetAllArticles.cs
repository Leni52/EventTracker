using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Domain.Features.Articles.Queries
{
    public class GetAllArticles : IRequest<IEnumerable<ArticleResponseModel>>
    {
        public class GetAllArticlesHandler : IRequestHandler<GetAllArticles, IEnumerable<ArticleResponseModel>>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetAllArticlesHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ArticleResponseModel>> Handle(GetAllArticles request, CancellationToken cancellationToken)
            {
                var articlesList = await _context.Articles.ToListAsync();
                var articlesResponse = _mapper.Map<IEnumerable<ArticleResponseModel>>(articlesList);
                if (!articlesResponse.Any())
                {
                    throw new ItemDoesNotExistException();
                }
                return articlesResponse;
            }
        }
    }
}
