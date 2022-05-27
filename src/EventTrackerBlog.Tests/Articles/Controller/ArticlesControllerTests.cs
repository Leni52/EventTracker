using EventTrackerBlog.WebAPI.Controllers;
using ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace EventTrackerBlog.Tests.Articles.Controller
{
    using static Data.Articles;
    public class ArticlesControllerTests
    {
        [Fact]
        public void ArticlesControllerShouldHaveAttributes()
       => MyController<ArticleController>
            .ShouldHave()
            .Attributes(x => x.ContainingAttributeOfType<ApiControllerAttribute>());

        [Fact]
        public void DeleteShouldReturnNoContent()
        => MyController<ArticleController>
            .Instance(controller => controller
            .WithData(TestArticle))
            .Calling(c => c.DeleteArticle(TestArticle.Id))
            .ShouldReturn()
            .NoContent();

        [Fact]
        public void DeleteArticleShouldThrowException()
           => MyController<ArticleController>
           .Instance().
           Calling(c => c.DeleteArticle(System.Guid.Empty))
           .ShouldThrow()
           .AggregateException()
           .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void GetAllArticlesShouldReturnCorrect()
      => MyController<ArticleController>
            .Instance(controller => controller
            .WithData(ThreeArticles))
            .Calling(c => c.GetAllArticles())
            .ShouldReturn()
            .Ok();

        [Fact]
        public void GetAllArticlesShouldThrowException()
           => MyController<ArticleController>
           .Instance(controller => controller
           .WithoutData())
           .Calling(c => c.GetAllArticles())
           .ShouldThrow()
           .AggregateException()
           .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void GetArticleByIdShouldReturnCorrect()
        => MyController<ArticleController>
            .Instance(controller => controller
            .WithData(TestArticle))
            .Calling(c => c.GetById(TestArticle.Id))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void GetArticleByIdShouldThrowException()
        => MyController<ArticleController>
            .Instance(controller => controller
            .WithoutData())
            .Calling(c => c.GetById(System.Guid.Empty))
            .ShouldThrow()
            .AggregateException()
            .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void CreateArticleShouldReturnCorrect()
            => MyController<ArticleController>
            .Instance(controller => controller
            .WithData(TestArticle))
            .Calling(c => c.CreateArticle(ValidRequestModel))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void CreateArticleInvalidTitleShouldReturnBadRequest()
           => MyController<ArticleController>
           .Instance(controller => controller
           .WithData(TestArticle))
           .Calling(c => c.CreateArticle(RequestModelInvalidTitle))
           .ShouldReturn()
           .BadRequest();

        [Fact]
        public void CreateArticleInvalidContentShouldReturnBadRequest()
           => MyController<ArticleController>
           .Instance(controller => controller
           .WithData(TestArticle))
           .Calling(c => c.CreateArticle(RequestModelInvalidContent))
           .ShouldReturn()
           .BadRequest();

        [Fact]
        public void UpdateArticleShouldReturnCorrect()
            => MyController<ArticleController>
            .Instance(controller => controller
            .WithData(TestArticle))
            .Calling(c => c.UpdateArticle(TestArticle.Id, ValidRequestModel))
            .ShouldReturn()
            .Ok();

        [Fact]
        public void UpdateArticleShouldThrowException()
           => MyController<ArticleController>
           .Instance(controller => controller
           .WithData(TestArticle))
           .Calling(c => c.UpdateArticle(System.Guid.Empty, ValidRequestModel))
           .ShouldThrow()
            .AggregateException()
            .ContainingInnerExceptionOfType<ItemDoesNotExistException>();

        [Fact]
        public void UpdateArticleShouldReturnBadRequest()
         => MyController<ArticleController>
         .Instance(controller => controller
         .WithData(TestArticle))
         .Calling(c => c.UpdateArticle(TestArticle.Id, RequestModelInvalidTitle))
         .ShouldReturn()
        .BadRequest();

    }
}
