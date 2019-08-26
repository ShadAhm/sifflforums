using AutoMapper;
using SifflForums.Data;
using SifflForums.Service.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SifflForums.Service
{
    public interface IForumSectionsService
    {
        ForumSectionModel GetById(int id);
        List<ForumSectionModel> GetAll();
        ForumSectionModel Insert(string username, ForumSectionModel input);
        ForumSectionModel Update(string username, ForumSectionModel input);
    }

    public class ForumSectionsService : IForumSectionsService
    {
        private readonly SifflContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public ForumSectionsService(SifflContext dbContext, IMapper mapper, IUsersService usersService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._usersService = usersService;
        }

        public ForumSectionModel GetById(int id)
        {
            var entity = _dbContext.ForumSections
                .SingleOrDefault(o => o.ForumSectionId == id);

            return _mapper.Map<ForumSectionModel>(entity);
        }

        public List<ForumSectionModel> GetAll()
        {
            var entities = _dbContext.ForumSections.ToList();

            return _mapper.Map<List<ForumSectionModel>>(entities);
        }

        public ForumSectionModel Insert(string username, ForumSectionModel input)
        {
            throw new NotImplementedException();
        }

        public ForumSectionModel Update(string username, ForumSectionModel input)
        {
            throw new NotImplementedException();
        }
    }
}
