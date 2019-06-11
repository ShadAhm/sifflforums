using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SifflForums.Api.Models;
using SifflForums.Data;
using SifflForums.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Services
{
    public interface ICommentThreadsService
    {
        List<CommentThreadViewModel> GetAll();
        CommentThreadViewModel Insert(CommentThreadViewModel value);
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

        public CommentThreadViewModel Insert(CommentThreadViewModel input)
        {
            CommentThread thread = new CommentThread();
            thread.Title = input.Title;
            thread.Text = input.Text;
            thread.UserId = 1;
            thread.CreatedAtUtc = DateTime.UtcNow;
            thread.CreatedBy = 1;
            thread.ModifiedAtUtc = DateTime.UtcNow;
            thread.ModifiedBy = 1;

            _dbContext.CommentThreads.Add(thread);
            _dbContext.SaveChanges();

            return _mapper.Map<CommentThreadViewModel>(thread);
        }
    }
}
