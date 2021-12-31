using MongoDB.Bson.Serialization.Attributes;

namespace SagaCommon
{
    public abstract class Entity<TPrimaryKey>
    {
        [BsonId]
        public TPrimaryKey Id { get; set; }
    }
}
