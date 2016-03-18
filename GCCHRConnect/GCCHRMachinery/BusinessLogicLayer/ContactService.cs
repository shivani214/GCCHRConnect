using GCCHRMachinery.Entities;
using GCCHRMachinery.DataAccessLayer.MongoDb;
using System.Collections;
using UniversalEntities;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace GCCHRMachinery.BusinessLogicLayer
{
    /// <summary>
    /// The logic layer for <see cref="Contact"/>
    /// </summary>
    public class ContactService
    {
        /// <summary>
        /// Validates the <paramref name="contactToCreate"/>.
        /// Inserts any new tags from <see cref="Contact.Tags"/> in <see cref="Tag"/>.
        /// Inserts a new <see cref="Contact"/> in the database.
        /// </summary>
        /// <param name="contactToCreate">The <see cref="Contact"/> to be inserted</param>
        /// <returns>Id of the inserted <see cref="Contact"/></returns>
        public string CreateContact(Contact contactToCreate)
        {
            string createdId = "";
            Validate(contactToCreate);
            TagService tagService = new TagService();
            tagService.UpdateMissingTags(contactToCreate);

            //Not needed TagService.Validate(contactToCreate.Tags);
            ContactDB db = new ContactDB();
            createdId = db.Create(contactToCreate);

            return createdId;
        }

        /// <summary>
        /// Validates various properties of <see cref="Contact"/>. Called prior to <see cref="CreateContact(Contact)"/>
        /// </summary>
        /// <param name="contactToValidate"></param>
        public void Validate(Contact contactToValidate)
        {
            #region RequiredValidation
            if (string.IsNullOrWhiteSpace(contactToValidate.Name.Title))
            {
                throw new System.ArgumentNullException("Title");
            }
            if (string.IsNullOrWhiteSpace(contactToValidate.Name.First))
            {
                throw new System.ArgumentNullException("First");
            }
            foreach (Address address in contactToValidate.Addresses)
            {
                if (string.IsNullOrWhiteSpace(address.City))
                {
                    throw new System.ArgumentNullException("City");
                }
            }
            #endregion

            #region ValueTypes
            foreach (string phone in contactToValidate.Phones)
            {
                if (!Regex.IsMatch(phone, @"/^\+?[0-9]+$/g"))
                {
                    throw new System.ArgumentException("Phone number is not in the correct format. Only numbers allowed, optionally beginning with a '+' sign.");
                }
            }

            foreach (string mobile in contactToValidate.Mobiles)
            {
                if (!Regex.IsMatch(mobile, @"/^(\+91)?([0-9]{10})$/g"))
                {
                    throw new System.ArgumentException(@"Mobile number must be 10 digit number, optionally beginning from '+91' (excluding 10 digits) matching the regular expression /^(\+91)?([0-9]{10})$/g");
                }
            }

            foreach (string email in contactToValidate.Emails)
            {
                if (!Regex.IsMatch(email, @"/^[a-z0-9!#$%&*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/g"))
                {
                    throw new System.ArgumentException(@"Invalid email format. It must match the regular expression /^[a-z0-9!#$%&*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/g");
                }
            }
            #endregion
        }

        /// <summary>
        /// Gets all <see cref="Contact"/> from the database
        /// </summary>
        /// <returns>An IEnumerable of <see cref="Contact"/></returns>
        public IEnumerable GetAllRecords()
        {
            IEnumerable allRecords;
            ContactDB db = new ContactDB();
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
            ContactDB db = new ContactDB();
            c = db.GetById(id);
            return c;
        }

        /// <summary>
        /// Deletes a <see cref="Contact"/>
        /// </summary>
        /// <param name="id">The <paramref name="id"/> of the <see cref="Contact"/> to be deleted</param>
        public void DeleteRecordsById(string id)
        {
            ContactDB db = new ContactDB();
            db.DeleteById(id);
        }


    }
}
