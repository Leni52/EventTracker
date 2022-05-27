using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Domain.DTO.Comments.Request;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventTrackerBlog.Domain.DTO.Comments.Response;

namespace EventTrackerBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentResponseModel>>> GetAllComments()
        {
            var query = new GetAllComments();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{articleId:guid}")]
        public async Task<ActionResult<IEnumerable<CommentResponseModel>>> GetCommentsByArticle(Guid articleId)
        {
            var query = new GetCommentsByArticle(articleId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{articleId:guid}/{commentId:guid}")]
        public async Task<ActionResult<CommentResponseModel>> GetCommentById(Guid articleId, Guid commentId)
        {
            var query = new GetCommentById(articleId, commentId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(CommentRequestModel model, Guid articleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new CreateComment(model.Content, articleId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{articleId:guid}/edit/{commentId:guid}")]
        public async Task<IActionResult> ЕditComment(Guid articleId, Guid commentId, CommentRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new EditComment(articleId, commentId, model.Content);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{articleId:guid}/delete/{commentId:guid}")]
        public async Task<IActionResult> DeleteComment(Guid articleId, Guid commentId)
        {
            var command = new DeleteComment(articleId, commentId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}