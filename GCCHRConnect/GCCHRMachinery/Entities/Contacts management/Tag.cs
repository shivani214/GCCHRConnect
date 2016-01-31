using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.Entities
{
    public class Tag: IMongoEntity
    {
        #region Fields
        public const string TableOrCollectionName = "Tag";
        #endregion

        public string TagName { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string CollectionName
        {
            get
            {
                return "Tags";
            }
        }

        #region Constructors and methods
        public override string ToString()
        {
            return string.Format("Id: {0}\tTag name: {1}", Id, TagName);
        }
        #endregion
    }
}
