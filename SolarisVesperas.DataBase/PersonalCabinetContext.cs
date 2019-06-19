using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalCabinet.DataBase
{
    public class PersonalCabinetContext
    {
        public readonly IMongoDatabase _database = null;

        public PersonalCabinetContext(IOptions<Settings> settings)
        {
            var connection = MongoClientSettings.FromUrl(MongoUrl.Create(settings.Value.ConnectionString));
            var client = new MongoClient(connection);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
            else
            {
                throw new MongoException("Unable to connect to Mongo database");
            }
        }

        public async Task<List<BsonDocument>> GetAllCollectionsNames()
        {
            List<BsonDocument> collection = new List<BsonDocument>();
            using (var collectionCursor = await _database.ListCollectionsAsync())
            {
                collection = await collectionCursor.ToListAsync();
            }
            return collection;
        }
    }
}
