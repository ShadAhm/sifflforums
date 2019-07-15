using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SifflForums.Data.Entities;
using SifflForums.Models;
using SifflForums.Service;
using System.Collections.Generic;

namespace SifflForums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : SifflControllerBase
    {
        ISubmissionsService _service;
        IUpvotesService _upvotesService; 

        public SubmissionsController(ISubmissionsService service, IUpvotesService upvotesService)
        {
            _service = service;
            _upvotesService = upvotesService;
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
            return _service.GetById(HttpContext.User.Identity.Name, id);
        }

        // POST api/values
        [HttpPost,Authorize]
        public ActionResult<SubmissionViewModel> Post([FromBody]SubmissionViewModel value)
        {
            return _service.Insert(HttpContext.User.Identity.Name, value);
        }
         
        // PUT api/values/5
        [HttpPut("{id}"), Authorize]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        // DELETE api/values/5
        [HttpDelete("{id}"), Authorize]
        public ActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status403Forbidden); 
        }

        [HttpPut("{id}/Upvote"), Authorize]
        public void Upvote(int id)
        {
            _upvotesService.CastVote(HttpContext.User.Identity.Name, nameof(Submission), id, false);
        }

        [HttpPut("{id}/Downvote"), Authorize]
        public void Downvote(int id)
        {
            _upvotesService.CastVote(HttpContext.User.Identity.Name, nameof(Submission), id, true);
        }

        // DELETE api/values/5
        [HttpDelete("{id}/RemoveVotes"), Authorize]
        public void RemoveVotes(int id)
        {
            _upvotesService.RemoveVotes(HttpContext.User.Identity.Name, nameof(Submission), id);
        }
    }
}
