using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;

namespace Arcnet.MongoDB.Framework.Repository
{
    public abstract class TDocument
    {
        [BsonIgnore]
        public abstract object Id { get; set; }
    }
}
