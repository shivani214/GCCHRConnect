using GCCHRMachinery.Entities;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    public class ContactDB : MongoTask<Contact>
    {
        //private static string col = ContactPersonOrganization.TableOrCollectionName;
        
        public ContactDB() : base(Contact.TableOrCollectionName)
        {

        }
        
    }
}
