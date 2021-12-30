using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SagaCommon
{
    public class Wallet : Entity<ObjectId>
    {
        [BsonElement("amount")]
        public long Amount { get; set; }
    }
}
