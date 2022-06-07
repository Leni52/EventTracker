﻿using System;
using AutoMapper;
using EventTrackerBlog.Data.Entities;
using EventTrackerBlog.Domain.DTO.Comments.Request;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using EventTrackerBlog.Domain.Features.Comments.Commands;

namespace EventTrackerBlog.WebAPI.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentRequestModel>().ReverseMap();
            CreateMap<Comment, CommentResponseModel>().ReverseMap();
            CreateMap<CommentRequestModel, CreateComment>().ReverseMap();
            CreateMap<CommentRequestModel, EditComment>().ReverseMap();
            CreateMap<CreateComment, Comment>()
                .ForMember(c => c.CreatedAt, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(c => c.LastModifiedAt, opt => opt.MapFrom(x => DateTime.Now))
                .ReverseMap();
        }
    }
}