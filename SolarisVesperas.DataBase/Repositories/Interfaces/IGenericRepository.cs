using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCabinet.DataBase
{
    public interface IGenericRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
        Task AddEntityAsync(TEntity entityItem);
        Task<TEntity> GetEntityAsync(ObjectId entityId);
        Task<TEntity> GetEntityAsync(Dictionary<string, string> arguments);
        Task<UpdateResult> UpdateEntityAsync(ObjectId entityId, TEntity entityItem);
        Task<DeleteResult> RemoveEntityAsync(ObjectId entityId);
        Task<ReplaceOneResult> ReplaceEntityAsync(ObjectId entityId, TEntity entityBody);
    }
}
