using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Models;

namespace SifflForums.AutoMapperProfiles
{
    public class SubmissionsProfile : Profile
    {
        public SubmissionsProfile()
        {
            CreateMap<Submission, SubmissionViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
        }
    }
}
