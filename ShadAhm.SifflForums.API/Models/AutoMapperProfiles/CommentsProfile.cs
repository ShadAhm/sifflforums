using AutoMapper;
using ShadAhm.SifflForums.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadAhm.SifflForums.Api.Models.AutoMapperProfiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentViewModel>(); 
        }
    }
}
