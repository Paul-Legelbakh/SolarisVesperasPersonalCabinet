using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using OElite;
using PersonalCabinet.DAL.Services.Interfaces;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;

namespace PersonalCabinet.UserInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService; 

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<User>> GetAllUsers()
        {
            var allUsers = _userService.GetUserInternal();
            return allUsers;
        }

        // GET api/users
        [HttpGet("{entityId}")]
        public Task<User> GetUser(ObjectId entityId)
        {
            return _userService.GetUserByIdInternal(entityId);
        }

        // POST api/users
        [HttpPost("adduser")]
        public void AddUser([FromBody]User value)
        {
            _userService.AddUser(value);
        }

        // PUT api/users
        [HttpPut("{entityId}")]
        public void UpdateUser(ObjectId entityId, [FromBody]User value)
        {
            _userService.UpdateUser(entityId, value);
        }

        // DELETE api/users
        [HttpDelete("{entityId}")]
        public void DeleteUser(ObjectId entityId)
        {
            _userService.RemoveUser(entityId);
        }
    }
}
