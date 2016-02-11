using System.Collections.Generic;
using System.Linq;
using GCCHRMachinery.Entities;
using MongoDB.Driver;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    interface IMongoTask<TEntity> where TEntity : IMongoEntity
    {
        string Create(TEntity entity);

        TEntity GetById(string id);

        IEnumerable<TEntity> GetAll();
    }

    public abstract class MongoTask<TEntity>: IMongoTask<TEntity> where TEntity : IMongoEntity
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


        public string Create(TEntity document)
        {
            connector.Collection.InsertOne(document);
            return document.Id;
        }

        public IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> allRecords;
            var filterBuild = Builders<TEntity>.Filter;
            var filter = filterBuild.Empty;
            allRecords = connector.Collection.Find(filter).ToEnumerable();
            return allRecords;
        }

        public TEntity GetById(string id)
        {
            TEntity document;
            var filterBuild = Builders<TEntity>.Filter;
            var filter = filterBuild.Eq(c => c.Id, id);
            document = connector.Collection.Find(filter).Single();
            return document;
        }
    }

}
