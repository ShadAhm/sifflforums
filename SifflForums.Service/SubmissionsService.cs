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

namespace SifflForums.Service
{
    public interface ISubmissionsService
    {
        Task<PaginatedListResult<SubmissionModel>> GetPagedAsync(string currentUsername, int forumSectionId, string sortType, int pageIndex, int pageSize);
        SubmissionModel Insert(string username, SubmissionModel value);
        SubmissionModel GetById(string currentUsername, int id);
        SubmissionModel Update(string username, SubmissionModel input);
    }

    public class SubmissionsService : ISubmissionsService
    {
        private SifflContext _dbContext;
        private IMapper _mapper;
        private IUsersService _usersService;
        private IUpvotesService _upvotesService;

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
                .CreateAsync<SubmissionModel>(queryable, MapToSubmissionVm(currentUsername), pageIndex, pageSize);


            return paginatedList.ToPagedResult();
        }

        private Func<Submission, SubmissionModel> MapToSubmissionVm(string currentUsername)
        {
            return o =>
            {
                var vm = _mapper.Map<Submission, SubmissionModel>(o);

                if (string.IsNullOrWhiteSpace(currentUsername))
                    return vm;

                vm.CurrentUserVoteWeight = o.VotingBox.Upvotes.SingleOrDefault(uv => uv.User.Username == currentUsername)?.Weight ?? 0;

                return vm;
            };
        }

        public SubmissionModel GetById(string currentUsername, int id)
        {
            var vm = _dbContext.Submissions
                .Include(o => o.User)
                .Include(o => o.VotingBox)
                .ThenInclude(o => o.Upvotes)
                .ThenInclude(o => o.User)
                .AsEnumerable()
                .Select(MapToSubmissionVm(currentUsername))
                .SingleOrDefault(o => o.SubmissionId == id);

            return vm;
        }

        public SubmissionModel Insert(string username, SubmissionModel input)
        {
            var user = _usersService.GetByUsername(username);

            Submission entity = new Submission();
            entity.Title = input.Title;
            entity.Text = input.Text;
            entity.UserId = user.UserId;
            entity.CreatedAtUtc = DateTime.UtcNow;
            entity.CreatedBy = user.UserId;
            entity.ModifiedAtUtc = DateTime.UtcNow;
            entity.ModifiedBy = user.UserId;
            entity.VotingBox = new VotingBox();
            entity.ForumSectionId = input.ForumSectionId; 

            _dbContext.Submissions.Add(entity);
            _dbContext.SaveChanges();

            // automatic upvote from the creator of the thread
            _upvotesService.CastVote(username, entity.VotingBox.VotingBoxId, false);

            return _mapper.Map<SubmissionModel>(entity);
        }

        public SubmissionModel Update(string username, SubmissionModel input)
        {
            var user = _usersService.GetByUsername(username);
            var submission = _dbContext.Submissions.Find(input.SubmissionId);

            if (submission != null)
            {
                Submission entity = new Submission();
                entity.Title = input.Title;
                entity.Text = input.Text;
                entity.ModifiedAtUtc = DateTime.UtcNow;
                entity.ModifiedBy = user.UserId;

                _dbContext.Submissions.Update(entity);
                _dbContext.SaveChanges();

                return _mapper.Map<SubmissionModel>(entity);
            }
            return null;
        }
    }
}
