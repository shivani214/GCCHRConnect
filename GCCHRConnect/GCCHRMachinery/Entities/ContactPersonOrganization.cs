using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalEntities;
using MongoDB.Bson.Serialization.Attributes;

namespace GCCHRMachinery.Entities
{
    public class ContactPersonOrganization : Contact, IMaintainInMongoDb
    {
        #region Fields
        public const string TableOrCollectionName = "Contacts";
        #endregion

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public List<string> Tags { get; set; }

        #region Constructors and Methods
        public override string ToString()
        {
            StringBuilder contact = new StringBuilder();
            contact.Append("Name: ");
            contact.AppendLine(this.Name.Title + this.Name.First + this.Name.Middle + this.Name.Last);
            return contact.ToString();
        }
        #endregion
    }

}
