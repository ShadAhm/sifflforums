using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SifflForums.Service;
using SifflForums.Service.Models.Dto;
using System.Collections.Generic;

namespace SifflForums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumSectionsController : SifflControllerBase
    {
        IForumSectionsService _service; 

        public ForumSectionsController(IForumSectionsService service)
        {
            this._service = service; 
        }

        [HttpGet("{id}"), AllowAnonymous, Authorize]
        public ActionResult<ForumSectionModel> Get(int id)
        {
            return _service.GetById(id);
        }

        [HttpGet(), AllowAnonymous, Authorize]
        public ActionResult<IEnumerable<ForumSectionModel>> GetAll()
        {
            return _service.GetAll();
        }
    }
}
