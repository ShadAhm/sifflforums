using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SifflForums.Api.Models.Auth;
using SifflForums.Data;
using SifflForums.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SifflForums.Api.Services
{
    public interface IAuthService
    {
        TokenModel SignUp(SignUpViewModel user);
    }

    public class AuthService : IAuthService
    {
        SifflContext _dbContext;
        IMapper _mapper; 

        public AuthService(SifflContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public TokenModel SignUp(SignUpViewModel user)
        {
            bool passwordDisallowed = _dbContext.BlacklistedPasswords.Any(o => o.Password == user.Password);

            if(passwordDisallowed)
            {
                // rejectuser
            }

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            User userEntity = new User();
            userEntity.DisplayName = user.Username;
            userEntity.Email = user.Email;
            userEntity.Password = Convert.ToBase64String(hash);
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.RegisteredAtUtc = DateTime.UtcNow;
            userEntity.LastPasswordResetUtc = DateTime.UtcNow;

            _dbContext.Add(userEntity);
            _dbContext.SaveChanges();

            return IssueToken();
        }

        private TokenModel IssueToken()
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
            return new TokenModel { Token = tokenString };
        }
    }
}
