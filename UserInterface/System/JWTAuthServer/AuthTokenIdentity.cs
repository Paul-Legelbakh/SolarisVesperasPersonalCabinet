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

        public AuthTokenIdentity(IOptions<Settings> settings)
        {
            uof = new UnitOfWork(settings);
        }

        public async virtual Task<ClaimsIdentity> LoginAsync(string userEmail, string userPassword)
        {
            var existUser = await GetUserDataAsync(userEmail, userPassword);
            if (existUser == null)
                return null;

            return GetUserClaim(existUser);
        }

        public async virtual Task<bool> RegistrationAsync(string userEmail, string userPassword)
        {
            var existUser = await GetUserDataAsync(userEmail);
            if (existUser != null)
                return false;
            
            var newUser = new User
            {
                Email = userEmail,
                Password = userPassword,
                Role = "SimpleUser"
            };

            await uof.Users.AddEntityAsync(newUser);
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
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetUserClaim(User userEntity)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userEntity.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userEntity.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AccessToken",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private async Task<User> GetUserDataAsync(string userEmail)
        {
            return await uof.Users.GetEntityAsync(new Dictionary<string, string> {
                { "Email", userEmail }
            });
        }
        private async Task<User> GetUserDataAsync(string userEmail, string userPassword)
        {
            return await uof.Users.GetEntityAsync(new Dictionary<string, string> {
                { "Email", userEmail },
                { "Password", userPassword }
            });
        }
    }
}
