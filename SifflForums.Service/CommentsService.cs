using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SifflForums.Data;
using SifflForums.Data.Entities;
using SifflForums.Data.Interfaces;
using SifflForums.Service.Common;
using SifflForums.Service.Models;
using SifflForums.Service.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Service
{
    public interface ICommentsService : IUpvotablesService
    {
        List<CommentModel> GetBySubmissionId(string currentUsername, int submissionId);
        CommentModel Insert(string currentUsername, CommentModel input);
        CommentModel Update(string currentUsername, CommentModel input);
        Task<PaginatedListResult<CommentModel>> GetPagedForSubmissionAsync(string currentUsername, int submissionId, string sortType, int pageIndex, int pageSize);
    }

    public class CommentsService : ICommentsService
    {
        private readonly SifflContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IUpvotesService _upvotesService;

        public CommentsService(SifflContext dbContext, IMapper mapper, IUsersService usersService, IUpvotesService upvotesService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._usersService = usersService;
            this._upvotesService = upvotesService;
        }

        public List<CommentModel> GetBySubmissionId(string currentUsername, int submissionId)
        {
            var comments = _dbContext.Comments
                .Include(c => c.User)
                .Include(c => c.VotingBox)
                .ThenInclude(vb => vb.Upvotes)
                .ThenInclude(uv => uv.User)
                .Where(c => c.SubmissionId == submissionId)
                .Select(MapToDto(currentUsername))
                .ToList();

            return comments;
        }

        public async Task<PaginatedListResult<CommentModel>> GetPagedForSubmissionAsync(string currentUsername, int submissionId, string sortType, int pageIndex, int pageSize)
        {
            IQueryable<Comment> queryable = _dbContext.Comments
                .Include(c => c.User)
                .Include(c => c.VotingBox)
                .ThenInclude(vb => vb.Upvotes)
                .ThenInclude(uv => uv.User)
                .Where(c => c.SubmissionId == submissionId);

            switch (sortType)
            {
                case SortType.New:
                    queryable = queryable.OrderByDescending(o => o.CreatedAtUtc);
                    break;
                case SortType.Top:
                    queryable = queryable.OrderByDescending(o => o.VotingBox.Upvotes.Sum(l => l.Weight));
                    break;
            }

            var paginatedList = await PaginatedList<Comment>
                .CreateAsync<CommentModel>(queryable, MapToDto(currentUsername), pageIndex, pageSize);

            return paginatedList.ToPagedResult();
        }

        private Func<Comment, CommentModel> MapToDto(string currentUsername)
        {
            return entity =>
            {
                var dto = _mapper.Map<Comment, CommentModel>(entity);

                if (string.IsNullOrWhiteSpace(currentUsername))
                    return dto;

                dto.CurrentUserVoteWeight = entity.VotingBox.Upvotes.SingleOrDefault(uv => uv.User.UserName == currentUsername)?.Weight ?? 0;
                return dto;
            };
        }

        public CommentModel Insert(string currentUsername, CommentModel input)
        {
            UserModel user = _usersService.GetByUsername(currentUsername);

            var entity = _mapper.Map<CommentModel, Comment>(input, opt => opt.AfterMap((src, dest) =>
            {
                dest.VotingBox = new VotingBox();
            }));

            _dbContext.Comments.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.Entry(entity).Reference(c => c.User).Load();

            // automatic upvote from the poster
            _upvotesService.Vote(user.Username, entity.Id, this, false);

            _dbContext.Entry(entity.VotingBox).Collection(c => c.Upvotes).Load();

            return MapToDto(currentUsername)(entity); 
        }

        public CommentModel Update(string currentUsername, CommentModel input)
        {
            var user = _usersService.GetByUsername(currentUsername);
            var entity = _dbContext.Comments.Find(input.Id);

            if (entity != null)
            {
                entity.Text = input.Text;
                entity.ModifiedAtUtc = DateTime.UtcNow;
                entity.ModifiedBy = user.UserId;

                _dbContext.Comments.Update(entity);
                _dbContext.SaveChanges();

                return _mapper.Map<CommentModel>(entity);
            }

            return null;
        }

        public IUpvotableEntity ResolveUpvotableEntity(string entityId)
        {
            return _dbContext.Comments.Find(entityId);
        }
    }
}
