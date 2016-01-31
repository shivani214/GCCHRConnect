using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
namespace GCCHRMachinery.Entities
{
    /// <summary>
    /// For all entities which are to be maintained in a database. Only the root entity must implement this 
    /// </summary>
    public interface IMongoEntity
    {
        string Id { get; set; }
        string CollectionName { get; }
    }
}
