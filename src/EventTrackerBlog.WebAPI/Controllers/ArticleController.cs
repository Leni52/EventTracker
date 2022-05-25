using EventTrackerBlog.Application.Features.Articles.Commands;
using EventTrackerBlog.Application.Features.Articles.Queries;
using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;
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

        [HttpDelete("{articleId}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteArticle(Guid articleId)
        {
            var command = new DeleteArticleById();
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
    }
}
