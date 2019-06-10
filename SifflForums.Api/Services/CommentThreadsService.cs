using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SifflForums.Api.Models;
using SifflForums.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Services
{
    public interface ICommentThreadsService
    {
        List<CommentThreadViewModel> GetAll();
    }

    public class CommentThreadsService : ICommentThreadsService
    {
        private SifflContext _dbContext;
        private IMapper _mapper;

        public CommentThreadsService(SifflContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<CommentThreadViewModel> GetAll()
        {
            var comments = _dbContext.CommentThreads
                .Include(o => o.User)
                .ToList();

            return _mapper.Map<List<CommentThreadViewModel>>(comments);
        }
    }
}
