using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Service.Models.Dto;

namespace SifflForums.Service.AutoMapperProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<ApplicationUser, UserModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
        }
    }
}
