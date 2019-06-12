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
    public interface ISubmissionsService
    {
        List<SubmissionViewModel> GetAll();
        SubmissionViewModel Insert(SubmissionViewModel value);
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

        public SubmissionViewModel Insert(SubmissionViewModel input)
        {
            Submission o = new Submission();
            o.Title = input.Title;
            o.Text = input.Text;
            o.UserId = 1;
            o.CreatedAtUtc = DateTime.UtcNow;
            o.CreatedBy = 1;
            o.ModifiedAtUtc = DateTime.UtcNow;
            o.ModifiedBy = 1;

            _dbContext.Submissions.Add(o);
            _dbContext.SaveChanges();

            return _mapper.Map<SubmissionViewModel>(o);
        }
    }
}
