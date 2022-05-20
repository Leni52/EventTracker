using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Commands.Articles
{
    public class CreateArticleCommand : IRequest<Article>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
