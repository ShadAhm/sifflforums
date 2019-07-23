﻿using AutoMapper;
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
        List<SubmissionViewModel> GetAll();
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

        public List<SubmissionViewModel> GetAll()
        {
            var comments = _dbContext.Submissions
                .Include(o => o.User)
                .ToList();

            return _mapper.Map<List<SubmissionViewModel>>(comments);
        }

        public SubmissionViewModel GetById(string currentUsername, int id)
        {
            var entity = _dbContext.Submissions
                .Include(o => o.User)
                .Include(o => o.Upvotes)
                .ThenInclude(o => o.User)
                .SingleOrDefault(o => o.SubmissionId == id);

            var viewModel = _mapper.Map<SubmissionViewModel>(entity);

            int userVoteWeight = 0;
            if (!string.IsNullOrWhiteSpace(currentUsername))
            {
                userVoteWeight = entity.Upvotes.SingleOrDefault(o => o.User.Username == currentUsername)?.Weight ?? 0;
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

            _dbContext.Submissions.Add(entity);
            _dbContext.SaveChanges();

            // automatic upvote from the creator of the thread
            _upvotesService.CastVote(username, nameof(Submission), entity.SubmissionId, false); 

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
