using AutoMapper;
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

namespace SifflForums.Service
{
    public interface ISubmissionsService : IUpvotablesService
    {
        Task<PaginatedListResult<SubmissionModel>> GetPagedAsync(string currentUsername, int forumSectionId, string sortType, int pageIndex, int pageSize);
        SubmissionModel Insert(string currentUsername, SubmissionModel value);
        SubmissionModel GetById(string currentUsername, int id);
        SubmissionModel Update(string currentUsername, SubmissionModel input);
    }

    public class SubmissionsService : ISubmissionsService
    {
        private readonly SifflContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IUpvotesService _upvotesService;

        public SubmissionsService(SifflContext dbContext, IMapper mapper, IUsersService usersService, IUpvotesService upvotesService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _usersService = usersService;
            _upvotesService = upvotesService;
        }

        public async Task<PaginatedListResult<SubmissionModel>> GetPagedAsync(string currentUsername, int forumSectionId, string sortType, int pageIndex, int pageSize)
        {
            IQueryable<Submission> queryable = _dbContext.Submissions
                .Include(o => o.User)
                .Include(o => o.Comments)
                .Include(o => o.VotingBox)
                .ThenInclude(o => o.Upvotes)
                .ThenInclude(o => o.User);

            if (forumSectionId > 0)
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
                var dto = _mapper.Map<Submission, SubmissionModel>(entity);

                if (string.IsNullOrWhiteSpace(currentUsername))
                    return dto;

                dto.CurrentUserVoteWeight = entity.VotingBox.Upvotes.SingleOrDefault(uv => uv.User.Username == currentUsername)?.Weight ?? 0;
                 
                return dto;
            };
        }

        public SubmissionModel GetById(string currentUsername, int id)
        {
            var vm = _dbContext.Submissions
                .Include(s => s.User)
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

            var entity = _mapper.Map<SubmissionModel, Submission>(input, opt => opt.AfterMap((src, dest) =>
            {
                dest.CreatedAtUtc = DateTime.UtcNow;
                dest.UserId = user.UserId;
                dest.CreatedAtUtc = DateTime.UtcNow;
                dest.CreatedBy = user.UserId;
                dest.ModifiedAtUtc = DateTime.UtcNow;
                dest.ModifiedBy = user.UserId;
                dest.VotingBox = new VotingBox();
            }));

            _dbContext.Submissions.Add(entity);
            _dbContext.SaveChanges();

            // automatic upvote from the creator of the thread
            _upvotesService.Vote(currentUsername, entity.SubmissionId, this, false);

            return _mapper.Map<SubmissionModel>(entity);
        }

        public SubmissionModel Update(string currentUsername, SubmissionModel input)
        {
            var user = _usersService.GetByUsername(currentUsername);
            var submission = _dbContext.Submissions.Where(o => o.SubmissionId == input.SubmissionId && o.CreatedBy == user.UserId);

            if (submission != null)
            {
                Submission entity = new Submission();
                entity.Text = input.Text;
                entity.ModifiedAtUtc = DateTime.UtcNow;
                entity.ModifiedBy = user.UserId;

                _dbContext.Submissions.Update(entity);
                _dbContext.SaveChanges();

                return _mapper.Map<SubmissionModel>(entity);
            }
            return null;
        }

        public IUpvotable ResolveUpvotableEntity(int entityId)
        {
            return _dbContext.Submissions.Find(entityId); 
        }
    }
}
