using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Queries
{
    public class GetArticleByIdQuery : IRequest<Article>
    {
        public Guid ArticleId { get; set; }      
        
    }
}
