using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCCHRMachinery.Entities;
using GCCHRMachinery.DataAccessLayer.MongoDb;
using System.Collections.Generic;

namespace GCCHRMachineryTest.DataAccessLayer.MongoDb.Contacts_management
{
    [TestClass]
    public class TagDBTest
    {
        [TestMethod]
        public void CreateTag()
        {
            Tag newTag = new Tag();
            TagDB dbOp = new TagDB();
            newTag.TagName= "brother";
            string IdofnewTag = dbOp.Create(newTag);
            System.Diagnostics.Debug.Write("<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(IdofnewTag);
        }

        [TestMethod]
        public void GetAllTags()
        {
            TagDB dbOp = new TagDB();
            IEnumerable<Tag> allTags = dbOp.GetAll();
            foreach (Tag tag in allTags)
            {
                System.Diagnostics.Debug.WriteLine(tag.ToString());
            }

        }
    }
}
