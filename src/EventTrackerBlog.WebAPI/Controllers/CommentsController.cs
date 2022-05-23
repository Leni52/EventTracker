using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Domain.DTO.Comments.Request;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Domain.DTO.Comments.Response;

namespace EventTrackerBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentResponseModel>>> GetAllComments()
        {
            var query = new GetAllCommentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("/api/{articleId:guid}/comments")]
        public async Task<ActionResult<IEnumerable<CommentResponseModel>>> GetCommentsByArticle(Guid articleId)
        {
            var query = new GetCommentsByArticleQuery(articleId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("/api/{articleId:guid}/comments/{commentId:guid}")]
        public async Task<ActionResult<CommentResponseModel>> GetCommentById(Guid articleId, Guid commentId)
        {
            var query = new GetCommentByIdQuery(articleId, commentId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("/api/{articleId:guid}/comments/new")]
        public async Task<ActionResult<Comment>> CreateComment(CommentRequestModel model, Guid articleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new CreateCommentCommand(model.Content, articleId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("/api/{articleId:guid}/comments/edit/{commentId:guid}")]
        public async Task<IActionResult> ЕditComment(Guid articleId, Guid commentId, CommentRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new EditCommentCommand(articleId, commentId, model.Content);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("/api/{articleId:guid}/comments/delete/{commentId:guid}")]
        public async Task<IActionResult> DeleteComment(Guid articleId, Guid commentId)
        {
            var command = new DeleteCommentCommand(articleId, commentId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}