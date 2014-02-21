using Arcnet.MongoDB.Framework.Attributes;
using Arcnet.MongoDB.Framework.Repository;
using MongoDB.Driver;

namespace Arcnet.MongoDB.Framework.Providers
{
    public class MongoCollectionProvider
    {
        public static MongoCollection<T> GetCollection<T>(MongoDatabase database) where T : TDocument
        {
            var collectionName = MongoCollectionNameAttribute.GetCollectionName<T>();
            return database.GetCollection<T>(collectionName);
        }
    }
}
