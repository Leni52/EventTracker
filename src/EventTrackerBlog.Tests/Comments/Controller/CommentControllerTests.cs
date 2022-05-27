using System;
using System.Linq;
using EventTrackerBlog.WebAPI.Controllers;
using ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace EventTrackerBlog.Tests.Comments.Controller
{
    using static Data.ControllerTestData;

    public class CommentControllerTests
    {
        [Fact]
        public void ControllerShouldHaveCorrectAttributes()
            => MyController<CommentController>
                .Instance()
                .ShouldHave()
                .Attributes(x => x.ContainingAttributeOfType<ApiControllerAttribute>());

        [Fact]
        public void GetAllCommentsShouldReturnCorrectResult()
            => MyController<CommentController>
                .Instance(controller => controller
                    .WithoutData())
                .Calling(c => c.GetAllComments())
                .ShouldReturn()
                .Ok();


        [Fact]
        public void GetCommentsByArticleShouldReturnOkWithValidData()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.GetCommentsByArticle(TestArticle.Id))
                .ShouldReturn()
                .Ok();

        [Fact]
        public void GetCommentsByArticleShouldThrowExceptionWithInvalidData()
            => MyController<CommentController>
                .Instance()
                .Calling(c => c.GetCommentsByArticle(new Guid()))
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void GetCommentByIdShouldReturnOkWithValidData()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.GetCommentById(TestArticle.Id, TestArticle.Comments.Select(cm => cm.Id).First()))
                .ShouldReturn()
                .Ok();

        [Fact]
        public void GetCommentByIdShouldThrowExceptionWithInvalidCommentId()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.GetCommentById(TestArticle.Id, TestComment.Id))
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void GetCommentByIdShouldThrowExceptionWithInvalidArticleId()
        => MyController<CommentController>
            .Instance(controller => controller.WithData(TestComment))
            .Calling(c => c.GetCommentById(TestArticle.Id, TestComment.Id))
            .ShouldThrow()
            .AggregateException()
            .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void CreateCommentShouldReturnOkWithValidModelState()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => 
                    c.CreateComment(ArticleValidRequestModel, TestArticle.Id))
                .ShouldReturn()
                .Ok();

        [Fact]
        public void CreateCommentShouldReturnBadRequestWithInvalidModelState()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.CreateComment(ArticleInvalidRequestModel, TestArticle.Id))
                .ShouldReturn()
                .BadRequest();
    }
}
