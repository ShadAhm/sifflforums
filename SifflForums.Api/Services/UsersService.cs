using AutoMapper;
using SifflForums.Api.Models;
using SifflForums.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Services
{
    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetAll();
        UserViewModel GetByUsername(string username);
    }

    public class UsersService : IUsersService
    {
        private SifflContext _dbContext;
        private IMapper _mapper;

        public UsersService(SifflContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            var entities = _dbContext.Users.Take(500).ToList();

            return _mapper.Map<IEnumerable<UserViewModel>>(entities);
        }

        public UserViewModel GetByUsername(string username)
        {
            var entity = _dbContext.Users.SingleOrDefault(o => o.Username == username);

            return _mapper.Map<UserViewModel>(entity);
        }
    }
}
