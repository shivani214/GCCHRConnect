using System;
using System.Web.UI.WebControls;
using GCCHRMachinery.BusinessLogicLayer;
using GCCHRMachinery.Entities;
using System.Collections.Generic;

namespace GCCHRConnect.ContactsManagement
{
    public partial class ContactList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ContactPersonOrganizationService contactManager = new ContactPersonOrganizationService();
            //GridViewContacts.DataSource = contactManager.GetAllRecords();
            //GridViewContacts.DataBind();

            //List<ContactPersonOrganization> contacts = new List<ContactPersonOrganization>();
            //ContactPersonOrganizationService contactsService = new ContactPersonOrganizationService();
            //contacts = contactsService.GetAllRecords() as List<ContactPersonOrganization>;
            //GridViewContacts.DataSource = contacts;
        }

        protected void GridViewContacts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                ContactPersonOrganization contactBeingBound = e.Row.DataItem as ContactPersonOrganization;
                //GridView gridTags = e.Row.FindControl("GridViewTags") as GridView;
                //gridTags.DataSource = contactBeingBound.Tags;
               // gridTags.DataBind();
                GridView gridMobiles = e.Row.FindControl("GridViewMobiles") as GridView;
                gridMobiles.DataSource = contactBeingBound.Mobiles;
                gridMobiles.DataBind();
                GridView gridPhones = e.Row.FindControl("GridViewPhones") as GridView;
                gridPhones.DataSource = contactBeingBound.Phones;
                gridPhones.DataBind();
                GridView gridEmails = e.Row.FindControl("GridViewEmails") as GridView;
                gridEmails.DataSource = contactBeingBound.Emails;
                gridEmails.DataBind();
                GridView gridAddresses = e.Row.FindControl("GridViewAddresses") as GridView;
                gridAddresses.DataSource = contactBeingBound.Addresses;
                gridAddresses.DataBind();
                
            }
        }
    }
}