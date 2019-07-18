using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SifflForums.Models;
using SifflForums.Service;
using System.Collections.Generic;

namespace SifflForums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : SifflControllerBase
    {
        ICommentsService _service; 

        public CommentsController(ICommentsService service)
        {
            _service = service; 
        }

        // GET api/values
        [HttpGet()]
        public ActionResult<IEnumerable<CommentViewModel>> Get(int submissionId)
        {
            return _service.GetBySubmissionId(submissionId); 
        }

        // POST api/values
        [HttpPost, Authorize]
        public ActionResult<CommentViewModel> Post([FromBody]CommentViewModel value)
        {
            return _service.Insert(HttpContext.User.Identity.Name, value); 
        }

        // PUT api/values/5
        [HttpPut("{id}"), Authorize]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        // DELETE api/values/5
        [HttpDelete("{id}"), Authorize]
        public ActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }
    }
}
