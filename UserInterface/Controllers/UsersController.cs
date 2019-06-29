using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using OElite;
using PersonalCabinet.DAL.Services.Interfaces;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;
using PersonalCabinet.UserInterface.Services;

namespace PersonalCabinet.UserInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService userService; 

        public UsersController(IOptions<Settings> settings, IUserService userService)
        {
            this.userService = userService;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Contact>> GetAllUsers()
        {
            return userService.GetUserInternal();
        }

        // GET api/users
        [HttpGet("{entityId}")]
        public Task<Contact> GetUser(ObjectId entityId)
        {
            return userService.GetUserByIdInternal(entityId);
        }

        // POST api/users
        [HttpPost("adduser")]
        public void AddUser([FromBody]Contact value)
        {
            userService.AddUser(value);
        }

        // PUT api/users
        [HttpPut("{entityId}")]
        public void UpdateUser(ObjectId entityId, [FromBody]string value)
        {
            userService.UpdateUser(entityId, value);
        }

        // DELETE api/users
        [HttpDelete("{entityId}")]
        public void DeleteUser(ObjectId entityId)
        {
            userService.RemoveUser(entityId);
        }
    }
}
