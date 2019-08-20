using AutoMapper;
using SifflForums.Data;
using SifflForums.Service.Models.Dto;
using System.Collections.Generic;
using System.Linq;

namespace SifflForums.Service
{
    public interface IUsersService
    {
        IEnumerable<UserModel> GetAll();
        UserModel GetByUsername(string username);
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

        public IEnumerable<UserModel> GetAll()
        {
            var entities = _dbContext.Users.Take(500).ToList();

            return _mapper.Map<IEnumerable<UserModel>>(entities);
        }

        public UserModel GetByUsername(string username)
        {
            var entity = _dbContext.Users.SingleOrDefault(o => o.Username == username);

            return _mapper.Map<UserModel>(entity);
        }
    }
}
