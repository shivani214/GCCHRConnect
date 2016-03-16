using GCCHRMachinery.DataAccessLayer.MongoDb;
using GCCHRMachinery.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.BusinessLogicLayer
{
    public class TagService
    {
        public void UpdateMissingTags()
        {
            ContactService contactService = new ContactService();
            TagDB tagDb = new TagDB();
            var allContacts = contactService.GetAllRecords();
            foreach (Contact contact in allContacts)
            {
                IEnumerable<string> allTags = tagDb.GetAllTagNames();
                List<string> tagsMasterList = allTags.ToList();
                foreach (string tagToCheck in contact.Tags)
                {
                    if (!tagsMasterList.Contains(tagToCheck))
                    {
                        Tag newTag = new Tag();
                        newTag.TagName = tagToCheck;
                        tagDb.Create(newTag);
                    }
                }
            }
        }

        public IEnumerable<string> GetAllTagNamesOnly()
        {
            TagDB db = new TagDB();
            IEnumerable<string> tagNames = db.GetAllTagNames();
            return tagNames;
        }
    }
}
