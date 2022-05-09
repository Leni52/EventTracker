using AutoMapper;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using EventTracker.DTO.CommentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment != null) return comment;
            throw new Exception("Comment doesn't exist.");
        }

        public async Task CreateCommentAsync(CommentCreateModel commentRequest)
        {
            var comment = _mapper.Map<Comment>(commentRequest);
            comment.CreatedAt = DateTime.Now;
            comment.LastModifiedAt = DateTime.Now;

            await _commentRepository.CreateAsync(comment);
            await _commentRepository.SaveAsync();
        }

        public async Task EditCommentAsync(CommentEditModel commentRequest, Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new Exception("Comment doesn't exist.");
            }

            comment = _mapper.Map<Comment>(commentRequest);
            comment.LastModifiedAt = DateTime.Now;

            _commentRepository.Update(comment);
            await _commentRepository.SaveAsync();
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var commentToDelete = await _commentRepository.GetByIdAsync(commentId);
            if (commentToDelete == null)
            {
                throw new Exception("Comment doesn't exist.");
            }

            _commentRepository.Delete(commentToDelete);
            await _commentRepository.SaveAsync();
        }
    }
}
