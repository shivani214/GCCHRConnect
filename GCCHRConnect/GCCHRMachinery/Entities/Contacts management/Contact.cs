﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalEntities;
using MongoDB.Bson.Serialization.Attributes;

namespace GCCHRMachinery.Entities
{
    /// <summary>
    /// Represents a contact as in a contact in a phonebook, which uses <see cref="ContactPersonOrganization"/> from UniversalEntities
    /// </summary>
    public class Contact : ContactPersonOrganization, IMongoEntity
    {
        #region Fields
        public const string TableOrCollectionName = "Contacts";
        #endregion
        
        /// <summary>
        /// The unique id of the <see cref="Contact"/>
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Nick name of the <see cref="Contact"/>
        /// </summary>
        [BsonIgnoreIfNull]
        public string NickName { get; set; }
        /// <summary>
        /// A list of tags to which the <see cref="Contact"/> belongs.
        /// </summary>
        /// <remarks>The tags are from a Master list <see cref="Tag"/></remarks>
        public List<string> Tags { get; set; }

        #region Constructors and Methods
        public Contact()
        {
            Name = new PersonName();
            Addresses = new List<Address>();
            Mobiles = new List<string>();
            Phones = new List<string>();
            Emails = new List<string>();
            Tags = new List<string>();
        }

        public override string ToString()
        {
            StringBuilder contact = new StringBuilder();
            int i = 1;
            contact.AppendFormat("Id:{0}", Id);
            contact.AppendLine();
            contact.AppendFormat("Name: {0} {1} {2} {3}", Name.Title, Name.First, Name.Middle, Name.Last);
            contact.AppendLine();
            if (Tags != null)
            {
                contact.AppendLine(" Tags:");
                foreach (string tag in Tags)
                {
                    contact.AppendFormat("{0}. {1}", i++, tag);
                    contact.AppendLine();
                }
            }
            contact.AppendLine("*****************");
            return contact.ToString();
        }

        /// <summary>
        /// Trims the leading and/or trailing spaces or tabs from all the string properties or collections
        /// </summary>
        public void TrimAllStrings()
        {
            Name.Title= Name.Title.Trim();
            Name.First= Name.First.Trim();
            Name.Middle=Name.Middle.Trim();
            Name.Last=Name.Last.Trim();
            NickName=NickName.Trim();
            Emails = Emails.Select(d => d.Trim()).ToList();
            Mobiles = Mobiles.Select(d => d.Trim()).ToList();
            Phones = Phones.Select(d => d.Trim()).ToList();
            Tags = Tags.Select(d => d.Trim()).ToList();
            List<Address> tempAddresses = Addresses;
            //Addresses.Clear();
            foreach (Address address in tempAddresses)
            {
                //Addresses.Remove(address);
                address.Line1=address.Line1.Trim();
                address.Line2=address.Line2.Trim();
                address.Line3=address.Line3.Trim();
                address.City=address.City.Trim();
                address.PinCode=address.PinCode.Trim();
                address.State=address.State.Trim();
                address.Country=address.Country.Trim();
                //Addresses.Add(address);
            }

        }
        #endregion
    }
}
