using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using OElite;
using PersonalCabinet.DAL;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;

namespace PersonalCabinet.UserInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UnitOfWork uof;

        public UsersController(IOptions<Settings> settings)
        {
            uof = new UnitOfWork(settings);
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Contact>> GetAllUsers()
        {
            return GetUserInternal();
        }

        private async Task<IEnumerable<Contact>> GetUserInternal()
        {
            return await uof.Users.GetAllEntities();
        }

        // GET api/users/5
        [HttpGet("{entityId}")]
        public Task<Contact> GetUser(ObjectId entityId)
        {
            return GetUserByIdInternal(entityId);
        }

        private async Task<Contact> GetUserByIdInternal(ObjectId entityId)
        {
            return await uof.Users.GetEntity(entityId) ?? new Contact();
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody]string value)
        {
            var body = JsonConvert.DeserializeObject<Contact>(value);
            uof.Users.AddEntity(new Contact()
                {
                    Email = body.Email,
                    Name = body.Name,
                    Password = body.Password
            });
        }

        // PUT api/users/5
        [HttpPut("{entityId}")]
        public void UpdateEntity(ObjectId entityId, [FromBody]string value)
        {
            var body = JsonConvert.DeserializeObject<Contact>(value);
            uof.Users.UpdateEntity(entityId, body);
        }

        // DELETE api/users/23243423
        [HttpDelete("{entityId}")]
        public void Delete(ObjectId entityId)
        {
            uof.Users.RemoveEntity(entityId);
        }
    }
}
