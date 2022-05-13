using AutoMapper;
using EventTracker.DAL.Entities;
using EventTracker.DTO.UserModels;

namespace EventTracker.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ExternalUser, UserRequestDTO>()
                .ReverseMap();
            CreateMap<ExternalUser, UserResponseDTO>()
                .ReverseMap();
        }
    }
}
