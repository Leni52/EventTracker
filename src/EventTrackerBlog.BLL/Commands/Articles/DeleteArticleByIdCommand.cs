using System;
using MediatR;

namespace EventTrackerBlog.Application.Commands.Articles
{
    public class DeleteArticleByIdCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }

    }
}
