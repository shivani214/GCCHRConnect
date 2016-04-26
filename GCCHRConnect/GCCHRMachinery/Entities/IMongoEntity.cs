using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
namespace GCCHRMachinery.Entities
{
    /// <summary>
    /// For all entities which are to be maintained in a database. Only the root entity must implement this.
    /// <para>Each entity must possess a <c>const string TableOrCollectionName</c> whose value should be a name by which the entity must be stored in database.</para>
    /// </summary>
    public interface IMongoEntity
    {
        /// <summary>
        /// Unique id for a document
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Every entity must trim all it's properties of type string and string collections here
        /// </summary>
        void TrimAllStrings();
    }
}
