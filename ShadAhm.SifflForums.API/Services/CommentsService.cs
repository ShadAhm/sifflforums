using AutoMapper;
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
        List<CommentViewModel> GetCommentsByCommentThreadId(int submissionId);
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

        public List<CommentViewModel> GetCommentsByCommentThreadId(int commentThreadId)
        {
            var comments = _dbContext.Comments
                .Where(c => c.CommentThreadId == commentThreadId)
                .ToList();

            List<CommentViewModel> commentsVm = _mapper.Map<List<CommentViewModel>>(comments);

            return commentsVm; 
        }
    }
}
