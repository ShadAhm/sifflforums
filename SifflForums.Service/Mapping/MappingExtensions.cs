using SifflForums.Data.Entities;
using SifflForums.Service.Models.Dto;
using System.Linq;

namespace SifflForums.Service.Mapping
{
    public static class MappingExtensions
    {
        public static UserModel ToModel(this ApplicationUser entity)
        {
            if (entity == null)
                return null;

            return new UserModel
            {
                UserId = entity.Id,
                Username = entity.UserName
            };
        }

        public static ForumSectionModel ToModel(this ForumSection entity)
        {
            if (entity == null)
                return null;

            return new ForumSectionModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsPrivate = entity.IsPrivate
            };
        }

        public static ForumSection ToEntity(this ForumSectionModel model)
        {
            return new ForumSection
            {
                Name = model.Name,
                Description = model.Description,
                IsPrivate = model.IsPrivate
            };
        }

        public static SubmissionModel ToModel(this Submission entity)
        {
            if (entity == null)
                return null;

            return new SubmissionModel
            {
                Id = entity.Id,
                SubmissionId = entity.Id,
                Title = entity.Title,
                Text = entity.Text,
                UserId = entity.UserId,
                Username = entity.User?.UserName,
                CreatedAtUtc = entity.CreatedAtUtc,
                Upvotes = SumUpvotes(entity.VotingBox),
                CommentsCount = entity.Comments?.Count ?? 0,
                ForumSectionId = entity.ForumSectionId
            };
        }

        // Ids, ownership, audit fields and navigations are set server-side, never taken from the client
        public static Submission ToEntity(this SubmissionModel model)
        {
            return new Submission
            {
                Title = model.Title,
                Text = model.Text,
                ForumSectionId = model.ForumSectionId
            };
        }

        public static CommentModel ToModel(this Comment entity)
        {
            if (entity == null)
                return null;

            return new CommentModel
            {
                Id = entity.Id,
                SubmissionId = entity.SubmissionId,
                Username = entity.User?.UserName,
                Text = entity.Text,
                Upvotes = SumUpvotes(entity.VotingBox),
                CreatedAtUtc = entity.CreatedAtUtc
            };
        }

        public static Comment ToEntity(this CommentModel model)
        {
            return new Comment
            {
                SubmissionId = model.SubmissionId,
                Text = model.Text
            };
        }

        private static int SumUpvotes(VotingBox votingBox)
        {
            return votingBox?.Upvotes?.Sum(uv => uv.Weight) ?? 0;
        }
    }
}
