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

        }

        protected string GetTagStyle(int itemIndex)
        {
            int styleNumber = itemIndex % 6;
            string cssStyle = "";
            switch (styleNumber)
            {
                case 0:
                    cssStyle = "label label-default";
                    break;
                case 1:
                    cssStyle = "label label-primary";
                    break;
                case 2:
                    cssStyle = "label label-success";
                    break;
                case 3:
                    cssStyle = "label label-warning";
                    break;
                case 4:
                    cssStyle = "label label-danger";
                    break;
                case 5:
                    cssStyle = "label label-info";
                    break;
            }
            return cssStyle;
        }
    }
}