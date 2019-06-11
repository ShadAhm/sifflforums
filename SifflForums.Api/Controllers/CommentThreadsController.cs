using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SifflForums.Api.Models;
using SifflForums.Api.Services;

namespace SifflForums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentThreadsController : SifflControllerBase
    {
        ICommentThreadsService _service; 

        public CommentThreadsController(ICommentThreadsService service)
        {
            _service = service; 
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CommentThreadViewModel>> Get()
        {
            return _service.GetAll(); 
        }

        // POST api/values
        [HttpPost]
        public ActionResult<CommentThreadViewModel> Post([FromBody]CommentThreadViewModel value)
        {
            return _service.Insert(value);
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
