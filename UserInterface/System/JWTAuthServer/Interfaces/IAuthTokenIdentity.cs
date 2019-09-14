using PersonalCabinet.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCabinet.UserInterface.System.JWTAuthServer
{
    public interface IAuthTokenIdentity
    {
        Task<ClaimsIdentity> GetIdentityAsync(User userEntity);
        Task<ClaimsIdentity> GetIdentityAsync(string userEmail, string userPassword);
        string CreateToken(ClaimsIdentity identity);
        Task<bool> Registration(string userEmail, string userPassword);
    }
}
