using MinimalApi_MongoDb.Domains;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MinimalApi_MongoDb.ViewModel
{
    public class OrderViewModel
    {
        [BsonIgnore]
        [JsonIgnore]
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("date")]
        public DateOnly? Date { get; set; }

        [BsonElement("productId")]
        public List<string>? ProductId { get; set; }

        [BsonElement("clientId")]
        public string? ClientId { get; set; }

        
    }
}
