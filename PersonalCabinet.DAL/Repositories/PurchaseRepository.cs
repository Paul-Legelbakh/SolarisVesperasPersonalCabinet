using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;
using PersonalCabinet.DataBase.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCabinet.DAL.Repositories
{
    public class PurchaseRepository : BaseRepository<Purchase>
    {
        public PurchaseRepository(IOptions<Settings> settings) : base(settings) { }

        public override async Task<UpdateResult> UpdateEntity(ObjectId entityId, Purchase entityItem)
        {
            var filter = Builders<Purchase>.Filter.Eq(ent => ent.Entity_id, entityId);
            var update = Builders<Purchase>.Update
                            .CurrentDate(ent => ent.ModifiedOn);

            return await _entity.UpdateOneAsync(filter, update);
        }
    }
}
