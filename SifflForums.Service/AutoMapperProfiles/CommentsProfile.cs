using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Models;

namespace SifflForums.AutoMapperProfiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
        }
    }
}
