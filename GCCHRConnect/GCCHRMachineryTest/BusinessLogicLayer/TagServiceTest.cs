using GCCHRMachinery.BusinessLogicLayer;
using GCCHRMachinery.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachineryTest.BusinessLogicLayer
{
    [TestClass]
    public class TagServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "The TagName cannot be null")]
        public void BlankTagCannotBeInserted()
        {
            Tag nullTag = new Tag();
            Tag emptyTag = new Tag() { TagName = string.Empty };
            Tag blankTag = new Tag() { TagName = "" };
            Tag whitespaceTag = new Tag() { TagName = "            " };

            TagService tagservice = new TagService();

            tagservice.CreateTag(nullTag);
            tagservice.CreateTag(emptyTag);
            tagservice.CreateTag(blankTag);
            tagservice.CreateTag(whitespaceTag);
        }
    }
}
