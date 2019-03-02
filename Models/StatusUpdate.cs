using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KitchenStatusServer.Models
{
    public class StatusUpdate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("user")]
        public string User { get; set; }

        [BsonElement("changes")]
        public StatusChange[] Changes { get; set; }
    }

    public class StatusChange
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("quantity")]
        public double Quantity { get; set; }
    }
}
