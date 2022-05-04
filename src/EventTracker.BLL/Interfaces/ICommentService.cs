﻿using EventTracker.DAL.Entities;
using EventTracker.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.BLL.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CommentRequestDTO commentRequest);
        Task DeleteCommentAsync(Guid commentId);
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(Guid commentId);
        Task UpdateCommentAsync(CommentRequestDTO commentRequest, Guid commentId);
    }
}