using EventTrackerBlog.DAL.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Commands.Articles
{
    public class UpdateArticleCommand : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
