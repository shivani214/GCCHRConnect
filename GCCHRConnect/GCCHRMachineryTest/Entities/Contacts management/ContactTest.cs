using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.Entities;
namespace GCCHRMachineryTest.Entities.Contacts_management
{
    [TestClass]
    public class ContactTest
    {
        [TestMethod]
        public void TrimAllStrings()
        {
            Contact c = new Contact();
            c.Name.Title = " Dr . ";
            c.Name.First = "Gaurang \t";
            c.Name.Middle = "Friend\t";
            c.Name.Last = "\tGupta";
            c.NickName = "Docsaab";
            c.Emails.Add("    gaurang@hotmail.com    ");
            c.Emails.Add("    gaurang@gmail.com");
            c.Mobiles.Add("+919005120111");
            c.Mobiles.Add("9005120111");
            c.Phones.Add("+915224060310");
            c.Tags.Add(" Doctor ");
            c.Tags.Add(" Self");
            c.Addresses.Add(new UniversalEntities.Address {Line1= " A-1/13 ",Line2="Sector-F ", Line3="Janki Puram     ",City="Lucknow",PinCode="     226021    ",State="UP",Country="India   "});
            c.Addresses.Add(new UniversalEntities.Address { Line1 = " B-1/41   ", Line2 = "    Sector-A ", Line3 = "     Kapoorthala Aliganj     \t   ", City = "Lucknow  ", PinCode = "226024    ", State = "Uttar Pradesh ", Country = "India   " });
            c.TrimAllStringCollections();
            Assert.AreEqual("Dr .", c.Name.Title);
            Assert.AreEqual("Gaurang", c.Name.First);
                Assert.AreEqual("Friend", c.Name.Middle);
            Assert.AreEqual("Gupta", c.Name.Last);
            Assert.AreEqual("Docsaab", c.NickName);
            Assert.AreEqual("gaurang@hotmail.com", c.Emails[0]);
            Assert.AreEqual("gaurang@gmail.com",c.Emails[1]);
            Assert.AreEqual("+919005120111", c.Mobiles[0]);
            Assert.AreEqual("9005120111", c.Mobiles[1]);
            Assert.AreEqual("Doctor", c.Tags[0]);
            Assert.AreEqual("Self", c.Tags[1]);
            Assert.AreEqual("A-1/13", c.Addresses[0].Line1);
            Assert.AreEqual("Sector-F", c.Addresses[0].Line2);
            Assert.AreEqual("Janki Puram", c.Addresses[0].Line3);
            Assert.AreEqual("Lucknow", c.Addresses[0].City);
            Assert.AreEqual("226021", c.Addresses[0].PinCode);
            Assert.AreEqual("UP", c.Addresses[0].State);
            Assert.AreEqual("India", c.Addresses[0].Country);

            Assert.AreEqual("B-1/41", c.Addresses[1].Line1);
            Assert.AreEqual("Sector-A", c.Addresses[1].Line2);
            Assert.AreEqual("Kapoorthala Aliganj", c.Addresses[1].Line3);
            Assert.AreEqual("Lucknow", c.Addresses[1].City);
            Assert.AreEqual("226024", c.Addresses[1].PinCode);
            Assert.AreEqual("Uttar Pradesh", c.Addresses[1].State);
            Assert.AreEqual("India", c.Addresses[1].Country);
        }
    }
}
