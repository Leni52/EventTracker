using AutoMapper;
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
        private static IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper) : base()
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentViewModel>> GetAllCommentsAsync()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentViewModel>> GetCommentByIdAsync(Guid commentId)
        {
            var commentById = await _commentService.GetCommentByIdAsync(commentId);
            return _mapper.Map<CommentViewModel>(commentById);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(CommentCreateModel commentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = _mapper.Map<Comment>(commentRequest);
            await _commentService.CreateCommentAsync(comment);

            return Ok("Comment created successfully.");
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> EditCommentAsync(CommentEditModel commentRequest, Guid commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = _mapper.Map<Comment>(commentRequest);
            await _commentService.EditCommentAsync(comment, commentId);

            return Ok("Comment updated successfully.");
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(Guid commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _commentService.DeleteCommentAsync(commentId);

            return Ok("Comment deleted successfully.");
        }
    }
}
