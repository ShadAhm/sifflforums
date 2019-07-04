using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SifflForums.Api.Models;
using SifflForums.Data;
using SifflForums.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Services
{
    public interface ICommentsService
    {
        List<CommentViewModel> GetBySubmissionId(int submissionId);
        CommentViewModel Insert(string username, CommentViewModel input); 
    }

    public class CommentsService : ICommentsService 
    {
        private SifflContext _dbContext;
        private IMapper _mapper;
        private IUsersService _usersService;

        public CommentsService(SifflContext dbContext, IMapper mapper, IUsersService usersService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _usersService = usersService;
        }

        public List<CommentViewModel> GetBySubmissionId(int submissionId)
        {
            var comments = _dbContext.Comments
                .Include(c => c.User)
                .Where(c => c.SubmissionId == submissionId)
                .ToList();

            return _mapper.Map<List<CommentViewModel>>(comments);
        }

        public CommentViewModel Insert(string username, CommentViewModel input)
        {
            var user = _usersService.GetByUsername(username);

            Comment comment = new Comment();
            comment.SubmissionId = input.SubmissionId;
            comment.Text = input.Text;

            comment.UserId = user.UserId;
            comment.CreatedAtUtc = DateTime.UtcNow;
            comment.CreatedBy = user.UserId;
            comment.ModifiedAtUtc = DateTime.UtcNow;
            comment.ModifiedBy = user.UserId;

            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();

            _dbContext.Entry(comment).Reference(c => c.User).Load(); 

            return _mapper.Map<CommentViewModel>(comment); 
        }
    }
}
