using AutoMapper;
using SifflForums.Data;
using SifflForums.Data.Entities;
using SifflForums.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SifflForums.Service
{
    public interface IUpvotesService
    {
        void RemoveVotes(string username, int upvotableEntityId, IUpvotablesService entityService);
        void Vote(string username, int upvotableEntityId, IUpvotablesService entityService, bool isDownvote);
    }

    public class UpvotesService : IUpvotesService
    {
        private readonly SifflContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public UpvotesService(SifflContext dbContext, IMapper mapper, IUsersService usersService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _usersService = usersService;
        }

        public void Vote(string username, int upvotableEntityId, IUpvotablesService entityService, bool isDownvote)
        {
            IUpvotable upvotable = entityService.ResolveUpvotableEntity(upvotableEntityId); 

            var user = _usersService.GetByUsername(username);
            if (user == null) { return; }

            int voteWeight = isDownvote ? -1 : 1;

            Upvote entity = new Upvote();
            entity.UserId = user.UserId;
            entity.Weight = voteWeight;

            var votes = _dbContext.Upvotes.Where(uv => uv.VotingBoxId == upvotable.VotingBoxId && uv.UserId == user.UserId).ToList();

            if(votes == null)
            {
                entity.VotingBoxId = upvotable.VotingBoxId;
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
                entity.VotingBoxId = upvotable.VotingBoxId;
                _dbContext.Upvotes.Add(entity);
                _dbContext.SaveChanges();
            }
        }

        public void RemoveVotes(string username, int upvotableEntityId, IUpvotablesService entityService)
        {
            IUpvotable upvotable = entityService.ResolveUpvotableEntity(upvotableEntityId); 

            var user = _usersService.GetByUsername(username);
            if (user == null) { return; }

            IEnumerable<Upvote> votes = null;

            votes = _dbContext.Upvotes.Where(uv => uv.VotingBoxId == upvotable.VotingBoxId && uv.UserId == user.UserId).ToList();

            if (votes != null)
            {
                _dbContext.Upvotes.RemoveRange(votes);
                _dbContext.SaveChanges();
            }
        }
    }
}
