﻿using System;
using System.Collections.Generic;
using System.Linq;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EventTrackerBlog.Domain.Seed
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
            context.Database.EnsureCreated();

            if (!context.Articles.Any())
            {
                SeedArticles(context);
                context.SaveChanges();
            }

            if (!context.Comments.Any())
            {
                SeedComments(context);
                context.SaveChanges();
            }
        }

        private static void SeedArticles(BlogDbContext context)
        {
            context.Articles.AddRange(new List<Article>
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

        private static void SeedComments(BlogDbContext context)
        {
            var articleId = context.Articles
                .Select(a => a.Id)
                .FirstOrDefault();

            context.Comments.AddRange(new List<Comment>
            {
                new()
                {
                    Content = "Test Article Comment",
                    ArticleId = articleId,
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now
                },
                new()
                {
                    Content = "Another Article Comment",
                    ArticleId = articleId,
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now
                }
            });
        }
    }
}