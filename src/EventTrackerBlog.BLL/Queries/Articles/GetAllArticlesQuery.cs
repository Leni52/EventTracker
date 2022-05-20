using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.BLL.Queries.Articles
{
    public class GetAllArticlesQuery : IRequest<IEnumerable<Article>>
    {
        public GetAllArticlesQuery()
        {

        }
    }
}
