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
            await _commentService.CreateCommentAsync(commentRequest);
            if (ModelState.IsValid)
            {
                return Ok("Comment created successfully.");
            }

            return BadRequest();
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateCommentAsync(CommentEditModel commentRequest, Guid commentId)
        {
            await _commentService.EditCommentAsync(commentRequest, commentId);
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
    }
}
