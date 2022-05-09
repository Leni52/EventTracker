using AutoMapper;
using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;

namespace EventTracker.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventRequestModel>()
                .ReverseMap();
            CreateMap<Event, EventResponseModel>()
                .ReverseMap();
        }
    }
}
