using System.Collections.Generic;
using System.Linq;
using GCCHRMachinery.Entities;
using MongoDB.Driver;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    /// <summary>
    /// All the basic and common database operations
    /// </summary>
    /// <typeparam name="TEntity">The type of entity</typeparam>
    interface IMongoTask<TEntity> where TEntity : IMongoEntity
    {
        /// <summary>
        /// Inserts a document in the database
        /// </summary>
        /// <param name="document">The document to insert</param>
        /// <returns>The id of the inserted document</returns>
        string Create(TEntity document);

        /// <summary>
        /// Searches and returns a document from the database based on the specified id
        /// </summary>
        /// <param name="id">The id to be searched</param>
        /// <returns>A single document</returns>
        TEntity GetById(string id);

        /// <summary>
        /// Returns all documents from a collection
        /// </summary>
        /// <returns>All documents</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Deletes a document
        /// </summary>
        /// <param name="id">The id of the document which is to be deleted</param>
        void DeleteById(string id);
    }

    public abstract class MongoTask<TEntity> : IMongoTask<TEntity> where TEntity : IMongoEntity
    {
        protected MongoConnector<TEntity> connector;

        /// <summary>
        /// To be obtained from TableOrCollectionName which is a const string field in each entity. If the field has not been provided for the entity, do it now!
        /// </summary>
        /// <param name="collectionNameForTask">Corresponding name of the entity in the table/collection.</param>
        public MongoTask(string collectionNameForTask)
        {
            connector = new MongoConnector<TEntity>(collectionNameForTask);
        }

        /// <summary>
        /// Inserts a document in the database
        /// </summary>
        /// <param name="document">The document to insert</param>
        /// <returns>The id of the inserted document</returns>
        public string Create(TEntity document)
        {
            connector.Collection.InsertOne(document);
            return document.Id;
        }

        /// <summary>
        /// Returns all documents from a collection
        /// </summary>
        /// <returns>All documents</returns>
        public IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> allRecords;
            var filterBuild = Builders<TEntity>.Filter;
            var filter = filterBuild.Empty;
            allRecords = connector.Collection.Find(filter).ToEnumerable();
            return allRecords;
        }

        /// <summary>
        /// Searches and returns a document from the database based on the specified id
        /// </summary>
        /// <param name="id">The id to be searched</param>
        /// <returns>A single document</returns>
        public TEntity GetById(string id)
        {
            TEntity document;
            var filterBuild = Builders<TEntity>.Filter;
            var filter = filterBuild.Eq(c => c.Id, id);
            document = connector.Collection.Find(filter).Single();
            return document;
        }

        /// <summary>
        /// Deletes a document
        /// </summary>
        /// <param name="id">The id of the document which is to be deleted</param>
        public void DeleteById(string id)
        {
            var filterBuild = Builders<TEntity>.Filter;
            var filter = filterBuild.Eq(c => c.Id, id);
            connector.Collection.DeleteOne(filter);
            
        }
    }

}
