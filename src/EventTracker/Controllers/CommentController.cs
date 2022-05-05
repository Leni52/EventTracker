using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Entities;
using EventTracker.DTO.CommentModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentResponseDTO>>> GetAllCommentsAsync()
        {
            var events = await _commentService.GetAllCommentsAsync();
            var eventsResponse = new List<CommentResponseDTO>();
            foreach (var e in events)
            {
                eventsResponse.Add(MapComment(e));
            }

            return eventsResponse;
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentResponseDTO>> GetCommentByIdAsync(Guid commentId)
        {
            var commentById = await _commentService.GetCommentByIdAsync(commentId);

            return MapComment(commentById);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(CommentRequestDTO commentRequest)
        {
            await _commentService.CreateCommentAsync(commentRequest);
            if (ModelState.IsValid)
            {
                return Ok("Comment created successfully.");
            }

            return BadRequest();
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateCommentAsync(CommentRequestDTO commentRequest, Guid commentId)
        {
            await _commentService.UpdateCommentAsync(commentRequest, commentId);
            if (ModelState.IsValid)
            {
                return Ok("Comment updated successfully.");
            }

            return BadRequest();
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(Guid commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);
            if (ModelState.IsValid)
            {
                return Ok("Comment deleted successfully.");
            }

            return BadRequest();
        }

        private CommentResponseDTO MapComment(Comment commentEntity)
        {
            var commentMap = new CommentResponseDTO()
            {
                EventId = commentEntity.EventId,
                Text = commentEntity.Text,
            };

            return commentMap;
        }
    }
}
