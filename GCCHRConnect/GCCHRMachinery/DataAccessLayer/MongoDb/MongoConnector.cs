using GCCHRMachinery.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
   public class MongoConnector<T> where T :IMongoEntity
    {
        public IMongoCollection<T> Collection { get; }
        public MongoConnector(string collectionName)
        {
            const string connectionString = "";
            MongoClient client = new MongoClient();
            const string databaseName = "GCCHRConnectDB";
            IMongoDatabase database = client.GetDatabase(databaseName);
            Collection = database.GetCollection<T>(collectionName);
        }

    }
}
