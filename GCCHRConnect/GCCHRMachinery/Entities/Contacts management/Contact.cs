using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalEntities;
using MongoDB.Bson.Serialization.Attributes;

namespace GCCHRMachinery.Entities
{
    public class Contact : ContactPersonOrganization, IMongoEntity
    {
        #region Fields
        public const string TableOrCollectionName = "Contacts";
        #endregion

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public HashSet<string> Tags { get; set; }

        #region Constructors and Methods
        public override string ToString()
        {
            StringBuilder contact = new StringBuilder();
            int i = 1;
            contact.AppendFormat("Name: {0} {1} {2} {3}", Name.Title, Name.First, Name.Middle, Name.Last);
            contact.AppendLine();
            if (Tags != null)
            {
                contact.AppendLine(" Tags:");
                foreach (string tag in Tags)
                {
                    contact.AppendFormat("{0}. {1}", i++, tag);
                    contact.AppendLine();
                }
            }
            contact.AppendLine("*****************");
            return contact.ToString();
        }
        #endregion
    }
}
