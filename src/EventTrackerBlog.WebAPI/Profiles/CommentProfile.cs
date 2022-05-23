using AutoMapper;
using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Domain.DTO.Comments.Request;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using EventTrackerBlog.Domain.Entities;

namespace EventTrackerBlog.WebAPI.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentRequestModel>().ReverseMap();
            CreateMap<Comment, CommentResponseModel>().ReverseMap();
            CreateMap<Comment, CommentEditRequestModel>().ReverseMap();
            CreateMap<Comment, CommentEditResponseModel>().ReverseMap();
            CreateMap<CommentRequestModel, CreateCommentCommand>().ReverseMap();
            CreateMap<CommentEditRequestModel, EditCommentCommand>().ReverseMap();
        }
    }
}