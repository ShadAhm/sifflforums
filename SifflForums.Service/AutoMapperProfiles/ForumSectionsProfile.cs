using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Service.Models.Dto;

namespace SifflForums.Service.AutoMapperProfiles
{
    public class ForumSectionsProfile : Profile
    {
        public ForumSectionsProfile()
        {
            CreateMap<ForumSection, ForumSectionModel>();
            CreateMap<ForumSectionModel, ForumSection>();
        }
    }
}
