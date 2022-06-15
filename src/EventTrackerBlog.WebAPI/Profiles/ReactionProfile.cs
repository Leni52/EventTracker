using AutoMapper;
using EventTrackerBlog.Domain.DTO.Reactions.Request;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using EventTrackerBlog.Domain.Entities;

namespace EventTrackerBlog.WebAPI.Profiles
{
    public class ReactionProfile : Profile
    {
        public ReactionProfile()
        {
            CreateMap<Reaction, ReactionRequestModel>()
             .ReverseMap();
            CreateMap<Reaction, ReactionResponseModel>()
             .ReverseMap();
        }
    }
}
