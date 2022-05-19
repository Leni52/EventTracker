using EventTrackerBlog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EventTrackerBlog.DAL.Data
{
    public class BlogDbContext : DbContext, IBlogDbContext
    {
        public BlogDbContext()
        {
        }

        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionStrings:Default");
            }

            base.OnConfiguring(optionsBuilder);
        }
        public async Task<int> SaveChanges()
        {
          
           return await base.SaveChangesAsync();
        }
    }
}
