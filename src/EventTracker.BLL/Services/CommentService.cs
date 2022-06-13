﻿using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Entities;
using ExceptionHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _unitOfWork.Comments.GetAllAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new ItemDoesNotExistException();
            }
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsFromEventAsync(Guid eventId)
        {
            Event commentedEvent = await _unitOfWork.Events.GetByIdAsync(eventId);
            return commentedEvent.Comments;
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            comment.LastModifiedAt = DateTime.Now;

            await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditCommentAsync(Comment commentEdit, Guid commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new ItemDoesNotExistException();
            }

            comment.Text = commentEdit.Text;
            comment.LastModifiedAt = DateTime.Now;

            _unitOfWork.Comments.Edit(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var commentToDelete = await _unitOfWork.Comments.GetByIdAsync(commentId);
            if (commentToDelete == null)
            {
                throw new ItemDoesNotExistException();
            }

            _unitOfWork.Comments.Delete(commentToDelete);
            await _unitOfWork.SaveAsync();
        }
    }
}
