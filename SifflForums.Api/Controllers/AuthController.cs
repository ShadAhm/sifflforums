using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SifflForums.Api.Models;
using SifflForums.Api.Models.Auth;
using SifflForums.Api.Services;

namespace SifflForums.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : SifflControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost, Route("signup")]
        public ActionResult<TokenModel> SignUp([FromBody]SignUpViewModel user)
        {
            var result = _authService.SignUp(user);

            if(result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginViewModel user)
        {
            var result = _authService.Login(user, out TokenModel token);

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