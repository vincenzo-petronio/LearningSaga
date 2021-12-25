using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceOne.Data
{
    public abstract class Entity<TPrimaryKey>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public TPrimaryKey Id { get; set; }
    }
}
