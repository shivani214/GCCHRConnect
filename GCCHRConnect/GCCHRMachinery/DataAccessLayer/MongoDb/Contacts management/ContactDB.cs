using GCCHRMachinery.Entities;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    /// <summary>
    /// The datalayer for <see cref="Contact"/>
    /// </summary>
    public class ContactDB : MongoTask<Contact>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ContactDB() : base(Contact.TableOrCollectionName)
        {

        }
        
    }
}
