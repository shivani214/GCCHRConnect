using GCCHRMachinery.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    /// <summary>
    /// Connects to the MongoDb database.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which the connector is to be used.</typeparam>
   public class MongoConnector<TEntity> where TEntity : IMongoEntity
    {
        /// <summary>
        /// The collection in MongoDb for the specified entity
        /// </summary>
        public IMongoCollection<TEntity> Collection { get; }

        /// <summary>
        /// The constructor which connects to the database.
        /// </summary>
        /// <param name="collectionNameForConnector">The collection name in database which corresponds to the specified entity</param>
        public MongoConnector(string collectionNameForConnector)
        {
            if (string.IsNullOrWhiteSpace(collectionNameForConnector))
            {
                throw new ArgumentNullException("collectionNameForConnector", "The collection name cannot be null");
            }
            const string connectionString = "";
            MongoClient client = new MongoClient();
            const string databaseName = "GCCHRConnectDB"; // should be a private class constant
            IMongoDatabase database = client.GetDatabase(databaseName);
            Collection = database.GetCollection<TEntity>(collectionNameForConnector);
        }

    }
}
