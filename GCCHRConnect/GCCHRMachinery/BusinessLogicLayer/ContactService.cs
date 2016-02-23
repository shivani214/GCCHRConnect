using GCCHRMachinery.Entities;
using GCCHRMachinery.DataAccessLayer.MongoDb;
using System.Collections;

namespace GCCHRMachinery.BusinessLogicLayer
{
    public class  ContactService
    {
        ContactDB db = new ContactDB();

        public string CreateContact(Contact contactToCreate)
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
        public Contact GetRecordsById(string id)
        {
            Contact c;
            c = db.GetById(id);
            return c;
        }
        public void DeleteRecordsById(string id)
        {
            db.DeleteById(id);
        }

    }
}
