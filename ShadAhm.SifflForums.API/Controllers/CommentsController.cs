using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShadAhm.SifflForums.Api.Models;
using ShadAhm.SifflForums.Api.Services;

namespace ShadAhm.SifflForums.Api.Controllers
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
        [HttpGet]
        public ActionResult<IEnumerable<CommentViewModel>> Get(int commentThreadId)
        {
            return _service.GetCommentsByCommentThreadId(commentThreadId); 
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]CommentViewModel value)
        {
            _service.Insert(value); 
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
