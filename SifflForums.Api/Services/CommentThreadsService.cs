using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public interface ISubmissionsService
    {
        List<SubmissionViewModel> GetAll();
        SubmissionViewModel Insert(SubmissionViewModel value);
        SubmissionViewModel GetById(int id);
    }

    public class SubmissionsService : ISubmissionsService
    {
        private SifflContext _dbContext;
        private IMapper _mapper;

        public SubmissionsService(SifflContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<SubmissionViewModel> GetAll()
        {
            var comments = _dbContext.Submissions
                .Include(o => o.User)
                .ToList();

            return _mapper.Map<List<SubmissionViewModel>>(comments);
        }

        public SubmissionViewModel GetById(int id)
        {
            var entity = _dbContext.Submissions
                .Include(o => o.User)
                .Include(o => o.Comments)
                .FirstOrDefault();

            return _mapper.Map<SubmissionViewModel>(entity);
        }

        public SubmissionViewModel Insert(SubmissionViewModel input)
        {
            Submission entity = new Submission();
            entity.Title = input.Title;
            entity.Text = input.Text;
            entity.UserId = 1;
            entity.CreatedAtUtc = DateTime.UtcNow;
            entity.CreatedBy = 1;
            entity.ModifiedAtUtc = DateTime.UtcNow;
            entity.ModifiedBy = 1;

            _dbContext.Submissions.Add(entity);
            _dbContext.SaveChanges();

            return _mapper.Map<SubmissionViewModel>(entity);
        }
    }
}
