using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Commands.Comments;
using EventTrackerBlog.Application.Queries.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Request;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IMediator _mediator;

        public CommentsController(BlogDbContext context, IMediator mediator)
        {
            _context = context;
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

            var command = new CreateCommentCommand
            {
                ArticleId = model.ArticleId,
                Content = model.Content
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> ЕditComment(Guid id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
