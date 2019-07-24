using AutoMapper;
using SifflForums.Data;
using SifflForums.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SifflForums.Service
{
    public interface IUpvotesService
    {
        void CastVote(string username, int votingBoxId, bool isDownvote);
        void RemoveVotes(string username, int votingBoxId);
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

        public void CastVote(string username, int votingBoxId, bool isDownvote)
        {
            var user = _usersService.GetByUsername(username);
            if (user == null) { return; }

            int voteWeight = isDownvote ? -1 : 1;

            Upvote entity = new Upvote();
            entity.UserId = user.UserId;
            entity.Weight = voteWeight;

            var votes = _dbContext.Upvotes.Where(uv => uv.VotingBoxId == votingBoxId && uv.UserId == user.UserId).ToList();

            if(votes == null)
            {
                entity.VotingBoxId = votingBoxId;
                _dbContext.Upvotes.Add(entity);
                _dbContext.SaveChanges();
                return; 
            }

            if (votes != null && votes.Count == 1 && votes.First().Weight == voteWeight)
            {
                return; 
            }
            else
            {
                _dbContext.RemoveRange(votes);
                entity.VotingBoxId = votingBoxId;
                _dbContext.Upvotes.Add(entity);
                _dbContext.SaveChanges();
            }
        }

        public void RemoveVotes(string username, int votingBoxId)
        {
            var user = _usersService.GetByUsername(username);
            if (user == null) { return; }

            IEnumerable<Upvote> votes = null;

            votes = _dbContext.Upvotes.Where(uv => uv.VotingBoxId == votingBoxId && uv.UserId == user.UserId).ToList();

            if (votes != null)
            {
                _dbContext.Upvotes.RemoveRange(votes);
                _dbContext.SaveChanges();
            }
        }
    }
}
