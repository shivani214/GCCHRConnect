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
        /// <summary>
        /// Looks up <see cref="Contact.Tags"/> for each <see cref="Contact"/> and if any is missing in master list <see cref="Tag"/>, it is inserted there.
        /// </summary>
        public void UpdateMissingTags()
        {
            ContactService contactService = new ContactService();
            TagDB tagDb = new TagDB();
            IEnumerable allContacts = contactService.GetAllRecords();
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
                        CreateTag(newTag);
                    }
                }
            }
            allContacts = null;
            tagDb = null;
        }

        /// <summary>
        /// Inserts tag in database after validating it
        /// </summary>
        /// <param name="newTag">Tag to insert</param>
        /// <returns><see cref="Tag.Id"/> of the inserted <paramref name="newTag"/></returns>
        /// <exception cref="ArgumentNullException">If <see cref="Tag.TagName"/> is null, empty, blank or whitespace</exception>
        public string CreateTag(Tag newTag)
        {
            if (string.IsNullOrWhiteSpace(newTag.TagName))
            {
                throw new ArgumentNullException("newTag.TagName", "The TagName cannot be null");
            }
            newTag.TagName.Trim();
            TagDB dbOp = new TagDB();
            dbOp.Create(newTag);
            dbOp = null;
            return newTag.Id;
        }

        public IEnumerable<string> GetAllTagNamesOnly()
        {
            TagDB db = new TagDB();
            IEnumerable<string> tagNames = db.GetAllTagNames();
            return tagNames;
        }
    }
}
