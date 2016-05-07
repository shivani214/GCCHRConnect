using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.Entities;
using GCCHRMachinery.BusinessLogicLayer;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace GCCHRMachineryTest.BusinessLogicLayer
{
    [TestClass]
    public class ContactServiceTest
    {
        [TestMethod]
        public void Validate()
        {
            string phone = "05222635375";
            string result = Regex.Match(phone, @"^\+?[0-9]+$").ToString();
            bool match = Regex.IsMatch(phone, @"^\+?[0-9]+$");
            Assert.IsTrue(match);
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

            contactToCreate.Tags = new List<string>();
            contactToCreate.Tags.Add("Self");
            contactToCreate.Tags.Add("Doctor");
            contactToCreate.Tags.Add("Doctor");

            ContactService contactManager = new ContactService();
            contactManager.CreateContact(contactToCreate);
            Assert.IsNotNull(contactToCreate.Id);
        }
    }
}
