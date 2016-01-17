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
            UniversalEntities.PersonName name;
            name.Title = "Dr.";
            name.First = "Gaurang";
            name.Middle = "Friend";
            name.Last = "Gupta";
            contactToCreate.Name = name;

            contactToCreate.Addresses = new List<UniversalEntities.Address>();
            UniversalEntities.Address address;
            address.Line1 = "Lucknow";
            address.Line2 = "Lucknow";
            address.Line3 = "Lucknow";
            address.City = "Lucknow";
            address.PinCode = "226021";
            address.State = "UP";
            address.Country= "Country";
            contactToCreate.Addresses.Add(address);

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
    }
}
