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
        public ActionResult<IEnumerable<SubmissionModel>> Get()
        {
            return _service.GetAll(this.CurrentUsername); 
        }

        // GET api/values/5
        [HttpGet("{id}"),AllowAnonymous,Authorize]
        public ActionResult<SubmissionModel> Get(int id)
        {
            return _service.GetById(this.CurrentUsername, id);
        }

        // POST api/values
        [HttpPost,Authorize]
        public ActionResult<SubmissionModel> Post([FromBody]SubmissionModel value)
        {
            return _service.Insert(this.CurrentUsername, value);
        }
         
        // PUT api/values
        [HttpPut, Authorize]
        public ActionResult<SubmissionModel> Put([FromBody]SubmissionModel value)
        {
            return _service.Update(this.CurrentUsername, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}"), Authorize]
        public ActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status403Forbidden); 
        }

        [HttpPut("{id}/Upvote"), Authorize]
        public ActionResult Upvote(int id, [FromQuery]int votingBoxId)
        {
            if (votingBoxId == 0)
                return BadRequest("No votingbox specified"); 

            _upvotesService.CastVote(this.CurrentUsername, votingBoxId, false);

            return Ok();
        }

        [HttpPut("{id}/Downvote"), Authorize]
        public ActionResult Downvote(int id, [FromQuery]int votingBoxId)
        {
            if (votingBoxId == 0)
                return BadRequest("No votingbox specified");

            _upvotesService.CastVote(this.CurrentUsername, votingBoxId, true);

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}/RemoveVotes"), Authorize]
        public ActionResult RemoveVotes(int id, [FromQuery]int votingBoxId)
        {
            if (votingBoxId == 0)
                return BadRequest("No votingbox specified");

            _upvotesService.RemoveVotes(this.CurrentUsername, votingBoxId);

            return Ok(); 
        }
    }
}
