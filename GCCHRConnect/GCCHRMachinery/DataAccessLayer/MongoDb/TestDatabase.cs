using GCCHRMachinery.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    public class TestDatabase
    {
        public static void InsertToTestDatabase()
        {
            var TestDoc = new BsonDocument("SampleKey", 12);

            MongoClient client = new MongoClient();
            var database = client.GetDatabase("TestDB");            
            var collection = database.GetCollection<BsonDocument>("Contact");
            collection.InsertOne(TestDoc);
        }
    }
}
