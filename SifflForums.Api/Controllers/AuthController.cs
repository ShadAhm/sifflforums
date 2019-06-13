using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var token = _authService.SignUp(user);

            return Ok(token); 
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginViewModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            if (user.Username == "johndoe" && user.Password == "def@123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                ); ;

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}