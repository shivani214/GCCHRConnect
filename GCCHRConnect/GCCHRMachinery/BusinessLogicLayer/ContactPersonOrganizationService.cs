using GCCHRMachinery.Entities;
using GCCHRMachinery.DataAccessLayer.MongoDb;
using System.Collections;

namespace GCCHRMachinery.BusinessLogicLayer
{
    public class  ContactPersonOrganizationService
    {
        ContactPersonOrganizationDB db = new ContactPersonOrganizationDB();

        public string CreateContact(ContactPersonOrganization contactToCreate)
        {
            string createdId;
            createdId = db.Create(contactToCreate);
            return createdId;
        }
        public IEnumerable GetAllRecords()
        {
            IEnumerable allRecords;
            allRecords = db.GetAll();
            return allRecords;
        }
        public ContactPersonOrganization GetRecordsById(string id)
        {
            ContactPersonOrganization c;
            c = db.GetById(id);
            return c;
        }
        public void DeleteRecordsById(string id)
        {
            db.DeleteById(id);
        }

    }
}
