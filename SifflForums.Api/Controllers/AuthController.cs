using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SifflForums.Models.Auth;
using SifflForums.Service;

namespace SifflForums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : SifflControllerBase
    {
        IAuthService _authService;
        IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            this._authService = authService;
            this._configuration = configuration; 
        }

        [HttpPost, Route("signup")]
        public ActionResult<TokenModel> SignUp([FromBody]SignUpModel user)
        {
            var result = _authService
                .SetServiceApiKey(_configuration["ServiceApiKey"])
                .SignUp(user);

            if(result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage); 
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            var result = _authService
                .SetServiceApiKey(_configuration["ServiceApiKey"])
                .Login(user);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }

        [HttpPost, Route("validatetoken")]
        public IActionResult ValidateToken([FromBody]TokenModel user)
        {
            return Ok();
        }
    }
}