using EventTrackerBlog.Application.Features.Reactions.Commands;
using EventTrackerBlog.Application.Features.Reactions.Queries;
using EventTrackerBlog.Domain.DTO.Reactions.Request;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventTrackerBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{reactionId:guid}")]
        public async Task<ActionResult<ReactionResponseModel>> GetReactionById(Guid reactionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var getReactions = new GetReactionById(reactionId);
            var result = await _mediator.Send(getReactions);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Reaction>> CreateReaction(ReactionRequestModel requestReaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createReaction = new CreateReaction(requestReaction.CommentId, requestReaction.Type);
            var result = await _mediator.Send(createReaction);

            return Ok(result);
        }

        [HttpPut("{reactionId:guid}")]
        public async Task<IActionResult> EditReaction(Guid reactionId, ReactionRequestModel requestReaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var editReaction = new EditReaction(reactionId, requestReaction.CommentId, requestReaction.Type);
            var result = await _mediator.Send(editReaction);

            return Ok(result);
        }

        [HttpDelete("{reactionId:guid}")]
        public async Task<IActionResult> DeleteReaction(Guid reactionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var deleteReaction = new DeleteReaction(reactionId);
            await _mediator.Send(deleteReaction);

            return Ok();
        }
    }
}
