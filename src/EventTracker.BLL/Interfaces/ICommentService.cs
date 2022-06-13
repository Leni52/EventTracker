using EventTracker.DAL.Entities;
using EventTracker.DTO.CommentModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.BLL.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(Comment commentRequest);
        Task DeleteCommentAsync(Guid commentId);
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(Guid commentId);
        Task EditCommentAsync(Comment commentRequest, Guid commentId);
        Task<IEnumerable<Comment>> GetCommentsFromEventAsync(Guid eventId);
    }
}