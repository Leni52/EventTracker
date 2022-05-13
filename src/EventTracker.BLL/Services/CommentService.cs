using AutoMapper;
using EventTracker.BLL.Exceptions;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _unitOfWork.Comments.GetAllAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            if (comment != null) return comment;
            throw new ItemDoesNotExistException("Comment doesn't exist.");
        }

        public async Task CreateCommentAsync(CommentCreateModel commentRequest)
        {
            var comment = _mapper.Map<Comment>(commentRequest);
            comment.CreatedAt = DateTime.Now;
            comment.LastModifiedAt = DateTime.Now;

            await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditCommentAsync(CommentEditModel commentRequest, Guid commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new ItemDoesNotExistException("Comment doesn't exist.");
            }

            comment = _mapper.Map<Comment>(commentRequest);
            comment.LastModifiedAt = DateTime.Now;

            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var commentToDelete = await _unitOfWork.Comments.GetByIdAsync(commentId);
            if (commentToDelete == null)
            {
                throw new ItemDoesNotExistException("Comment doesn't exist.");
            }

            _unitOfWork.Comments.Delete(commentToDelete);
            await _unitOfWork.SaveAsync();
        }
    }
}
