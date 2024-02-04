using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Models
{
    public class Product
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
