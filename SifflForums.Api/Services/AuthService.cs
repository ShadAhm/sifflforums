﻿using AutoMapper;
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
        bool Login(LoginViewModel input, out TokenModel token);
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

        public bool Login(LoginViewModel input, out TokenModel token)
        {
            token = null; 
            User user = _dbContext.Users.Where(o => o.Username == input.Username).SingleOrDefault();

            byte[] salt = Convert.FromBase64String(user.Salt);
            string hash = HashPasswordIntoBase64(input.Password, salt);

            if(hash != user.Password)
            {
                // reject user
            }

            token = IssueToken(input.Username);

            return true; 
        }

        public TokenModel SignUp(SignUpViewModel user)
        {
            bool passwordDisallowed = _dbContext.BlacklistedPasswords.Any(o => o.Password == user.Password);

            if(passwordDisallowed)
            {
                // rejectuser
                return null; 
            }

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

            return IssueToken(userEntity.Username);
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
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
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