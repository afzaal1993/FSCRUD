using MongoDB.Bson;

namespace core.Entities
{
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}
