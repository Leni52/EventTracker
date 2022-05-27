using AutoMapper;
using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.Entities;

namespace EventTrackerBlog.WebAPI.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleRequestModel>()
             .ReverseMap();
            CreateMap<Article, ArticleResponseModel>()
             .ReverseMap();
        }
    }
}
