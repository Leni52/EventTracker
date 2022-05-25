using System;
using MediatR;
using System.Collections.Generic;
using EventTrackerBlog.Domain.DTO.Comments.Response;

namespace EventTrackerBlog.Application.Features.Comments.Queries
{
    public class GetCommentsByArticleQuery : IRequest<IEnumerable<CommentResponseModel>>
    {
        public Guid ArticleId { get; }

        public GetCommentsByArticleQuery(Guid articleId)
        {
            ArticleId = articleId;
        }
    }
}