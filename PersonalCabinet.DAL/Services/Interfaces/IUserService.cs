using MongoDB.Bson;
using PersonalCabinet.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCabinet.DAL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUserInternal();
        Task<User> GetUserByIdInternal(ObjectId entityId);
        void AddUser(User entityItem);
        void UpdateUser(ObjectId entityId, User value);
        void RemoveUser(ObjectId entityId);
    }
}
