using MediatR;
using System;
using EventTrackerBlog.Domain.Entities;

namespace EventTrackerBlog.Application.Features.Articles.Commands
{
    public class UpdateArticleCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
