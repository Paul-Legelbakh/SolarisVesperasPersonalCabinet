using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalCabinet.UserInterface.System.JWTAuthServer;
using PersonalCabinet.DAL.Services.Interfaces;

namespace PersonalCabinet.UserInterface.Controllers.Authorization
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthTokenIdentity _tokenHandler;
        private readonly CookieOptions _options;

        public AuthController(IUserService service, IAuthTokenIdentity tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _options = new CookieOptions();
            _options.Expires = DateTime.UtcNow.AddDays(1);
            _options.HttpOnly = false;
            _options.Path = "/";
            _options.IsEssential = true;
            _userService = service;
        }

        [HttpPost, Route("signin")]
        public async Task Login([FromBody]AuthLoginDto req)
        {
            var identity = await _tokenHandler.LoginAsync(req.Email, req.Password);
            if (identity == null)
            {
                Response.StatusCode = 401;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }
            var token = _tokenHandler.CreateToken(identity);
            Response.Cookies.Append("Token", token, _options);
            Response.ContentType = "application/json";
        }

        [HttpPost, Route("signup")]
        public async Task Registration([FromBody]AuthRegisterDto req)
        {
            if (!await _tokenHandler.RegistrationAsync(req.Email, req.Password))
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("User already exists!");
                return;
            }
            await Response.WriteAsync("Registration is successed.");
        }
    }
}
