using AutoMapper;
using SifflForums.Data;
using SifflForums.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SifflForums.Service
{
    public interface IUpvotesService
    {
        void CastVote(string username, string parentEntity, int submissionId, bool isDownvote);
        void RemoveVotes(string username, string parentEntity, int parentId);
    }

    public class UpvotesService : IUpvotesService
    {
        private SifflContext _dbContext;
        private IMapper _mapper;
        private IUsersService _usersService;

        public UpvotesService(SifflContext dbContext, IMapper mapper, IUsersService usersService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _usersService = usersService;
        }

        public void CastVote(string username, string parentEntity, int parentId, bool isDownvote)
        {
            var user = _usersService.GetByUsername(username);
            if (user == null) { return; }

            int voteWeight = isDownvote ? -1 : 1; 

            Upvote entity = new Upvote();
            entity.UserId = user.UserId;
            entity.Weight = voteWeight;

            bool proceedable = false; 
            if (parentEntity.Equals(nameof(Submission)))
            {
                var vote = _dbContext.Upvotes.SingleOrDefault(uv => uv.SubmissionId == parentId && uv.UserId == user.UserId && uv.Weight == voteWeight);

                if (vote == null)
                {
                    entity.SubmissionId = parentId;
                    proceedable = true; 
                }
            }
            else if (parentEntity.Equals(nameof(Comment)))
            {
                var upvote = _dbContext.Upvotes.SingleOrDefault(uv => uv.CommentId == parentId && uv.UserId == user.UserId && uv.Weight == voteWeight);

                if (upvote == null)
                {
                    entity.CommentId = parentId;
                    proceedable = true;
                }
            }

            if(proceedable)
            {
                _dbContext.Upvotes.Add(entity);
                _dbContext.SaveChanges();
            }
        }

        public void RemoveVotes(string username, string parentEntity, int parentId)
        {
            var user = _usersService.GetByUsername(username);
            if (user == null) { return; }

            IEnumerable<Upvote> votes = null; 

            if (parentEntity.Equals(nameof(Submission)))
            {
                votes = _dbContext.Upvotes.Where(uv => uv.SubmissionId == parentId && uv.UserId == user.UserId).ToList();
            }
            else if (parentEntity.Equals(nameof(Comment)))
            {
                votes = _dbContext.Upvotes.Where(uv => uv.CommentId == parentId && uv.UserId == user.UserId).ToList();
            }

            if (votes != null)
            {
                _dbContext.Upvotes.RemoveRange(votes);
                _dbContext.SaveChanges();
            }
        }
    }
}
