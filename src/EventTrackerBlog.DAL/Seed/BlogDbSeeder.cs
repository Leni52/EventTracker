using System;
using System.Collections.Generic;
using System.Linq;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventTrackerBlog.DAL.Seed
{
    public static class BlogDbSeeder
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            Seed(serviceScope.ServiceProvider.GetService<BlogDbContext>());
        }

        private static void Seed(BlogDbContext context)
        {
             context.Database.EnsureCreated();

            if (!context.Articles.Any())
            {
                SeedArticles(context);
            }

            if (!context.Comments.Any())
            {
                SeedComments(context);
            }

            context.SaveChanges();
        }

        private static void SeedArticles(DbContext context)
        {
            context.AddRange(new List<Article>
            {
                new()
                {
                    Title = "Test Article",
                    Content = "Test Article Content",
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now
                },
                new()
                {
                    Title = "Another Article",
                    Content = "Another Article Content",
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now
                }
            });
        }

        private static void SeedComments(DbContext context)
        {
            context.AddRange(new List<Comment>
            {
                new()
                {
                    Content = "Test Article Comment",
                    ArticleId = new Guid("E2333320-8BEA-47B1-7187-08DA395D2539"),
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now
                },
                new()
                {
                    Content = "Another Article Comment",
                    ArticleId = new Guid("A61EB005-074E-4805-7188-08DA395D2539"),
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now
                }
            });
        }
    }
}
