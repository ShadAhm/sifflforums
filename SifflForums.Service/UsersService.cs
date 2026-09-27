using SifflForums.Data;
using SifflForums.Service.Mapping;
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
        private readonly SifflContext _dbContext;

        public UsersService(SifflContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserModel> GetAll()
        {
            var entities = _dbContext.Users.Take(500).ToList();

            return entities.Select(e => e.ToModel()).ToList();
        }

        public UserModel GetByUsername(string username)
        {
            var entity = _dbContext.Users.SingleOrDefault(u => u.UserName == username);

            return entity.ToModel();
        }
    }
}
