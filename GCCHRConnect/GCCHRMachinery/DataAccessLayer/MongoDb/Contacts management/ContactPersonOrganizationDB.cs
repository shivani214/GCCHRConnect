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
    public class ContactPersonOrganizationDB : MongoTask<ContactPersonOrganization>
    {
        public string CollectionName { set; }

        public ContactPersonOrganizationDB(string collectionNameForDB) : base(collectionNameForDB)
        {
            
        }

        public static string CreateContact(ContactPersonOrganization newContact)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("GCCHRConnectDB");
            IMongoCollection<ContactPersonOrganization> collection = database.GetCollection<ContactPersonOrganization>(ContactPersonOrganization.TableOrCollectionName);
            collection.InsertOne(newContact);
            return newContact.Id;
        }

        public static ContactPersonOrganization GetContact(string id)
        {
            ContactPersonOrganization contactToRetrieve;
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("GCCHRConnectDB");
            IMongoCollection<ContactPersonOrganization> collection = database.GetCollection<ContactPersonOrganization>(ContactPersonOrganization.TableOrCollectionName);
            var filterBuild = Builders<ContactPersonOrganization>.Filter;
            var filter = filterBuild.Eq(c => c.Id, id);
            contactToRetrieve = collection.Find(filter).Single();
            return contactToRetrieve;
        }

        public static List<ContactPersonOrganization> GetAllContacts()
        {
            List<ContactPersonOrganization> allContacts;
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("GCCHRConnectDB");
            IMongoCollection<ContactPersonOrganization> collection = database.GetCollection<ContactPersonOrganization>(ContactPersonOrganization.TableOrCollectionName);
            var filterBuild = Builders<ContactPersonOrganization>.Filter;
            var filter = filterBuild.Empty;
            allContacts = collection.Find(filter).ToList();
            return allContacts;
        }
    }
}
