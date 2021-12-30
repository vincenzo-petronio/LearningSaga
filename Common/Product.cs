using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SagaCommon
{
    public class Product : Entity<ObjectId>
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("qty")]
        public long Quantity { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }
    }
}
