using EventTracker.BLL.Interfaces;
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
        private readonly DatabaseContext _context;

        public CommentService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
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

            await _context.Comments.AddAsync(commentToCreate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(CommentRequestDTO commentRequest, Guid commentId)
        {
            var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(e => e.Id == commentId);
            if (commentToUpdate == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            commentToUpdate.Text = commentRequest.Text;
            commentToUpdate.LastModifiedAt = DateTime.Now;

            _context.Comments.Update(commentToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var commentToDelete = await GetCommentByIdAsync(commentId);
            if (commentToDelete == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            _context.Comments.Remove(commentToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
