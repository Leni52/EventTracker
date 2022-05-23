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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CommentsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("/comments")]
        public async Task<ActionResult<IEnumerable<CommentResponseModel>>> GetAllComments()
        {
            var query = new GetAllCommentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{articleId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentResponseModel>>> GetCommentsByArticle(Guid articleId)
        {
            var query = new GetCommentsByArticleQuery(articleId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponseModel>> GetCommentById(Guid id)
        {
            var query = new GetCommentByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(CommentRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var command = _mapper.Map<CreateCommentCommand>(model);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ЕditComment(CommentEditRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = _mapper.Map<EditCommentCommand>(model);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var command = new DeleteCommentCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}