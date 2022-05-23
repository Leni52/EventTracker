using AutoMapper;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Application.Features.Articles.Queries;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Handlers.Articles
{
    public class GetAllArticlesHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<ArticleResponseModel>>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public GetAllArticlesHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ArticleResponseModel>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {
            var articlesList = await _context.Articles.ToListAsync();
            var articlesResponse = _mapper.Map<IEnumerable<ArticleResponseModel>>(articlesList);
            if (articlesResponse == null)
            {
                throw new ItemDoesNotExistException();
            }
            return articlesResponse;
        }
    }
}
