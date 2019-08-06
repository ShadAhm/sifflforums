using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SifflForums.Models;
using SifflForums.Data;
using SifflForums.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SifflForums.Service
{
    public interface ISubmissionsService
    {
        List<SubmissionViewModel> GetAll(string currentUsername);
        SubmissionViewModel Insert(string username, SubmissionViewModel value);
        SubmissionViewModel GetById(string currentUsername, int id);
        SubmissionViewModel Update(string username, SubmissionViewModel input);
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

        public List<SubmissionViewModel> GetAll(string currentUsername)
        {
            var entities = _dbContext.Submissions
                .Include(o => o.User)
                .Include(o => o.VotingBox)
                .ThenInclude(o => o.Upvotes)
                .ThenInclude(o => o.User)
                .AsEnumerable()
                .Select(MapToSubmissionVm(currentUsername))
                .ToList();

            return entities;
        }

        private Func<Submission, SubmissionViewModel> MapToSubmissionVm(string currentUsername)
        {
            return o =>
            {
                if (string.IsNullOrWhiteSpace(currentUsername))
                    return _mapper.Map<Submission, SubmissionViewModel>(o);

                var vm = _mapper.Map<Submission, SubmissionViewModel>(o);
                vm.CurrentUserVoteWeight = o.VotingBox.Upvotes.SingleOrDefault(uv => uv.User.Username == currentUsername)?.Weight ?? 0;

                return vm;
            };
        }

        public SubmissionViewModel GetById(string currentUsername, int id)
        {
            var entity = _dbContext.Submissions
                .Include(o => o.User)
                .Include(o => o.VotingBox)
                .ThenInclude(o => o.Upvotes)
                .ThenInclude(o => o.User)
                .SingleOrDefault(o => o.SubmissionId == id);

            var viewModel = _mapper.Map<SubmissionViewModel>(entity);

            int userVoteWeight = 0;
            if (!string.IsNullOrWhiteSpace(currentUsername))
            {
                userVoteWeight = entity.VotingBox.Upvotes.SingleOrDefault(o => o.User.Username == currentUsername)?.Weight ?? 0;
            }
            viewModel.CurrentUserVoteWeight = userVoteWeight; 

            return viewModel;
        }

        public SubmissionViewModel Insert(string username, SubmissionViewModel input)
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

            _dbContext.Submissions.Add(entity);
            _dbContext.SaveChanges();

            // automatic upvote from the creator of the thread
            _upvotesService.CastVote(username, entity.VotingBox.VotingBoxId, false); 

            return _mapper.Map<SubmissionViewModel>(entity);
        }

        public SubmissionViewModel Update(string username, SubmissionViewModel input)
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

                return _mapper.Map<SubmissionViewModel>(entity);
            }
            return null; 
        }
    }
}
