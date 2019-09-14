using Microsoft.Extensions.Options;
using MongoDB.Bson;
using PersonalCabinet.DAL.Services.Interfaces;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCabinet.DAL.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork uof;

        public UserService(IOptions<Settings> settings)
        {
            uof = new UnitOfWork(settings);
        }

        public async Task<IEnumerable<User>> GetUserInternal()
        {
            return await uof.Users.GetAllEntities();
        }

        public async Task<User> GetUserByIdInternal(ObjectId entityId)
        {
            return await uof.Users.GetEntity(entityId) ?? new User();
        }

        public void AddUser(User entityItem)
        {
            uof.Users.AddEntity(entityItem);
        }

        public void UpdateUser(ObjectId entityId, User value)
        {
            uof.Users.UpdateEntity(entityId, value);
        }

        public void RemoveUser(ObjectId entityId)
        {
            uof.Users.RemoveEntity(entityId);
        }
    }
}
