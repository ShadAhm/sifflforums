using Microsoft.EntityFrameworkCore;
using SifflForums.Data;
using SifflForums.Data.Entities;
using SifflForums.Service.Models.Dto;
using SifflForums.Service.Common;
using SifflForums.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SifflForums.Data.Interfaces;
using SifflForums.Service.Mapping;

namespace SifflForums.Service
{
    public interface ISubmissionsService : IUpvotablesService
    {
        Task<PaginatedListResult<SubmissionModel>> GetPagedAsync(string currentUsername, string forumSectionId, string sortType, int pageIndex, int pageSize);
        SubmissionModel Insert(string currentUsername, SubmissionModel value);
        SubmissionModel GetById(string currentUsername, string id);
        SubmissionModel Update(string currentUsername, SubmissionModel input);
    }

    public class SubmissionsService : ISubmissionsService
    {
        private readonly SifflContext _dbContext;
        private readonly IUsersService _usersService;
        private readonly IUpvotesService _upvotesService;

        public SubmissionsService(SifflContext dbContext, IUsersService usersService, IUpvotesService upvotesService)
        {
            _dbContext = dbContext;
            _usersService = usersService;
            _upvotesService = upvotesService;
        }

        public async Task<PaginatedListResult<SubmissionModel>> GetPagedAsync(string currentUsername, string forumSectionId, string sortType, int pageIndex, int pageSize)
        {
            IQueryable<Submission> queryable = _dbContext.Submissions
                .Include(o => o.User)
                .Include(o => o.Comments)
                .Include(o => o.VotingBox)
                .ThenInclude(o => o.Upvotes)
                .ThenInclude(o => o.User);

            if (!string.IsNullOrEmpty(forumSectionId))
                queryable = queryable.Where(o => o.ForumSectionId == forumSectionId);

            switch(sortType)
            {
                case SortType.New:
                    queryable = queryable.OrderByDescending(o => o.CreatedAtUtc);
                    break;
                case SortType.Top:
                    queryable = queryable.OrderByDescending(o => o.VotingBox.Upvotes.Sum(l => l.Weight));
                    break;
            }

            var paginatedList = await PaginatedList<Submission>
                .CreateAsync<SubmissionModel>(queryable, MapToDto(currentUsername), pageIndex, pageSize);

            return paginatedList.ToPagedResult();
        }

        private Func<Submission, SubmissionModel> MapToDto(string currentUsername)
        {
            return entity =>
            {
                var dto = entity.ToModel();

                if (string.IsNullOrWhiteSpace(currentUsername))
                    return dto;

                dto.CurrentUserVoteWeight = entity.VotingBox.Upvotes.SingleOrDefault(uv => uv.User.UserName == currentUsername)?.Weight ?? 0;
                 
                return dto;
            };
        }

        public SubmissionModel GetById(string currentUsername, string id)
        {
            var vm = _dbContext.Submissions
                .Where(s => s.Id == id)
                .Include(s => s.User)
                .Include(s => s.Comments)
                .Include(s => s.VotingBox)
                .ThenInclude(s => s.Upvotes)
                .ThenInclude(s => s.User)
                .AsEnumerable()
                .Select(MapToDto(currentUsername))
                .SingleOrDefault(s => s.SubmissionId == id);

            return vm;
        }

        public SubmissionModel Insert(string currentUsername, SubmissionModel input)
        {
            var user = _usersService.GetByUsername(currentUsername);

            var entity = input.ToEntity();
            entity.UserId = user.UserId;
            entity.CreatedAtUtc = DateTime.UtcNow;
            entity.CreatedBy = user.UserId;
            entity.ModifiedAtUtc = DateTime.UtcNow;
            entity.ModifiedBy = user.UserId;
            entity.VotingBox = new VotingBox();

            _dbContext.Submissions.Add(entity);
            _dbContext.SaveChanges();

            // automatic upvote from the creator of the thread
            _upvotesService.Vote(currentUsername, entity.Id, this, false);

            return entity.ToModel();
        }

        public SubmissionModel Update(string currentUsername, SubmissionModel input)
        {
            var user = _usersService.GetByUsername(currentUsername);
            var submission = _dbContext.Submissions.Where(o => o.Id == input.SubmissionId && o.CreatedBy == user.UserId);

            if (submission != null)
            {
                Submission entity = new Submission();
                entity.Text = input.Text;
                entity.ModifiedAtUtc = DateTime.UtcNow;
                entity.ModifiedBy = user.UserId;

                _dbContext.Submissions.Update(entity);
                _dbContext.SaveChanges();

                return entity.ToModel();
            }
            return null;
        }

        public IUpvotableEntity ResolveUpvotableEntity(string entityId)
        {
            return _dbContext.Submissions.Find(entityId); 
        }
    }
}
