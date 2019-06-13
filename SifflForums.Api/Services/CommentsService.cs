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
        void Insert(CommentViewModel input); 
        List<CommentViewModel> GetBySubmissionId(int submissionId);
    }

    public class CommentsService : ICommentsService 
    {
        private SifflContext _dbContext;
        private IMapper _mapper; 

        public CommentsService(SifflContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper; 
        }

        public List<CommentViewModel> GetBySubmissionId(int submissionId)
        {
            var comments = _dbContext.Comments
                .Include(c => c.User)
                .Where(c => c.SubmissionId == submissionId)
                .ToList();

            return _mapper.Map<List<CommentViewModel>>(comments);
        }

        public void Insert(CommentViewModel input)
        {
            Comment comment = new Comment();
            comment.SubmissionId = input.SubmissionId;
            comment.Text = input.Text;

            comment.UserId = 1;
            comment.CreatedAtUtc = DateTime.UtcNow;
            comment.CreatedBy = 1;
            comment.ModifiedAtUtc = DateTime.UtcNow;
            comment.ModifiedBy = 1;

            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges(); 
        }
    }
}
