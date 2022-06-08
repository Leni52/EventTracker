using AutoMapper;
using EventTrackerBlog.Data.Entities;
using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.DTO.Articles.Response;

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
