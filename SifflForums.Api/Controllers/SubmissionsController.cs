using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SifflForums.Service.Models.Dto;
using SifflForums.Service;
using SifflForums.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<PaginatedListResult<SubmissionModel>>> Get(int forumSectionId, string sort, int pageIndex, int pageSize)
        {
            return await _service.GetPagedAsync(this.CurrentUsername, forumSectionId, sort, pageIndex, pageSize);
        }

        // GET api/values/5
        [HttpGet("{id}"), AllowAnonymous, Authorize]
        public ActionResult<SubmissionModel> Get(int id)
        {
            return _service.GetById(this.CurrentUsername, id);
        }

        // POST api/values
        [HttpPost, Authorize]
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
        public ActionResult Upvote(int id)
        {
            if (id == 0)
                return BadRequest("No submission specified");

            _upvotesService.Vote(this.CurrentUsername, id, _service, false);

            return Ok();
        }

        [HttpPut("{id}/Downvote"), Authorize]
        public ActionResult Downvote(int id)
        {
            if (id == 0)
                return BadRequest("No submission specified");

            _upvotesService.Vote(this.CurrentUsername, id, _service, true);

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}/RemoveVotes"), Authorize]
        public ActionResult RemoveVotes(int id)
        {
            if (id == 0)
                return BadRequest("No submission specified");

            _upvotesService.RemoveVotes(this.CurrentUsername, id, _service);

            return Ok();
        }
    }
}
