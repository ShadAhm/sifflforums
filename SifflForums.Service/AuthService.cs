using AutoMapper;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using SifflForums.Data;
using SifflForums.Data.Entities;
using SifflForums.Models;
using SifflForums.Models.Dto;
using SifflForums.Service.Validators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SifflForums.Service
{
    public interface IAuthService
    {
        RequestResult<TokenModel> SignUp(SignUpModel user);
        RequestResult<TokenModel> Login(LoginModel input);
        IAuthService SetServiceApiKey(string key);
    }

    public class AuthService : IAuthService
    {
        SifflContext _dbContext;
        IMapper _mapper;
        string _serviceApiKey;

        public AuthService(SifflContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public IAuthService SetServiceApiKey(string key)
        {
            this._serviceApiKey = key;
            return this;
        }

        public RequestResult<TokenModel> Login(LoginModel input)
        {
            if (string.IsNullOrWhiteSpace(this._serviceApiKey))
                throw new Exception("Expected API key");

            User user = _dbContext.Users.Where(o => o.Username == input.Username).SingleOrDefault();

            byte[] salt = Convert.FromBase64String(user.Salt);
            string hash = HashPasswordIntoBase64(input.Password, salt);

            if (hash != user.Password)
            {
                return RequestResult<TokenModel>.Fail("Incorrect password");
            }

            TokenModel token = IssueToken(input.Username);

            return RequestResult<TokenModel>.Success(token);
        }

        public RequestResult<TokenModel> SignUp(SignUpModel user)
        {
            if (string.IsNullOrWhiteSpace(this._serviceApiKey))
                throw new Exception("Expected API key");

            var result = new SignUpValidator(this._dbContext).Validate(user);

            if (!result.IsValid)
                return RequestResult<TokenModel>.Fail(result.ToString(","));

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            User userEntity = new User();
            userEntity.Username = user.Username;
            userEntity.Email = user.Email;
            userEntity.Password = HashPasswordIntoBase64(user.Password, salt);
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.RegisteredAtUtc = DateTime.UtcNow;
            userEntity.LastPasswordResetUtc = DateTime.UtcNow;

            _dbContext.Add(userEntity);
            _dbContext.SaveChanges();

            var token = IssueToken(userEntity.Username);

            return RequestResult<TokenModel>.Success(token);
        }

        public string HashPasswordIntoBase64(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            return Convert.ToBase64String(hash);
        }

        public bool ValidateToken(TokenModel token)
        {
            return true;
        }

        private TokenModel IssueToken(string username)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_serviceApiKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "user")
            };

            var tokeOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return new TokenModel { Token = tokenString };
        }
    }
}
