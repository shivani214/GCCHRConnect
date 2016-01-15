using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.DataAccessLayer.MongoDb;

namespace GCCHRMachineryTest.DataAccessLayer.MongoDb
{
    [TestClass]
    public class GCCHRConnectDB
    {
        [TestMethod]
        public void InsertOperation()
        {
            TestDatabase.InsertToTestDatabase();
        }
    }
}
