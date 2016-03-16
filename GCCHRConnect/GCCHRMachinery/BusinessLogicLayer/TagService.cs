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
                IEnumerable<Tag> allTags = tagDb.GetAll();
                List<Tag> tagsMasterList = allTags.ToList();
                List<string> tagsReferenceList = new List<string>();
                foreach (Tag tag in tagsMasterList)
                {
                    tagsReferenceList.Add(tag.TagName);
                }
                foreach (string tagToCheck in contact.Tags)
                {
                    if (!tagsReferenceList.Contains(tagToCheck))
                    {
                        Tag newTag = new Tag();
                        newTag.TagName = tagToCheck;
                        tagDb.Create(newTag);
                    }
                }
            }
        }
    }
}
