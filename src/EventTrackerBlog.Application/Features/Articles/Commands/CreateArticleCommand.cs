using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Features.Articles.Commands
{
    public class CreateArticleCommand : IRequest<ArticleRequestModel>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public CreateArticleCommand(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
