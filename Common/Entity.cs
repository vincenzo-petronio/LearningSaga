using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SagaCommon
{
    public abstract class Entity<TPrimaryKey>
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public TPrimaryKey Id { get; set; }
    }
}
