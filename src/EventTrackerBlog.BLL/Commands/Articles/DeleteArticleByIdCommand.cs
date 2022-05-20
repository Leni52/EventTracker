using EventTrackerBlog.DAL.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventTrackerBlog.Application.Commands.Articles
{
    public class DeleteArticleByIdCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
    }
}
