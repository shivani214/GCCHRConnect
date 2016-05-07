using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.Entities;
using UniversalEntities;
using GCCHRMachinery.Utilities;

namespace GCCHRMachineryTest.Utilities
{
    [TestClass]
    public class SorterTest
    {
        [TestMethod]
        public void Sort()
        {
            Address a = new Address();
            a.Line1 = "2";
            a.Line2 = "3";
            a.Line3 = "1";

            string[] itemValues = new string[] { a.Line1, a.Line2, a.Line3 };
            string[] itemKeys = new string[] { "Line1", "Line2" , "Line3" };
            Sorter.Sort(itemKeys,itemValues);

        }
    }
}
