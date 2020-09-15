using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SifflForums.Data.Services
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserId()
        {
            return _context.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
