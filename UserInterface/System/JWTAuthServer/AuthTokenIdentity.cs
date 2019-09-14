using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonalCabinet.DAL;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;
using PersonalCabinet.DAL.Services.Interfaces;

namespace PersonalCabinet.UserInterface.System.JWTAuthServer
{
    public class AuthTokenIdentity : IAuthTokenIdentity
    {
        private readonly UnitOfWork uof;
        private readonly IUserService _userService;

        public AuthTokenIdentity(IOptions<Settings> settings, IUserService service)
        {
            uof = new UnitOfWork(settings);
            _userService = service;
        }

        public async virtual Task<ClaimsIdentity> GetIdentityAsync(User userEntity)
        {
            var existUser = await GetUserDataAsync(userEntity.Email, userEntity.Password);
            if (existUser == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, existUser.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, existUser.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        public async virtual Task<ClaimsIdentity> GetIdentityAsync(string userEmail, string userPassword)
        {
            var existUser = await GetUserDataAsync(userEmail, userPassword);
            if (existUser == null)
                return null;

            return GetUserClaim(existUser);
        }

        public async virtual Task<bool> Registration(string userEmail, string userPassword)
        {
            var existUser = await GetUserDataAsync(userEmail, userPassword);
            if (existUser != null)
                return false;
            
            var newUser = new User();
            newUser.Email = userEmail;
            newUser.Password = userPassword;
            newUser.Role = "SimpleUser";
            await uof.Users.AddEntity(newUser);
            return true;
        }

        public virtual string CreateToken(ClaimsIdentity identity)
        {
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    notBefore: DateTime.UtcNow,
                    audience: AuthOptions.AUDIENCE,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetUserClaim(User userEntity)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userEntity.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userEntity.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private async Task<User> GetUserDataAsync(string userEmail, string userPassword)
        {
            return await uof.Users.GetEntity(new Dictionary<string, string> {
                { "Email", userEmail },
                { "Password", userPassword }
            });
        }
    }
}
