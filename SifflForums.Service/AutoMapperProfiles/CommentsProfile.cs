using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Service.Models.Dto;
using System;
using System.Linq;

namespace SifflForums.Service.AutoMapperProfiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Upvotes, opt => opt.MapFrom(src => src.VotingBox.Upvotes.Sum(uv => uv.Weight)));

            CreateMap<CommentModel, Comment>();
        }
    }
}
