using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PersonalCabinet.UserInterface.System.JWTAuthServer
{
    public class AuthOptions
    {
        const string ENCRYPTION_KEY = "SolarisVesperasCns3250877KEY";
        public const int LIFETIME = 1435;
        public const string ISSUER = "ConsimpleAuthServer";
        public const string AUDIENCE = "Supervisor";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ENCRYPTION_KEY));
    }
}
