using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.Entities;
using System.Collections.Generic;
using GCCHRMachinery.DataAccessLayer.MongoDb;

namespace GCCHRMachineryTest.DataAccessLayer.MongoDb
{
    [TestClass]
    public class ContactPersonOrganizationDB_Test
    {
        [TestMethod]
        public void CreateContactTest()
        {
            ContactPersonOrganization contactToCreate = new ContactPersonOrganization();
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
            address1.Country= "Country";
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

            string idOfNewContact = ContactPersonOrganizationDB.CreateContact(contactToCreate);
            System.Diagnostics.Debug.Write("<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(idOfNewContact);
        }

        [TestMethod]
        public void GetContact()
        {
            ContactPersonOrganization contact;
            contact = ContactPersonOrganizationDB.GetContact("569b64cf49894904107c2680");
            System.Diagnostics.Debug.Write("<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(contact.Name.Title + " " + contact.Name.First);
        }
    }
}
