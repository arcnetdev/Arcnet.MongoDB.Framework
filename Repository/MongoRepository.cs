using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Arcnet.MongoDB.Framework.Exceptions;
using Arcnet.MongoDB.Framework.Providers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Arcnet.MongoDB.Framework.Repository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : TDocument
    {
        protected readonly MongoCollection<T> _collection;

        public MongoRepository(): this(MongoDatabaseProvider.GetDatabase())
        {
        }

        public MongoRepository(MongoDatabase database)
        {
            _collection = MongoCollectionProvider.GetCollection<T>(database);
        }

        public T Insert(T obj)
        {
            try
            {
                if (obj == null) throw new ArgumentNullException("obj");
                _collection.Insert(obj);
            }
            catch (WriteConcernException ex)
            {
                throw new MongoRepositoryException(ex.Message, ex);
            }
            return obj;
        }

        public IEnumerable<T> InsertBatch(IEnumerable<T> objects)
        {
            var insertBatch = objects as T[] ?? objects.ToArray();
            _collection.InsertBatch(insertBatch);
            return insertBatch;
        }

        public T Save(T obj)
        {
            try
            {
                if (obj == null) throw new ArgumentNullException("obj");
                _collection.Save(obj);
                return obj;
            }
            catch (WriteConcernException ex)
            {
                throw new MongoRepositoryException(ex.Message, ex);
            }
        }

        public IEnumerable<T> FindAll()
        {
            return _collection.FindAll();
        }

        public T FindOneById(object id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return _collection.FindOneById(BsonValue.Create(id));
        }

        public T FindOne(IMongoQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return _collection.FindOne(query);
        }

        public T FindOne(Expression<Func<T, bool>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return _collection.AsQueryable().Where(expression).FirstOrDefault();
        }

        public IEnumerable<T> Find(IMongoQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return _collection.Find(query);
        }

        public IEnumerable<T> Find(IMongoQuery query, int skip, int limit)
        {
            if (query == null) throw new ArgumentNullException("query");
            return _collection.Find(query).SetSkip(skip).SetLimit(limit);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return _collection.AsQueryable().Where(expression);
        }


        public IEnumerable<T> Find(Expression<Func<T, bool>> expression, int skip, int limit)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return _collection.AsQueryable().Where(expression).Skip(skip).Take(limit);
        }


        public T FindAndModifyById(object id, IMongoUpdate updateBuilder)
        {
            try
            {
                if (id == null) throw new ArgumentNullException("id");
                var query = Query<T>.EQ(x => x.Id, id);
                var result = _collection.FindAndModify(query, SortBy.Null, updateBuilder, true);
                return result.GetModifiedDocumentAs<T>();
            }
            catch (WriteConcernException ex)
            {
                throw new MongoRepositoryException(ex.Message, ex);
            }
        }

        public void Update(T obj)
        {
            try
            {
                _collection.Update(Query<T>.EQ(x => x.Id, obj.Id), Update<T>.Replace(obj));
            }
            catch (WriteConcernException ex)
            {
                throw new MongoRepositoryException(ex.Message, ex);
            }
        }

        public void Update(IMongoQuery query, IMongoUpdate updateBuilder)
        {
            try
            {
                _collection.Update(query, updateBuilder, UpdateFlags.Multi);
            }
            catch (WriteConcernException ex)
            {
                throw new MongoRepositoryException(ex.Message, ex);
            }
        }

        public void UpdateById(object id, IMongoUpdate updateBuilder)
        {
            try
            {
                _collection.Update(Query<T>.EQ(x => x.Id, id), updateBuilder);
            }
            catch (WriteConcernException ex)
            {
                throw new MongoRepositoryException(ex.Message, ex);
            }
        }

        public void Remove(IMongoQuery query)
        {
            var wCR = _collection.Remove(query);
            if (wCR.HasLastErrorMessage) throw new MongoRepositoryException(wCR.ErrorMessage);
        }

        public void RemoveById(object id)
        {
            var wCR = _collection.Remove(Query<T>.EQ(x => x.Id, id));
            if (wCR.HasLastErrorMessage) throw new MongoRepositoryException(wCR.ErrorMessage);
        }

        public long Count()
        {
            return _collection.Count();
        }

        public long Count(IMongoQuery query)
        {
            return _collection.Count(query);
        }

        public void EnsureIndex(IMongoIndexKeys keys)
        {
            _collection.EnsureIndex(keys);
        }

        public void EnsureIndex(IMongoIndexKeys keys, IMongoIndexOptions options)
        {
            _collection.EnsureIndex(keys, options);
        }

        public GetIndexesResult GetIndexes()
        {
            return _collection.GetIndexes();
        }

        public IList<TMember> Distinct<TMember>(string key)
        {
            var values = _collection.Distinct<TMember>(key);
            return values.ToList();
        } 


        //public T Add(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Add(IEnumerable<T> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public T Single(Expression<Func<T, bool>> criteria)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<T> All()
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<T> All(int page, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<T> All(Expression<Func<T, bool>> criteria)
        //{
        //    throw new NotImplementedException();
        //}

        //public MongoCursor<T> FindAs(IMongoQuery query)
        //{
        //    throw new NotImplementedException();
        //}

        //public T Update(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(IEnumerable<T> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Update(IMongoQuery query, IMongoUpdate update, UpdateFlags flags)
        //{
        //    throw new NotImplementedException();
        //}

        //public T FindAndModify(IMongoQuery query, IMongoSortBy sortBy, IMongoUpdate update, bool returnNew, bool upsert)
        //{
        //    throw new NotImplementedException();
        //}

        //public T FindAndRemove(IMongoQuery query, IMongoSortBy sortBy)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Delete(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public long Delete(IMongoQuery query)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveAll()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
