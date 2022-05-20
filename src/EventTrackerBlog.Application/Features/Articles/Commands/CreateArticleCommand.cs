using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Features.Articles.Commands
{
    public class CreateArticleCommand : IRequest<Article>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
