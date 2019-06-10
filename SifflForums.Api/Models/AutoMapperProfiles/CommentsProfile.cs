﻿using AutoMapper;
using SifflForums.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Models.AutoMapperProfiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.DisplayName));
        }
    }
}
