using GCCHRMachinery.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using UniversalEntities;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    /// <summary>
    /// The datalayer for <see cref="Contact"/>
    /// </summary>
    public class ContactDB : MongoTask<Contact>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ContactDB() : base(Contact.TableOrCollectionName)
        {

        }

        /// <summary>
        /// Gets a list of <see cref="Contact"/> from database based on the search criteria <paramref name="nameFilter"/>. The search returns exact and not partial matches.
        /// </summary>
        /// <param name="nameFilter">The name as search criteria. If null value for a property in search criteria is not considered. To include it as a criteria, an empty string "" must be provided.</param>
        /// <returns>Multiple contacts matching the search criteria</returns>
        public List<Contact> SearchContacts(PersonName nameFilter)
        {
            List<Contact> searchedContacts = new List<Contact>();
            FilterDefinitionBuilder<Contact> filterBuild = Builders<Contact>.Filter;

            FilterDefinition<Contact> filter = filterBuild.Empty;
            if (!string.IsNullOrEmpty(nameFilter.Title))
            {
                filter = filter & filterBuild.Eq(c => c.Name.Title, nameFilter.Title);
            }

            if (!string.IsNullOrEmpty(nameFilter.First))
            {
                filter = filter & filterBuild.Eq(c => c.Name.First, nameFilter.First);
            }

            if (!string.IsNullOrEmpty(nameFilter.Middle))
            {
                filter = filter & filterBuild.Eq(c => c.Name.Middle, nameFilter.Middle);
            }

            if (!string.IsNullOrEmpty(nameFilter.Last))
            {
                filter = filter & filterBuild.Eq(c => c.Name.Last, nameFilter.Last);
            }
            searchedContacts = connector.Collection.Find(filter).ToList();
            return searchedContacts;
        }

        /// <summary>
        /// Search contacts from database based on a specified string. Tags are excluded from this search
        /// </summary>
        /// <param name="universalFilter">The string to be searched in records.</param>
        /// <returns>Partial matches are returned too.</returns>
        public List<Contact> SearchContacts(string universalFilter)
        {
            string regExFilter = "/" + universalFilter + "/i";
            List<Contact> searchedContacts = new List<Contact>();
            FilterDefinitionBuilder<Contact> filterBuild = Builders<Contact>.Filter;

            FilterDefinition<Contact> filter;
            filter = filterBuild.Regex(c => c.Name.Title, new BsonRegularExpression(regExFilter));
            filter = filter | filterBuild.Regex(c => c.Name.First, new BsonRegularExpression(regExFilter));
            filter = filter | filterBuild.Regex(c => c.Name.Middle, new BsonRegularExpression(regExFilter));
            filter = filter | filterBuild.Regex(c => c.Name.Last, new BsonRegularExpression(regExFilter));
            filter = filter | filterBuild.Regex(c => c.Addresses, new BsonRegularExpression(regExFilter));
            filter = filter | filterBuild.Regex(c => c.Mobiles, new BsonRegularExpression(regExFilter));
            filter = filter | filterBuild.Regex(c => c.Phones, new BsonRegularExpression(regExFilter));
            filter = filter | filterBuild.Regex(c => c.Emails, new BsonRegularExpression(regExFilter));
            searchedContacts = connector.Collection.Find(filter).ToList();
            return searchedContacts;
        }
    }
}
