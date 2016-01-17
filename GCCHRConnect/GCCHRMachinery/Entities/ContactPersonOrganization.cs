using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalEntities;

namespace GCCHRMachinery.Entities
{
    public class ContactPersonOrganization : Contact, IMaintainInMongoDb
    {
        #region Fields
        public const string TableOrCollectionName = "Contacts";
        #endregion

        public string Id { get; set; }

        public List<string> Tags { get; set; }

        #region Constructors and Methods
        
        #endregion
    }

}
