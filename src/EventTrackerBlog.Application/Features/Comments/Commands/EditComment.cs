using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Domain.Features.Comments.Commands
{
    public class EditComment : IRequest<CommentResponseModel>
    {
        public Guid ArticleId { get; set; }
        public Guid CommentId { get; }
        public string Content { get; }

        public EditComment(Guid articleId, Guid commentId, string content)
        {
            ArticleId = articleId;
            CommentId = commentId;
            Content = content;
        }

        public class EditCommentHandler : IRequestHandler<EditComment, CommentResponseModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;
            public EditCommentHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CommentResponseModel> Handle(EditComment request, CancellationToken cancellationToken)
            {
                var article = await _context.Articles
                    .Include(a => a.Comments)
                    .FirstOrDefaultAsync(a => a.Id == request.ArticleId, cancellationToken: cancellationToken);

                if (article == null)
                {
                    throw new ItemDoesNotExistException();
                }

                var commentToEdit = article.Comments
                    .FirstOrDefault(c => c.Id == request.CommentId);

                if (commentToEdit == null)
                {
                    throw new ItemDoesNotExistException();
                }

                commentToEdit.Content = request.Content;
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<CommentResponseModel>(commentToEdit);
            }
        }
    }
}