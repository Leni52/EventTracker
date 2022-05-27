﻿using System;
using System.Collections.Generic;
using System.Linq;
using EventTrackerBlog.Domain.DTO.Comments.Request;
using EventTrackerBlog.Domain.Entities;

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
            Comments = Enumerable.Range(0, 5).Select(_ => new Comment())
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

        public static CommentRequestModel ArticleValidRequestModel = new()
        {
            Content = "Article request model"
        };

        public static CommentRequestModel ArticleInvalidRequestModel = new()
        {
            Content = string.Empty
        };
    }
}
