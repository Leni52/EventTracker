using AutoMapper;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Application.Features.Articles.Queries;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Handlers.Articles
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, ArticleResponseModel>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public GetArticleByIdHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ArticleResponseModel> Handle(GetArticleByIdQuery query, CancellationToken cancellationToken)
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


