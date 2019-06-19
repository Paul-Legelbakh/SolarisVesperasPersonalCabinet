using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalCabinet.DataBase;
using PersonalCabinet.DataBase.Models;
using PersonalCabinet.DataBase.Repositories;
using System.Threading.Tasks;

namespace PersonalCabinet.DAL.Repositories
{
    public class UserRepository : BaseRepository<Contact>
    {
        public UserRepository(IOptions<Settings> settings) : base(settings) { }

        public override async Task<UpdateResult> UpdateEntity(ObjectId entityId, Contact entityItem)
        {
            var filter = Builders<Contact>.Filter.Eq(ent => ent.Entity_id, entityId);
            var update = Builders<Contact>.Update
                            .CurrentDate(ent => ent.ModifiedOn)
                            .Set(ent => ent.Name, entityItem.Name)
                            .Set(ent => ent.SecondName, entityItem.SecondName)
                            .Set(ent => ent.Address, entityItem.Address)
                            .Set(ent => ent.BirthDate, entityItem.BirthDate)
                            .Set(ent => ent.MobilePhone, entityItem.MobilePhone)
                            .Set(ent => ent.Description, entityItem.Description)
                            .Set(ent => ent.Purchases, entityItem.Purchases);

            return await _entity.UpdateOneAsync(filter, update);
        }
    }
}
