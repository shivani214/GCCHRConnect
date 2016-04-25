using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.Entities;
using GCCHRMachinery.BusinessLogicLayer;
using System.Text.RegularExpressions;

namespace GCCHRMachineryTest.BusinessLogicLayer
{
    [TestClass]
    public class ContactServiceTest
    {
        [TestMethod]
        public void Validate()
        {
            string phone = "9412723600";
            string result = Regex.Match(phone, @"^\+?[0-9]+$").ToString();
            bool match = Regex.IsMatch(phone, @"^\+?[0-9]+$");
            Assert.IsTrue(match);
        }
    }
}
