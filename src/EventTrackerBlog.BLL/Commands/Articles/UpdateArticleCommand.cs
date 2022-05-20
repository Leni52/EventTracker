using System;
using MediatR;

namespace EventTrackerBlog.Application.Commands.Articles
{
    public class UpdateArticleCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
