using System;
using System.Linq;
using EventTrackerBlog.UnitTests.Comments.Data;
using EventTrackerBlog.WebAPI.Controllers;
using ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace EventTrackerBlog.UnitTests.Comments.Controller
{
    using static ControllerTestData;

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
                    c.CreateComment(CommentValidRequestModel, TestArticle.Id))
                .ShouldReturn()
                .Ok();

        [Fact]
        public void CreateCommentShouldReturnBadRequestWithInvalidModelState()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.CreateComment(CommentInvalidRequestModel, TestArticle.Id))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void EditCommentShouldReturnOkWithValidModelState()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.ЕditComment(TestArticle.Id, TestArticle.Comments.Select(cm => cm.Id).First(), CommentValidRequestModel))
                .ShouldReturn()
                .Ok();

        [Fact]
        public void EditCommentShouldReturnBadRequestWithInvalidModelState()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.ЕditComment(TestArticle.Id, TestArticle.Comments.Select(cm => cm.Id).First(), CommentInvalidRequestModel))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void EditCommentShouldThrowExceptionWithInvalidArticleId()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestComment))
                .Calling(c => c.ЕditComment(TestArticle.Id, TestComment.Id, CommentValidRequestModel))
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<ItemDoesNotExistException>();
        
        [Fact]
        public void EditCommentShouldThrowExceptionWithInvalidCommentId()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.ЕditComment(TestArticle.Id, TestComment.Id, CommentValidRequestModel))
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void DeleteShouldReturnNoContentWithValidData()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.DeleteComment(TestArticle.Id, TestArticle.Comments
                    .Select(cm => cm.Id)
                    .First()))
                .ShouldReturn()
                .NoContent();

        [Fact]
        public void DeleteShouldThrowExceptionWithInvalidArticleId()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestComment))
                .Calling(c => c.DeleteComment(TestArticle.Id, TestComment.Id))
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void DeleteShouldThrowExceptionWithInvalidCommentId()
            => MyController<CommentController>
                .Instance(controller => controller.WithData(TestArticle))
                .Calling(c => c.DeleteComment(TestArticle.Id, TestComment.Id))
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<ItemDoesNotExistException>();
    }
}