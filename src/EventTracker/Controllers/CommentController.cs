using EventTracker.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private static ICommentService _commentService;

        public CommentController(ICommentService commentService) : base()
        {
            _commentService = commentService;
        }
    }
}
