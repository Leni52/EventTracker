using System.Collections.Generic;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Queries
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<CommentResponseModel>>
    {
    }
}