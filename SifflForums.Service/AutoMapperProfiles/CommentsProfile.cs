using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Models;
using System.Linq;

namespace SifflForums.AutoMapperProfiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Upvotes, opt => opt.MapFrom(src => src.VotingBox.Upvotes.Sum(uv => uv.Weight)));
        }
    }
}
