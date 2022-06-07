using EventTrackerBlog.Data.Entities;
using EventTrackerBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Data.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
        }

        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions {get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionStrings:Default");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}