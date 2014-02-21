using System;
using System.Configuration;
using MongoDB.Driver;

namespace Arcnet.MongoDB.Framework.Providers
{
    public class MongoDatabaseProvider
    {
        public static MongoDatabase GetDatabase(string connectionStringName = "MongoServerSettings")
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings == null) throw new NullReferenceException("A connectionString não existe.");
            var url = new MongoUrl(connectionStringSettings.ConnectionString);
            var server = new MongoClient(url).GetServer();
            return server.GetDatabase(url.DatabaseName);
        }
    }
}
