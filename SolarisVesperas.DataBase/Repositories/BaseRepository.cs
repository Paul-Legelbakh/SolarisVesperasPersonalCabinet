using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalCabinet.DataBase.Repositories
{
    public abstract class BaseRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IMongoCollection<TEntity> _entity = null;
        private PersonalCabinetContext _context = null;

        public BaseRepository(IOptions<Settings> settings)
        {
            _context = new PersonalCabinetContext(settings);
            _entity = GetEntityCollection();
        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAllEntities()
        {
            return await _entity.Find(_ => true).ToListAsync();
        }

        public virtual async Task<TEntity> GetEntity(ObjectId entityId)
        {
            var filter = Builders<TEntity>.Filter.Eq("Entity_id", entityId);
            return await _entity.Find(filter)
                            .FirstOrDefaultAsync();
        }

        public virtual async Task AddEntity(TEntity entityItem)
        {
            await _entity.InsertOneAsync(entityItem);
        }

        public virtual async Task<DeleteResult> RemoveEntity(ObjectId entityId)
        {
            return await _entity.DeleteOneAsync(
                 Builders<TEntity>.Filter.Eq("Entity_id", entityId));
        }
        public abstract Task<UpdateResult> UpdateEntity(ObjectId entityId, TEntity entityItem);

        public virtual async Task<ReplaceOneResult> ReplaceEntity(ObjectId entityId, TEntity entityItem)
        {
            var filter = Builders<TEntity>.Filter.Eq("Entity_id", entityId);
            return await _entity.ReplaceOneAsync(filter, entityItem);
        }
        private IMongoCollection<TEntity> GetEntityCollection()
            => _context._database.GetCollection<TEntity>(typeof(TEntity).Name);
    }
}
