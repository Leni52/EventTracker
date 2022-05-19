using EventTrackerBlog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EventTrackerBlog.DAL.Data
{
    public interface IBlogDbContext
    { 
        DbSet<Article> Articles { get; set; }

        Task<int> SaveChanges();
    }
}