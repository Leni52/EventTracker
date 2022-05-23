using AutoMapper;
using EventTrackerBlog.Application.Features.Articles.Commands;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Handlers.Articles
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleRequestModel>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public CreateArticleCommandHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ArticleRequestModel> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = new Article()
            {
                Title = request.Title,
                Content = request.Content
            };

            _context.Articles.Add(article);
            _context.SaveChanges();
            var articleResponse = _mapper.Map<ArticleRequestModel>(article);
            return articleResponse;
        }
    }
}

