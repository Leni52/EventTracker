using EventTrackerBlog.Domain.DTO.Articles.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventTrackerBlog.Data.Entities;

namespace EventTrackerBlog.Tests.Data
{
    public static class Articles
    {
        public static Article TestArticle => new()
        {
            Id = Guid.Parse("D9D76124-7D4E-4A13-E925-08DA3CA9C95C"),
            Title = "Test title",
            Content = "Test Content",

        };

        public static IEnumerable<Article> ThreeArticles =>
            Enumerable.Range(0, 3).Select(_ => new Article());

        public static ArticleRequestModel ValidRequestModel
        => new()
        {
            Title = "New title",
            Content = "New content"
        };
        public static ArticleRequestModel RequestModelInvalidTitle
        => new()
        {
            Title = "",
            Content = "New content"
        };
        public static ArticleRequestModel RequestModelInvalidContent
        => new()
        {
            Title = "New title",
            Content = ""
        };

    }
}
