using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.Entities
{
    /// <summary>
    /// Represents a single Tag which may be assigned to multiple <see cref="Contact"/>
    /// </summary>
    public class Tag : IMongoEntity
    {
        #region Fields
        private string tagName;
        public const string TableOrCollectionName = "Tag";

        #endregion

        /// <summary>
        /// The name of the Tag. Try to avoid acronyms.
        /// </summary>
        public string TagName
        {
            get
            {
                return tagName;
            }
            set
            {
                tagName = value.Trim();
            }
        }
        /// <summary>
        /// The unique id of the <see cref="Tag"/>
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        #region Constructors and methods
        public override string ToString()
        {
            return string.Format("Id: {0}\tTag name: {1}", Id, TagName);
        }

        public void TrimAllStringCollections()
        {

        }
        #endregion
    }
}
