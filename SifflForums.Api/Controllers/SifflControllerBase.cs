using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Controllers
{
    public class SifflControllerBase : ControllerBase
    {
        private string _currentUsername;
        protected string CurrentUsername => _currentUsername ?? (_currentUsername = HttpContext.User.Identity.Name);
    }
}
