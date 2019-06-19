using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCabinet.DataBase
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAllEntities();
        Task AddEntity(TEntity entityItem);
        Task<TEntity> GetEntity(ObjectId entityId);
        Task<UpdateResult> UpdateEntity(ObjectId entityId, TEntity entityItem);
        Task<DeleteResult> RemoveEntity(ObjectId entityId);
        Task<ReplaceOneResult> ReplaceEntity(ObjectId entityId, TEntity entityBody);
    }
}
