using GCCHRMachinery.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    public static class TagDB
    {
        public static string CreateTag(Entities.Tag tag)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("GCCHRConnectDB");
            IMongoCollection<Entities.Tag> collection = database.GetCollection<Entities.Tag>(Entities.Tag.TableOrCollectionName);
            collection.InsertOne(tag);
            return tag.Id;
        }
    }
}
