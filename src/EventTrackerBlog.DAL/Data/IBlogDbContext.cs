using EventTrackerBlog.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.DAL.Data
{
    public interface IBlogDbContext
    {
        DbSet<Article> Articles { get; set; }
    }
}