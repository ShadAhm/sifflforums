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

            if (parentEntity.Equals(nameof(Submission)))
            {
                var votes = _dbContext.Upvotes.Where(uv => uv.SubmissionId == parentId && uv.UserId == user.UserId).ToList();

                // TODO: refactor
                if (votes != null && votes.Count > 1)
                {
                    _dbContext.RemoveRange(votes);
                    entity.SubmissionId = parentId;
                    _dbContext.Upvotes.Add(entity);
                    _dbContext.SaveChanges();
                }
                else if (votes != null && votes.Count == 1)
                {
                    if (votes.First().Weight == voteWeight)
                        return;
                    else
                    {
                        _dbContext.RemoveRange(votes);
                        entity.SubmissionId = parentId;
                        _dbContext.Upvotes.Add(entity);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    entity.SubmissionId = parentId;
                    _dbContext.Upvotes.Add(entity);
                    _dbContext.SaveChanges();
                }
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
