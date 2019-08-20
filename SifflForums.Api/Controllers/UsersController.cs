using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SifflForums.Service.Models.Dto;
using SifflForums.Service;
using System.Collections.Generic;

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
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        [HttpGet("{username}")]
        public ActionResult<UserModel> Get(string username)
        {
            return _service.GetByUsername(username);
        }

        [HttpPost]
        public ActionResult Post([FromBody]object value)
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
