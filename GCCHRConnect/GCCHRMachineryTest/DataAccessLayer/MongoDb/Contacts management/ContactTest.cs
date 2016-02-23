using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.Entities;
using System.Collections.Generic;
using GCCHRMachinery.DataAccessLayer.MongoDb;
using UniversalEntities;

namespace GCCHRMachineryTest.DataAccessLayer.MongoDb
{
    [TestClass]
    public class ContactDBTest
    {
        [TestMethod]
        public void GetContact()
        {
            Contact contact = new Contact();
            ContactDB dbOp = new ContactDB();
            contact = dbOp.GetById("569cc58b2e4487187893b3f7");
            
            System.Diagnostics.Debug.Write("<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(contact.ToString());
        }

        [TestMethod]
        public void GetAllContacts()
        {
            ContactDB dbOp = new ContactDB();

            IEnumerable<Contact> allContacts = dbOp.GetAll();
            
            foreach (Contact contact in allContacts)
            {
                System.Diagnostics.Debug.WriteLine(contact.ToString());
            }
        }

        [TestMethod]
        public void CreateContact()
        {
            Contact contactToCreate = new Contact();
            //contactToCreate.Id = "Try#7";
            contactToCreate.Name = new UniversalEntities.PersonName();
            //UniversalEntities.PersonName name;
            contactToCreate.Name.Title = "Dr.";
            contactToCreate.Name.First = "Gaurang";
            contactToCreate.Name.Middle = "Friend";
            contactToCreate.Name.Last = "Gupta";

            contactToCreate.Addresses = new List<UniversalEntities.Address>();
            UniversalEntities.Address address1 = new UniversalEntities.Address();
            address1.Line1 = "Lucknow";
            address1.Line2 = "Lucknow";
            address1.Line3 = "Lucknow";
            address1.City = "Lucknow";
            address1.PinCode = "226021";
            address1.State = "UP";
            address1.Country = "Country";
            contactToCreate.Addresses.Add(address1);

            UniversalEntities.Address address2 = new UniversalEntities.Address();
            address2.Line1 = "Lucknow";
            address2.Line2 = "Lucknow";
            address2.Line3 = "Lucknow";
            address2.City = "Lucknow";
            address2.PinCode = "226021";
            address2.State = "UP";
            address2.Country = "Country";
            contactToCreate.Addresses.Add(address2);

            contactToCreate.Phones = new List<string>();
            contactToCreate.Phones.Add("+915222326464");
            contactToCreate.Phones.Add("+915222326565");
            contactToCreate.Phones.Add("+915224060310");

            contactToCreate.Mobiles = new List<string>();
            contactToCreate.Mobiles.Add("+919005120111");
            contactToCreate.Mobiles.Add("+919919992181");

            contactToCreate.Emails = new List<string>();
            contactToCreate.Emails.Add("gaurangfgupta@gmail.com");
            contactToCreate.Emails.Add("gaurang_gupta@hotmail.com");
            contactToCreate.Emails.Add("gaurang_gupta@yahoo.com");

            contactToCreate.Tags = new HashSet<string>();
            contactToCreate.Tags.Add("Self");
            contactToCreate.Tags.Add("Doctor");
            contactToCreate.Tags.Add("Doctor");
            
            ContactDB db = new ContactDB();


            string idOfNewContact = db.Create(contactToCreate);
            System.Diagnostics.Debug.Write("<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(idOfNewContact);
        }
        [TestMethod]
        public void DeleteById()
        {

            ContactDB dbOp = new ContactDB();
            dbOp.DeleteById("569cc58b2e4487187893b3f7");
        }

        [TestMethod]
        public void GetContactsByName()
        {
            PersonName searchCriteria = new PersonName();
            //searchCriteria.Id = "569c93c44989491f542704c2";
            //searchCriteria.First = "Gaurang";
            //searchCriteria.Middle = "Friend";
            //searchCriteria.Last = "Gupta";
            //searchCriteria.Title = "Dr.";

            List<Contact> cList = new List<Contact>();
            ContactDB dbOp = new ContactDB();
            cList = dbOp.SearchContacts(searchCriteria);
            foreach (Contact contact in cList)
            {
                System.Diagnostics.Debug.WriteLine(contact.ToString());
            }
        }

        [TestMethod]
        public void GetContactsByString()
        {
            List<Contact> filteredList = new List<Contact>();
            string universalFilter = "1978";
            ContactDB dbOp = new ContactDB();
            filteredList = dbOp.SearchContacts(universalFilter);
            foreach (Contact contact in filteredList)
            {
                System.Diagnostics.Debug.WriteLine(contact.ToString());
            }
        }
    }
}
