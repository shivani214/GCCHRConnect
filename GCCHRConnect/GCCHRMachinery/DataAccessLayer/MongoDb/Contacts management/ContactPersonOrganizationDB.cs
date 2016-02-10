using GCCHRMachinery.Entities;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    public class ContactPersonOrganizationDB : MongoTask<ContactPersonOrganization>
    {
        //private static string col = ContactPersonOrganization.TableOrCollectionName;
        
        public ContactPersonOrganizationDB() : base(ContactPersonOrganization.TableOrCollectionName)
        {

        }
        
    }
}
