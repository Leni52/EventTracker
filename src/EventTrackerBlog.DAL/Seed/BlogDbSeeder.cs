using System;
using System.Collections.Generic;
using System.Linq;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EventTrackerBlog.DAL.Seed
{
    public static class BlogDbSeeder
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Seed(serviceScope.ServiceProvider.GetService<BlogDbContext>());
            }
        }

        private static void Seed(BlogDbContext context)
        {
          //  context.Database.EnsureCreated();

            if (!context.Articles.Any())
            {
                context.AddRange(new List<Article>
                {
                    new Article
                    {
                        Title = "Test Article",
                        Content = "Test Article Content",
                        CreatedAt = DateTime.Now,
                        LastModifiedAt = DateTime.Now
                    },
                    new Article
                    {
                        Title = "Another Article",
                        Content = "Another Article Content",
                        CreatedAt = DateTime.Now,
                        LastModifiedAt = DateTime.Now
                    },
                });
            }

            context.SaveChanges();
        }
    }
}
