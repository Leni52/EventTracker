using MediatR;
using System;

namespace EventTrackerBlog.BLL.Commands.Articles
{
    public class DeleteArticleByIdCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
    }
}
