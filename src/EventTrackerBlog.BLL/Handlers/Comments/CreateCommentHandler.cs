using System;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.BLL.Commands.Comments;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.DTO.Comments.Response;
using EventTrackerBlog.DAL.Entities;
using MediatR;

namespace EventTrackerBlog.BLL.Handlers.Comments
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentResponseModel>
    {
        private readonly BlogDbContext _context;

        public CreateCommentHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<CommentResponseModel> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Content = request.Content,
                ArticleId = request.ArticleId,
                CreatedAt = DateTime.Now,
                LastModifiedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: Inject auto mapper
            return new CommentResponseModel
            {
                ArticleId = comment.ArticleId,
                Content = comment.Content
            };
        }
    }
}