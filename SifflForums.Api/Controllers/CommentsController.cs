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
        private readonly ICommentsService _service;
        private readonly IUpvotesService _upvotesService;

        public CommentsController(ICommentsService service, IUpvotesService upvotesService)
        {
            this._service = service;
            this._upvotesService = upvotesService;
        }

        // GET api/values
        [HttpGet(),AllowAnonymous,Authorize]
        public ActionResult<IEnumerable<CommentModel>> Get(int submissionId)
        {
            return _service.GetBySubmissionId(this.CurrentUsername, submissionId); 
        }

        [HttpPost, Authorize]
        public ActionResult<CommentModel> Post([FromBody]CommentModel value)
        {
            return _service.Insert(this.CurrentUsername, value); 
        }

        [HttpPut, Authorize]
        public ActionResult<CommentModel> Put([FromBody]CommentModel value)
        {
            return _service.Update(this.CurrentUsername, value);
        }

        [HttpDelete("{id}"), Authorize]
        public ActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
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
