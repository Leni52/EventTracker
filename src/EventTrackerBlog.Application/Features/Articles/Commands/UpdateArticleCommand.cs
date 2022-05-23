using MediatR;
using System;
using EventTrackerBlog.Domain.Entities;
using EventTrackerBlog.Domain.DTO.Articles.Response;

namespace EventTrackerBlog.Application.Features.Articles.Commands
{
    public class UpdateArticleCommand : IRequest<ArticleResponseModel>
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
