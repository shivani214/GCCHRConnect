using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.Entities;
using GCCHRMachinery.DataAccessLayer.MongoDb;

namespace GCCHRMachineryTest.DataAccessLayer.MongoDb.Contacts_management
{
    [TestClass]
    public class TagDBTest
    {
        [TestMethod]
        public void CreateTag()
        {
            Tag newTag = new Tag();
            newTag.TagName= "Doctor";
            string IdofnewTag = TagDB.CreateTag(newTag);
            System.Diagnostics.Debug.Write("<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(IdofnewTag);
        }
    }
}
