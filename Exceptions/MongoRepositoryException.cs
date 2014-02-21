using System;

namespace Arcnet.MongoDB.Framework.Exceptions
{
    public class MongoRepositoryException : Exception
    {
        public MongoRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MongoRepositoryException(string message) : base(message)
        {
        }
    }
}
