﻿using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using EventTracker.DTO.Requests;
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

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            return await _commentRepository.GetByIdAsync(commentId);
        }

        public async Task CreateCommentAsync(CommentRequestDTO commentRequest)
        {
            var commentToCreate = new Comment()
            {
                Text = commentRequest.Text,
                EventId = commentRequest.EventId,
                CreatedAt = DateTime.Now,
                LastModifiedAt = DateTime.Now
            };

            await _commentRepository.CreateAsync(commentToCreate);
            await _commentRepository.SaveAsync();
        }

        public async Task UpdateCommentAsync(CommentRequestDTO commentRequest, Guid commentId)
        {
            var commentToUpdate = await _commentRepository.GetByIdAsync(commentId);
            if (commentToUpdate == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            commentToUpdate.Text = commentRequest.Text;
            commentToUpdate.LastModifiedAt = DateTime.Now;

            _commentRepository.Update(commentToUpdate);
            await _commentRepository.SaveAsync();
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var commentToDelete = await _commentRepository.GetByIdAsync(commentId);
            if (commentToDelete == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            _commentRepository.Delete(commentToDelete);
            await _commentRepository.SaveAsync();
        }
    }
}
