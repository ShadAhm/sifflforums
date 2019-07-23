using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Models;
using System.Linq; 

namespace SifflForums.AutoMapperProfiles
{
    public class SubmissionsProfile : Profile
    {
        public SubmissionsProfile()
        {
            CreateMap<Submission, SubmissionViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Upvotes, opt => opt.MapFrom(src => src.Upvotes.Sum(uv => uv.Weight)));
        }
    }
}
