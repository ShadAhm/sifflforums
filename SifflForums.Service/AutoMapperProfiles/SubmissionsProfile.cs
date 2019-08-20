﻿using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Service.Models.Dto;
using System.Linq;

namespace SifflForums.Service.AutoMapperProfiles
{
    public class SubmissionsProfile : Profile
    {
        public SubmissionsProfile()
        {
            CreateMap<Submission, SubmissionModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()))
                .ForMember(dest => dest.Upvotes, opt => opt.MapFrom(src => src.VotingBox.Upvotes.Sum(uv => uv.Weight)));
        }
    }
}
