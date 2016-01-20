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
            newTag.TagName= "Family";
            string IdofnewTag = TagDB.CreateTag(newTag);
            System.Diagnostics.Debug.Write("<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(IdofnewTag);
        }

        [TestMethod]
        public void GetAllTags()
        {
            List<Tag> allTags = TagDB.GetAllTags();
            foreach (Tag tag in allTags)
            {
                System.Diagnostics.Debug.WriteLine(tag.ToString());
            }

        }
    }
}
