using AutoMapper;
using SifflForums.Data.Entities;
using SifflForums.Service.Models.Dto;
using System.Linq;

namespace SifflForums.Service.AutoMapperProfiles
{
    public class SubmissionsProfile : Profile
    {
        public SubmissionsProfile()
        {
            CreateMap<Submission, SubmissionModel>()
                .ForMember(dest => dest.SubmissionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()))
                .ForMember(dest => dest.Upvotes, opt => opt.MapFrom(src => src.VotingBox.Upvotes.Sum(uv => uv.Weight)));

            // Ids, ownership and navigations are set server-side, never taken from the client
            CreateMap<SubmissionModel, Submission>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.VotingBox, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());
        }
    }
}
