using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalCabinet.DataBase.Models
{
    public class BaseEntity : IEntity
    {
        [BsonId]
        public ObjectId Entity_id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
