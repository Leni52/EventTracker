using System;
using System.Linq;
using EventTrackerBlog.Data.Entities;
using EventTrackerBlog.Domain.DTO.Comments.Request;

namespace EventTrackerBlog.Tests.Comments.Data
{
    public class ControllerTestData
    {
        public static Article TestArticle = new()
        {
            Id = new Guid(),
            Title = "Test article",
            Content = "Test article content",
            CreatedAt = DateTime.Now,
            LastModifiedAt = DateTime.Now,
            Comments = Enumerable.Range(0, 5)
                .Select(_ => new Comment())
                .ToList()
        };

        public static Comment TestComment = new()
        {
            Id = new Guid(),
            Content = "Test comment for test article",
            ArticleId = TestArticle.Id,
            CreatedAt = DateTime.Now,
            LastModifiedAt = DateTime.Now
        };

        public static CommentRequestModel CommentValidRequestModel = new()
        {
            Content = "Article request model"
        };

        public static CommentRequestModel CommentInvalidRequestModel = new()
        {
            Content = string.Empty
        };
    }
}