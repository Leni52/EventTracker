using EventTrackerBlog.BLL.Commands;
using EventTrackerBlog.BLL.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EventTrackerBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        [HttpPost]
        public async Task<IActionResult> CreateArticle(CreateArticleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            return Ok(await Mediator.Send(new GetArticleListsQuery()));
        }
        [HttpGet("{articleId}")]
        public async Task<IActionResult> GetById(Guid articleId)
        {
            return Ok(await Mediator.Send(new GetArticleByIdQuery { ArticleId = articleId }));

        }
        [HttpDelete("{articleId}")]
        public async Task<IActionResult> DeleteArticle(Guid articleId)
        {
            return Ok(await Mediator.Send(new DeleteArticleByIdCommand { ArticleId = articleId }));
        }
        [HttpPut("articleId")]
        public async Task<IActionResult>UpdateArticle(Guid articleId, UpdateArticleCommand command)
        {
            if (articleId != command.ArticleId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
