using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SifflForums.Api.Models;
using SifflForums.Api.Services;

namespace SifflForums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : SifflControllerBase
    {
        ISubmissionsService _service; 

        public SubmissionsController(ISubmissionsService service)
        {
            _service = service; 
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<SubmissionViewModel>> Get()
        {
            return _service.GetAll(); 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<SubmissionViewModel> Get(int id)
        {
            return _service.GetById(id);
        }

        // POST api/values
        [HttpPost,Authorize]
        public ActionResult<SubmissionViewModel> Post([FromBody]SubmissionViewModel value)
        {
            string username = HttpContext.User.Identity.Name;

            return _service.Insert(value);
        }

        // PUT api/values/5
        [HttpPut("{id}"), Authorize]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}"), Authorize]
        public void Delete(int id)
        {
        }
    }
}
