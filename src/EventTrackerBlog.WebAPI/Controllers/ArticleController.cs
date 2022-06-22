using EventTrackerBlog.Application.Features.Reactions.Queries;
using EventTrackerBlog.Data.Entities;
using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using EventTrackerBlog.Domain.Features.Articles.Commands;
using EventTrackerBlog.Domain.Features.Articles.Queries;
using EventTrackerBlog.WebAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTrackerBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IMediator _mediator;
        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<Article>> CreateArticle(ArticleRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new CreateArticle(request.Title, request.Content);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ServiceFilter(typeof(LogActionFilter))]
        public async Task<ActionResult<IEnumerable<ArticleResponseModel>>> GetAllArticles()
        {
            var query = new GetAllArticles();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{articleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ArticleResponseModel>> GetById(Guid articleId)
        {
            var query = new GetArticleById(articleId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{articleId:guid}/reaction")]
        public async Task<ActionResult<IEnumerable<ReactionResponseModel>>> GetReactionsByArticle(Guid articleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var getReactions = new GetReactionsByArticle(articleId);
            var result = await _mediator.Send(getReactions);

            return Ok(result);
        }

        [HttpDelete("{articleId}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteArticle(Guid articleId)
        {
            var command = new DeleteArticleById(articleId);
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut("articleId")]
        [ProducesResponseType(403)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateArticle(Guid articleId, ArticleRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var command = new UpdateArticle(request.Title, request.Content, articleId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("keyword/{keyword}")]
        public async Task<IActionResult> GetArticlesByKeyword(string keyword)
        {
            var query = new GetArticleByKeyword(keyword);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
