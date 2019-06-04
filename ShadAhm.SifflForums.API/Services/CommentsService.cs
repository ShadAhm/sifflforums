using ShadAhm.SifflForums.Api.Models;
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
        public List<CommentViewModel> GetCommentsBySubmissionId(int submissionId)
        {
            return new List<CommentViewModel> { new CommentViewModel() { Username = "dsako", Text = "this is the first comment" } }; 
        }
    }
}
