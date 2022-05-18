using EventTrackerBlog.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.DAL.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
        }

        public DbSet<Article> Articles { get; set; }
    }
}
