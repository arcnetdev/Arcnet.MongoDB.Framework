﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Arcnet.MongoDB.Framework.Repository
{
    public interface IMongoRepository<T>
    {
        T Insert(T obj);
        IEnumerable<T> InsertBatch(IEnumerable<T> objects);
        T Save(T obj);
        IEnumerable<T> FindAll();
        T FindOneById(object id);
        T FindOne(IMongoQuery query);
        T FindOne(Expression<Func<T, bool>> expression);
        IEnumerable<T> Find(IMongoQuery query);
        IEnumerable<T> Find(IMongoQuery query, int skip, int limit);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression, int skip, int limit);
        void Update(T obj);
        void Update(IMongoQuery query, IMongoUpdate updateBuilder);
        void UpdateById(object id, IMongoUpdate updateBuilder);
        T FindAndModifyById(object id, IMongoUpdate updateBuilder);
        void RemoveById(object id);
        void Remove(IMongoQuery query);
        long Count();
        long Count(IMongoQuery query);
        void EnsureIndex(IMongoIndexKeys keys);
        void EnsureIndex(IMongoIndexKeys keys, IMongoIndexOptions options);
        GetIndexesResult GetIndexes();
        IList<TMember> Distinct<TMember>(string key);

        ///// <summary>
        ///// Adds the new entity in the repository.
        ///// </summary>
        ///// <param name="entity">The entity to add.</param>
        ///// <returns>The added entity including its new ObjectId.</returns>
        ///// <exception cref="AlreadyExistsException">Incase duplicate key is inserted</exception>
        //T Add(T entity);

        ///// <summary>
        ///// Adds the new entities in the repository.
        ///// </summary>
        ///// <param name="entities">The entities of type T.</param>
        //void Add(IEnumerable<T> entities);

        ///// <summary>
        ///// Returns a single T by the given criteria.
        ///// </summary>
        ///// <param name="criteria">The expression.</param>
        ///// <returns>A single T matching the criteria.</returns>
        //T Single(Expression<Func<T, bool>> criteria);

        ///// <summary>
        ///// Returns All the records of T.
        ///// </summary>
        ///// <returns>IQueryable of T.</returns>
        //IQueryable<T> All();

        ///// <summary>
        ///// Returns All the records of T for given page specification.
        ///// </summary>
        ///// <param name="page">The page number.</param>
        ///// <param name="pageSize">Size of the page.</param>
        ///// <returns>IQueryable of T.</returns>
        //IQueryable<T> All(int page, int pageSize);

        ///// <summary>
        ///// Returns the list of T where it matches the criteria.
        ///// </summary>
        ///// <param name="criteria">The expression.</param>
        ///// <returns>IQueryable of T.</returns>
        //IQueryable<T> All(Expression<Func<T, bool>> criteria);

        ///// <summary>
        ///// Finds all the documents based on specified query.
        ///// </summary>
        ///// <typeparam name="T">The type of the document</typeparam>
        ///// <param name="query">The query.</param>
        ///// <returns></returns>
        //MongoCursor<T> FindAs(IMongoQuery query);

        ///// <summary>
        ///// Upserts an entity.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        ///// <returns>The updated entity.</returns>
        //T Update(T entity);

        ///// <summary>
        ///// Upserts the entities.
        ///// </summary>
        ///// <param name="entities">The entities to update.</param>
        //void Update(IEnumerable<T> entities);

        ///// <summary>
        ///// Updates the document specified by query.
        ///// </summary>
        ///// <param name="query">The query.</param>
        ///// <param name="update">The update.</param>
        ///// <param name="flags">The flags.</param>
        ///// <returns>True if any documented was affected, false otherwise</returns>
        //bool Update(IMongoQuery query, IMongoUpdate update, UpdateFlags flags);

        ///// <summary>
        ///// Finds and modifies one document atomically.
        ///// </summary>
        ///// <param name="query">The query.</param>
        ///// <param name="sortBy">The sort by.</param>
        ///// <param name="update">The update.</param>
        ///// <param name="returnNew">if set to <c>true</c> [return new].</param>
        ///// <param name="upsert">if set to <c>true</c> [upsert].</param>
        ///// <returns></returns>
        //T FindAndModify(IMongoQuery query, IMongoSortBy sortBy, IMongoUpdate update, bool returnNew, bool upsert);

        ///// <summary>
        ///// Finds and removes one document atomically.
        ///// </summary>
        ///// <param name="query">The query.</param>
        ///// <param name="sortBy">The sort by.</param>
        ///// <returns></returns>
        //T FindAndRemove(IMongoQuery query, IMongoSortBy sortBy);

        ///// <summary>
        ///// Deletes the given entity.
        ///// </summary>
        ///// <param name="entity">The entity to delete.</param>
        //bool Delete(T entity);

        ///// <summary>
        ///// Deletes all documents that match the query.
        ///// </summary>
        ///// <param name="query">The query.</param>
        ///// <returns>The number of documents deleted.</returns>
        //long Delete(IMongoQuery query);

        ///// <summary>
        ///// Removes all the records from the collection.
        ///// </summary>
        //void RemoveAll();
    }
}
