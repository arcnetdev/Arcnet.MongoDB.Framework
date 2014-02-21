using System;
using System.IO;
using System.Linq.Expressions;
using Arcnet.MongoDB.Framework.Providers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;

namespace Arcnet.MongoDB.Framework
{   
    public class GridFS
    {
        private readonly MongoGridFS _mongoGridFS;

        public GridFS()
            : this(MongoDatabaseProvider.GetDatabase())
        {
        }

        public GridFS(string connectionStringName)
            : this(MongoDatabaseProvider.GetDatabase(connectionStringName))
        {
        }

        public GridFS(MongoDatabase database)
        {
            _mongoGridFS = database.GetGridFS(new MongoGridFSSettings());
        }

        public MongoGridFSFileInfo ImportFile(Stream stream, string fileName, MongoGridFSCreateOptions createOptions)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (createOptions == null) throw new ArgumentNullException("createOptions");
            if(createOptions.Id == null) throw new NullReferenceException("createOptions.Id");
            if (createOptions.UploadDate == null) throw new NullReferenceException("createOptions.UploadDate");
            if (createOptions.ContentType == null) throw new NullReferenceException("createOptions.ContentType");
            return _mongoGridFS.Upload(stream, fileName, createOptions);
        }

        public MongoGridFSFileInfo FindOneById(BsonValue id)
        {
            return _mongoGridFS.FindOneById(id);
        }

        [Obsolete("Not Tested")]
        public void Delete(Expression<Func<MongoGridFSFileInfo, bool>> expression)
        {
            _mongoGridFS.Delete(Query<MongoGridFSFileInfo>.Where(expression));
        }

        public void DeleteById(BsonValue id)
        {
            _mongoGridFS.DeleteById(id);
        }
    }
}
