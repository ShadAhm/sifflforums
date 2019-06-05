using ShadAhm.SifflForums.Api.Models;
using ShadAhm.SifflForums.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadAhm.SifflForums.Api.Services
{
    public interface ICommentsService
    {
        List<CommentViewModel> GetCommentsBySubmissionId(int submissionId);
    }

    public class CommentsService : ICommentsService 
    {
        private SifflContext _dbContext; 

        public CommentsService(SifflContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public List<CommentViewModel> GetCommentsBySubmissionId(int submissionId)
        {
            var comments = _dbContext.Comments.ToList(); 

            return new List<CommentViewModel> { new CommentViewModel() { Username = "dsako", Text = "this is the first comment" } }; 
        }
    }
}
