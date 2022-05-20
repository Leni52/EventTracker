using MediatR;
using System;

namespace EventTrackerBlog.Application.Features.Articles.Commands
{
    public class DeleteArticleByIdCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
    }
}
