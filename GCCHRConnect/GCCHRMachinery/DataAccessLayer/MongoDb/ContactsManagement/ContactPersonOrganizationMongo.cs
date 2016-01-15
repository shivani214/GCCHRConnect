using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCCHRMachinery.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    public static class ContactPersonOrganizationDB
    {
        public static string CreateContact(ContactPersonOrganization newContact)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("GCCHRConnectDB");
            IMongoCollection<ContactPersonOrganization> collection = database.GetCollection<ContactPersonOrganization>(ContactPersonOrganization.TableOrCollectionName);
            collection.InsertOne(newContact);

            IMongoDatabase database1 = client.GetDatabase("TestDB");
            IMongoCollection<BsonDocument> collection1 = database1.GetCollection<BsonDocument>(ContactPersonOrganization.TableOrCollectionName);
            BsonDocument TestDoc = new BsonDocument("SampleKey", 12);
            collection1.InsertOne(TestDoc);
            return newContact.Id;
        }
    }
}
