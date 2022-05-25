using AutoMapper;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Features.Articles.Commands
{
    public class CreateArticle : IRequest<ArticleRequestModel>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public CreateArticle(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public class CreateArticleHandler : IRequestHandler<CreateArticle, ArticleRequestModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public CreateArticleHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ArticleRequestModel> Handle(CreateArticle request, CancellationToken cancellationToken)
            {
                var article = new Article()
                {
                    Title = request.Title,
                    Content = request.Content
                };

                await _context.Articles.AddAsync(article);
                await _context.SaveChangesAsync();
                var articleResponse = _mapper.Map<ArticleRequestModel>(article);
                return articleResponse;
            }
        }
    }
}
