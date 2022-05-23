using System;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Queries
{
    public class GetCommentByIdQuery : IRequest<CommentResponseModel>
    {
        public Guid ArticleId { get; set; }
        public Guid CommentId { get; }

        public GetCommentByIdQuery(Guid articleId, Guid commentId)
        {
            ArticleId = articleId;
            CommentId = commentId;
        }
    }
}