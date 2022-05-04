using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Entities;
using EventTracker.DTO.Responses;
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
        public async Task<ActionResult<CommentResponseDTO>> GetEventByIdAsync(Guid commentId)
        {
            var commentById = await _commentService.GetCommentByIdAsync(commentId);

            return MapComment(commentById);
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
