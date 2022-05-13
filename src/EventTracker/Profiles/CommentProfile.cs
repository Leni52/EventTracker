using AutoMapper;
using EventTracker.DAL.Entities;
using EventTracker.DTO.CommentModels;

namespace EventTracker.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentCreateModel>()
                .ReverseMap();
            CreateMap<Comment, CommentEditModel>()
                .ReverseMap();
            CreateMap<Comment, CommentViewModel>()
                .ReverseMap();
        }
    }
}
