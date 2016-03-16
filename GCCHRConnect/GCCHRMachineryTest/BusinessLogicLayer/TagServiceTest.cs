using GCCHRMachinery.BusinessLogicLayer;
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
        public void UpdateMissingTagsTest()
        {
            TagService updateTag = new TagService();
            updateTag.UpdateMissingTags(); 
        }
    }
}
