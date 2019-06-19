using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PersonalCabinet.DataBase
{
    public interface IEntity
    {
        [BsonId]
        ObjectId Entity_id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
