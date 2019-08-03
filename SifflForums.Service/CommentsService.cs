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
    public interface ICommentsService
    {
        List<CommentViewModel> GetBySubmissionId(string currentUsername, int submissionId);
        CommentViewModel Insert(string username, CommentViewModel input);
        CommentViewModel Update(string username, CommentViewModel input);
    }

    public class CommentsService : ICommentsService 
    {
        private SifflContext _dbContext;
        private IMapper _mapper;
        private IUsersService _usersService;
        private readonly IUpvotesService _upvotesService;

        public CommentsService(SifflContext dbContext, IMapper mapper, IUsersService usersService, IUpvotesService upvotesService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._usersService = usersService;
            this._upvotesService = upvotesService;
        }

        public List<CommentViewModel> GetBySubmissionId(string currentUsername, int submissionId)
        {
            var entities = _dbContext.Comments
                .Include(c => c.User)
                .Include(c => c.VotingBox)
                .ThenInclude(vb => vb.Upvotes)
                .ThenInclude(uv => uv.User)
                .Where(c => c.SubmissionId == submissionId)
                .ToList();

            var vms = _mapper.Map<List<CommentViewModel>>(entities);

            if (!string.IsNullOrWhiteSpace(currentUsername))
            {
                foreach (var item in vms)
                {
                    var entity = entities.SingleOrDefault(o => o.CommentId == item.CommentId);

                    item.CurrentUserVoteWeight = entity.VotingBox.Upvotes.SingleOrDefault(o => o.User.Username == currentUsername)?.Weight ?? 0;
                }
            }

            return vms;
        }

        public CommentViewModel Insert(string username, CommentViewModel input)
        {
            var user = _usersService.GetByUsername(username);

            Comment entity = new Comment();
            entity.SubmissionId = input.SubmissionId;
            entity.Text = input.Text;

            entity.UserId = user.UserId;
            entity.CreatedAtUtc = DateTime.UtcNow;
            entity.CreatedBy = user.UserId;
            entity.ModifiedAtUtc = DateTime.UtcNow;
            entity.ModifiedBy = user.UserId;
            entity.VotingBox = new VotingBox(); 

            _dbContext.Comments.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.Entry(entity).Reference(c => c.User).Load(); 

            _upvotesService.CastVote(user.Username, entity.VotingBox.VotingBoxId, false);

            return _mapper.Map<CommentViewModel>(entity); 
        }

        public CommentViewModel Update(string username, CommentViewModel input)
        {
            var user = _usersService.GetByUsername(username);
            var entity = _dbContext.Comments.Find(input.CommentId);

            if(entity.UserId != user.UserId)
            {
                // reject action
                return null; 
            }

            if(entity != null)
            {
                entity.Text = input.Text;
                entity.ModifiedAtUtc = DateTime.UtcNow;
                entity.ModifiedBy = user.UserId;

                _dbContext.Comments.Update(entity);
                _dbContext.SaveChanges(); 

                return _mapper.Map<CommentViewModel>(entity);
            }

            return null; 
        }
    }
}
