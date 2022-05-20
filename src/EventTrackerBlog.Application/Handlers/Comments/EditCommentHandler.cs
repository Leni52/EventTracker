using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class EditCommentHandler : IRequestHandler<EditCommentCommand, CommentEditResponseModel>
    {
        private readonly BlogDbContext _context;

        public EditCommentHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<CommentEditResponseModel> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToEdit = _context.Comments
                .FirstOrDefault(c => c.Id == request.Id);

            if (commentToEdit == null)
            {
                return null;
            }

            commentToEdit.Content = request.Content;
            await _context.SaveChangesAsync(cancellationToken);

            return new CommentEditResponseModel
            {
                Id = commentToEdit.Id,
                Content = commentToEdit.Content,
                ArticleId = commentToEdit.ArticleId
            };
        }
    }
}
