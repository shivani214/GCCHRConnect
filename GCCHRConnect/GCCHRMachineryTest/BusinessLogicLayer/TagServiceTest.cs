using GCCHRMachinery.BusinessLogicLayer;
using GCCHRMachinery.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace GCCHRMachineryTest.BusinessLogicLayer
{
    [TestClass]
    public class TagServiceTest
    {
        TagService tagManager;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The TagName cannot be null")]
        public void BlankTagCannotBeInserted()
        {
            GCCHRMachinery.Entities.Tag nullTag = new GCCHRMachinery.Entities.Tag();
            GCCHRMachinery.Entities.Tag emptyTag = new GCCHRMachinery.Entities.Tag() { TagName = string.Empty };
            GCCHRMachinery.Entities.Tag blankTag = new GCCHRMachinery.Entities.Tag() { TagName = "" };
            GCCHRMachinery.Entities.Tag whitespaceTag = new GCCHRMachinery.Entities.Tag() { TagName = "            " };

            tagManager = new TagService();

            tagManager.CreateTag(nullTag);
            tagManager.CreateTag(emptyTag);
            tagManager.CreateTag(blankTag);
            tagManager.CreateTag(whitespaceTag);
        }

        [TestMethod]
        public void DuplicateTagCreationProhibited()
        {
            GCCHRMachinery.Entities.Tag duplicateTag = new GCCHRMachinery.Entities.Tag();
            duplicateTag.TagName = "Doctor";
            TagService tagManager = new TagService();
            try
            {
                tagManager.CreateTag(duplicateTag);
                Assert.Fail("An exception should have been thrown");
            }
            catch (MongoWriteException ex)
            {
                Assert.IsTrue(ex.Message.Contains("duplicate key error"));
                //throw;
            }
        }
    }
}
