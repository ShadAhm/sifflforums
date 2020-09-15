using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Service.Models.Dto;

namespace SifflForums.Service.AutoMapperProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<ApplicationUser, UserModel>().ReverseMap();
        }
    }
}
