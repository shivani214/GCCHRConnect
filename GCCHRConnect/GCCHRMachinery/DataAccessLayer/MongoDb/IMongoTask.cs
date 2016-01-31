using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCCHRMachinery.Entities;
using GCCHRMachinery.DataAccessLayer.MongoDb;
using MongoDB.Driver;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    interface IMongoTask<T> where T : IMongoEntity
    {
        string Create(T entity);

        T GetById(string id);
    }

    public abstract class MongoTask<T>: IMongoTask<T> where T : IMongoEntity
    {
        MongoConnector<T> connector;

        public MongoTask(string collectionName)
        {
            connector = new MongoConnector<T>(collectionName);
        }
        public string Create(T document)
        {
            connector.Collection.InsertOne(document);
            return document.Id;
        }

        public T GetById(string id)
        {
            T document;
            var filterBuild = Builders<T>.Filter;
            var filter = filterBuild.Eq(c => c.Id, id);
            document = connector.Collection.Find(filter).Single();
            return document;
        }
    }

}
