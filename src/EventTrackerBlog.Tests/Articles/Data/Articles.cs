using System;
using System.Collections.Generic;
using System.Linq;
using EventTrackerBlog.Data.Entities;
using EventTrackerBlog.Domain.DTO.Articles.Request;

namespace EventTrackerBlog.Tests.Articles.Data
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
