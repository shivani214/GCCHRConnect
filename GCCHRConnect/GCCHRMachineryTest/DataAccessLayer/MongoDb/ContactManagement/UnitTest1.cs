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

            contactToCreate.Phones = new List<UniversalEntities.Phone>();
            UniversalEntities.Phone phone1;
            phone1.CountryCode = "+91";
            phone1.StdCode = "0522";
            phone1.Number = "2326464";
            contactToCreate.Phones.Add(phone1);

            UniversalEntities.Phone phone2;
            phone2.CountryCode = "+91";
            phone2.StdCode = "0522";
            phone2.Number = "2326565";
            contactToCreate.Phones.Add(phone2);

            contactToCreate.Emails = new List<UniversalEntities.Email>();
            UniversalEntities.Email email1;
            email1.EmailAddress = "gaurangfgupta@gmail.com";
            contactToCreate.Emails.Add(email1);

            UniversalEntities.Email email2;
            email2.EmailAddress = "gaurang_gupta@hotmail.com";
            contactToCreate.Emails.Add(email2);

            UniversalEntities.Email email3;
            email3.EmailAddress = "gaurang_gupta@yahoo.com";
            contactToCreate.Emails.Add(email3);

            string idOfNewContact = ContactPersonOrganizationDB.CreateContact(contactToCreate);
            System.Diagnostics.Debug.Write("<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(idOfNewContact);
        }
    }
}
