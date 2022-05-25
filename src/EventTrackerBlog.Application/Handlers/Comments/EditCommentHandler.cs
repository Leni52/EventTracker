using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class EditCommentHandler : IRequestHandler<EditCommentCommand, CommentResponseModel>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public EditCommentHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentResponseModel> Handle(EditCommentCommand request, CancellationToken cancellationToken)
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