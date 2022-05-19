using System;
using MediatR;

namespace EventTrackerBlog.BLL.Commands.Comments
{
    public class CreateCommentCommand : IRequest<Guid>
    {

    }
}
