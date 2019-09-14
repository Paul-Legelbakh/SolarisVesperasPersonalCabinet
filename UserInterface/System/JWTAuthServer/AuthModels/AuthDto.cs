using System.ComponentModel.DataAnnotations;

namespace PersonalCabinet.UserInterface.System.JWTAuthServer
{
    public class AuthRegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class AuthLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
