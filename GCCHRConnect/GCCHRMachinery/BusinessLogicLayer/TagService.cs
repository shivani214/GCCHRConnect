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
        /// Looks for each tag from <paramref name="tagsToCheckAndUpdate"/> and if any is missing in master list <see cref="Tag"/>, it is inserted there.
        /// </summary>
        public void UpdateMissingTags(HashSet<string> tagsToCheckAndUpdate)
        {
            IEnumerable<string> allTagsFromMaster = GetAllTagNamesOnly();
            List<string> tagsMasterList = allTagsFromMaster.ToList();
            foreach (string tagToCheck in tagsToCheckAndUpdate)
            {
                if (!tagsMasterList.Contains(tagToCheck))
                {
                    Tag newTag = new Tag();
                    newTag.TagName = tagToCheck;
                    CreateTag(newTag);
                }
            }
        }

        /// <summary>
        /// Looks for each tag from tags of <paramref name="contactTagsToCheckUpdate"/> and if any is missing in master list <see cref="Tag"/>, it is inserted there.
        /// </summary>
        /// <param name="contactTagsToCheckUpdate"></param>
        public void UpdateMissingTags(Contact contactTagsToCheckUpdate)
        {
            UpdateMissingTags(contactTagsToCheckUpdate.Tags);
        }

        /// <summary>
        /// Looks for each tag from tags of each <paramref name="contactsTagsToCheckAndUpdate"/> and if any is missing in master list <see cref="Tag"/>, it is inserted there.
        /// </summary>
        /// <param name="contactsTagsToCheckAndUpdate"></param>
        public void UpdateMissingTags(IEnumerable<Contact> contactsTagsToCheckAndUpdate)
        {
            foreach (Contact contact in contactsTagsToCheckAndUpdate.ToList())
            {
                UpdateMissingTags(contact);
            }
        }

        /// <summary>
        /// Inserts tag in database after validating it
        /// </summary>
        /// <param name="newTag">Tag to insert</param>
        /// <returns><see cref="Tag.Id"/> of the inserted <paramref name="newTag"/></returns>
        /// <exception cref="ArgumentNullException">If <see cref="Tag.TagName"/> is null, empty, blank or whitespace</exception>
        public string CreateTag(Tag newTag)
        {
            Validate(newTag.TagName);
            newTag.TagName.Trim();
            TagDB dbOp = new TagDB();
            dbOp.Create(newTag);
            dbOp = null;
            return newTag.Id;
        }

        /// <summary>
        /// Validates a tag name
        /// </summary>
        /// <param name="tagNameToValidate">The <see cref="Tag"/> to be validated</param>
        /// <exception cref="ArgumentNullException"><paramref name="tagNameToValidate"/></exception>
        public static void Validate(string tagNameToValidate)
        {
            if (string.IsNullOrWhiteSpace(tagNameToValidate))
            {
                throw new ArgumentNullException("newTag.TagName", "The TagName cannot be null");
            }
        }

        /// <summary>
        /// Validates a list of tag names
        /// </summary>
        /// <param name="tagNamesToValidate">The list of tags to be validated</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Validate(HashSet<string> tagNamesToValidate)
        {
            foreach (string tag in tagNamesToValidate)
            {
                Validate(tag);
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
