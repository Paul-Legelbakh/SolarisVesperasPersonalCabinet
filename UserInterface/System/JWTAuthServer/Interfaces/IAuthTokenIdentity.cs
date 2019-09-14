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
        Task<ClaimsIdentity> LoginAsync(string userEmail, string userPassword);
        string CreateToken(ClaimsIdentity identity);
        Task<bool> RegistrationAsync(string userEmail, string userPassword);
    }
}
