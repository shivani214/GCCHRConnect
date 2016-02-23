using GCCHRMachinery.Entities;
using GCCHRMachinery.DataAccessLayer.MongoDb;
using System.Collections;

namespace GCCHRMachinery.BusinessLogicLayer
{
    /// <summary>
    /// The logic layer for <see cref="Contact"/>
    /// </summary>
    public class  ContactService
    {
        ContactDB db = new ContactDB();

        /// <summary>
        /// Inserts a new <see cref="Contact"/> in the database
        /// </summary>
        /// <param name="contactToCreate">The <see cref="Contact"/> to be inserted</param>
        /// <returns>Id of the inserted <see cref="Contact"/></returns>
        public string CreateContact(Contact contactToCreate)
        {
            string createdId;
            createdId = db.Create(contactToCreate);
            return createdId;
        }

        /// <summary>
        /// Gets all <see cref="Contact"/> from the database
        /// </summary>
        /// <returns>An IEnumerable of <see cref="Contact"/></returns>
        public IEnumerable GetAllRecords()
        {
            IEnumerable allRecords;
            allRecords = db.GetAll();
            return allRecords;
        }
        /// <summary>
        /// Searches a contact from the database
        /// </summary>
        /// <param name="id">The <paramref name="id"/> of the <see cref="Contact"/> to be searched</param>
        /// <returns>A single <see cref="Contact"/></returns>
        public Contact GetRecordById(string id)
        {
            Contact c;
            c = db.GetById(id);
            return c;
        }

        /// <summary>
        /// Deletes a <see cref="Contact"/>
        /// </summary>
        /// <param name="id">The <paramref name="id"/> of the <see cref="Contact"/> to be deleted</param>
        public void DeleteRecordsById(string id)
        {
            db.DeleteById(id);
        }

    }
}
