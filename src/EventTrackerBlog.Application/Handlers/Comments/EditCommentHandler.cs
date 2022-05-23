using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class EditCommentHandler : IRequestHandler<EditCommentCommand, CommentEditResponseModel>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public EditCommentHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentEditResponseModel> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToEdit = _context.Comments
                .FirstOrDefault(c => c.Id == request.Id);

            if (commentToEdit == null)
            {
                throw new ItemDoesNotExistException();
            }

            commentToEdit.Content = request.Content;
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CommentEditResponseModel>(commentToEdit);
        }
    }
}
