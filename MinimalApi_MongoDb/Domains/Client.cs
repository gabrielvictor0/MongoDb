using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Globalization;
using System.Text.Json.Serialization;

namespace MinimalApi_MongoDb.Domains
{
    public class Client
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("cpf")]
        public string? Cpf { get; set; }

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("address")]
        public string? Address { get; set; }

        [BsonElement("userId")]
        public string? IdUser { get; set; }
    }
}
