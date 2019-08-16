using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Models.Dto;

namespace SifflForums.AutoMapperProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserModel>();
        }
    }
}
