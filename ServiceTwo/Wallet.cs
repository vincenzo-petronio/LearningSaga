using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SagaCommon;

namespace ServiceTwo
{
    public class Wallet : Entity<ObjectId>
    {
        [BsonElement("amount")]
        public long Amount { get; set; }
    }
}
