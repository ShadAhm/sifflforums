using SifflForums.Data;
using SifflForums.Service.Mapping;
using SifflForums.Service.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SifflForums.Service
{
    public interface IForumSectionsService
    {
        ForumSectionModel GetById(string id);
        List<ForumSectionModel> GetAll();
        ForumSectionModel Insert(string currentUsername, ForumSectionModel input);
        ForumSectionModel Update(string currentUsername, ForumSectionModel input);
    }

    public class ForumSectionsService : IForumSectionsService
    {
        private readonly SifflContext _dbContext;
        private readonly IUsersService _usersService;

        public ForumSectionsService(SifflContext dbContext, IUsersService usersService)
        {
            this._dbContext = dbContext;
            this._usersService = usersService;
        }

        public ForumSectionModel GetById(string id)
        {
            var entity = _dbContext.ForumSections
                .SingleOrDefault(fs => fs.Id == id);

            return entity.ToModel();
        }

        public List<ForumSectionModel> GetAll()
        {
            var entities = _dbContext.ForumSections.ToList();

            return entities.Select(e => e.ToModel()).ToList();
        }

        public ForumSectionModel Insert(string currentUsername, ForumSectionModel input)
        {
            var user = _usersService.GetByUsername(currentUsername);

            var entity = input.ToEntity();
            entity.CreatedAtUtc = DateTime.UtcNow;
            entity.CreatedBy = user.UserId;
            entity.ModifiedAtUtc = DateTime.UtcNow;
            entity.ModifiedBy = user.UserId;

            _dbContext.ForumSections.Add(entity);
            _dbContext.SaveChanges();

            return entity.ToModel();
        }

        public ForumSectionModel Update(string currentUsername, ForumSectionModel input)
        {
            throw new NotImplementedException();
        }
    }
}
