using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Commands
{
    public class CreateArticleCommand : IRequest<Article>
    {
        public string Title { get; set; }
        public string Content { get; set; }
       
    }
}
