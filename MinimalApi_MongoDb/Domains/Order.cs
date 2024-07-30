using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MinimalApi_MongoDb.Domains
{
    public class Order
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("date")]
        public DateOnly? Date { get; set; }

        [BsonElement("productId")]
        public List<string>? ProductId { get; set; }
        public List<Product>? Products { get; set; }

        [JsonIgnore]
        [BsonElement("clientId")]
        public string? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
