using EventTrackerBlog.Application.Commands.Comments;
using EventTrackerBlog.Application.Queries.Comments;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Request;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

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

        [HttpGet("{articleId}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(Guid articleId)
        {
            var query = new GetAllCommentsQuery(articleId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(Guid id)
        {
            var query = new GetCommentByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

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

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var command = new DeleteCommentCommand
            {
                CommentId = id
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
