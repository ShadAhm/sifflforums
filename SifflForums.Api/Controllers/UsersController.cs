using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SifflForums.Api.Models;
using SifflForums.Api.Services;

namespace SifflForums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : SifflControllerBase
    {
        IUsersService _service;

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> Get()
        {
            var result = _service.GetAll();

            return Ok(result); 
        }

        [HttpGet("{username}")]
        public ActionResult<UserViewModel> Get(string username)
        {
            return _service.GetByUsername(username);
        }

        [HttpPost]
        public ActionResult Post([FromBody]SubmissionViewModel value)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        [HttpPut("{id}"), Authorize]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        [HttpDelete("{id}"), Authorize]
        public ActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }
    }
}
