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
        public ActionResult<TokenModel> SignUp([FromBody]SignUpViewModel user)
        {
            var result = _authService
                .SetServiceApiKey(_configuration["ServiceApiKey"])
                .SignUp(user);

            return Ok(result.Data);
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginViewModel user)
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