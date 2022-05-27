using EventTrackerBlog.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace EventTrackerBlog.Tests.Comments.Controller
{
    public class CommentControllerTests
    {
        [Fact]
        public void ControllerShouldHaveCorrectAttributes()
            => MyController<CommentController>
                .Instance()
                .ShouldHave()
                .Attributes(x => x.ContainingAttributeOfType<ApiControllerAttribute>());
    }
}
