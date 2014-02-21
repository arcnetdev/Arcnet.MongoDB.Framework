using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Arcnet.MongoDB.Framework.Attributes
{
    public class MongoCollectionNameAttribute : Attribute
    {
        public string collectionName;

        public MongoCollectionNameAttribute(string collectionName)
        {
            if (String.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException("collectionName");
            this.collectionName = collectionName;
        }

        public static string GetCollectionName<T>()
        {
            var attribute = GetCustomAttribute<T>(typeof(MongoCollectionNameAttribute));
            if (attribute == null)
                throw new NullReferenceException("MongoCollectionName Attribute not specified.");
            return ((MongoCollectionNameAttribute)attribute).collectionName;
        }

        public static string GetCollectionName(MemberInfo memberInfo)
        {
            var attribute = GetCustomAttribute(typeof(MongoCollectionNameAttribute), memberInfo);
            if (attribute == null)
                throw new NullReferenceException("MongoCollectionName Attribute not specified");
            return ((MongoCollectionNameAttribute)attribute).collectionName;
        }

        private static Attribute GetCustomAttribute<T>(Type attribute)
        {
            if (attribute == null) throw new ArgumentNullException("attribute");
            return Attribute.GetCustomAttribute(typeof(T), attribute);
        }

        private static Attribute GetCustomAttribute(Type attribute, MemberInfo memberInfo)
        {
            if (attribute == null) throw new ArgumentNullException("attribute");
            return GetCustomAttribute(memberInfo, attribute);
        }
    }
}
