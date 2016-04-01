using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//ContactsManagement
//using ContactsManagement.Objects;
//using ContactsManagement.Logics;

namespace GCCHRConnect.ContactsManagement
{
    public partial class ImportUtility : System.Web.UI.Page
    {
        DataSet ImportedExcel;

        public DataSet AllContactsList
        {
            get
            {
                return ImportedExcel;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Wizard1.ActiveStepIndex = 0;
            }
            else
            {
                if (ViewState["ImportedExcel"] != null)
                {
                    ImportedExcel = (DataSet)ViewState["ImportedExcel"];
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Boolean");
            dt.Rows.Add(new object[] { "True", true });
            dt.Rows.Add(new object[] { "False", false });
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }

        /// <summary>
        /// Returns the the complete path (including file name)
        /// </summary>
        /// <returns></returns>
        private string savePath(string fileName)
        {
            //todo check if path exists, if not, create it
            string path = "/tempExcelUpload/";
            DirectoryInfo tempDir = new DirectoryInfo(Server.MapPath(path));
            return tempDir + fileName;
            //todo Add code to check for duplicate files (if a file already exists)
        }

        protected void Import_Click(object sender, EventArgs e)
        {
            ////string fileName = FileUpload1.PostedFile.FileName;
            ////string saveLocation = savePath(fileName);
            ////FileUpload1.SaveAs(saveLocation);

            ////FileName.Text = fileName;
            ////UploadedFileProperties.Visible = true;

            //ImportedExcel = new DataSet();

            //Dictionary<string, bool> ColumnConsistency;
            //Dictionary<string, int> RemoveBlankRows;
            //Dictionary<string, bool> validation;

            ////Stopwatch watch = Stopwatch.StartNew();
            ////watch.Start();
            //ImportedExcel = ContactLogic.ImportFromExcel(LocationOfSavedFile.Value);
            ////watch.Stop();

            //ColumnConsistency = new Dictionary<string, bool>();
            //ColumnConsistency = ContactLogic.CheckColumnConsistency(ref ImportedExcel);

            //RemoveBlankRows = new Dictionary<string, int>();
            //RemoveBlankRows = ContactLogic.RemoveBlankRows(ref ImportedExcel);

            //validation = new Dictionary<string, bool>();
            //validation = ContactLogic.ValidateImportedDataSet(ref ImportedExcel);


            //ColumnConsistencySummary.DataSource = ColumnConsistency;
            ////ColumnConsistencySummary.Visible = true;
            //ColumnConsistencySummary.DataBind();
            //BlankRowsDeleteSummary.DataSource = RemoveBlankRows;
            ////BlankRowsDeleteSummary.Visible = true;
            //BlankRowsDeleteSummary.DataBind();
            //ValidationSummary.DataSource = validation;
            ////ValidationSummary.Visible = true;
            //ValidationSummary.DataBind();

            //bool allTableColumnsConsistent = allTablesHaveConsistentColumns(ref ColumnConsistency);
            //bool allTablesValid = allTablesValidated(ref validation);
            //if (allTableColumnsConsistent && allTablesValid)
            //{
            //    MaintainDatasetToViewstate();
            //}

            //foreach (DataTable table in ImportedExcel.Tables)
            //{
            //    string remarksNotNull = "REMARKS <> ''";
            //    DataTable faultyContacts = filterFromTable(table, remarksNotNull);
            //    faultyContacts.TableName = table.TableName.Trim() + " Faulty contacts";
            //    string remarksNull = "REMARKS = '' OR REMARKS IS NULL";
            //    DataTable validContacts = filterFromTable(table, remarksNull);
            //    validContacts.TableName = table.TableName.Trim() + " Valid contacts";

            //    GridView gridFaultyContacts = new GridView();
            //    gridFaultyContacts.ID = "FaultyContactsGrid_" + table.TableName;
            //    gridFaultyContacts.Caption = faultyContacts.TableName;
            //    gridFaultyContacts.DataSource = faultyContacts;
            //    gridFaultyContacts.DataBind();
            //    FaultyContacts.Controls.Add(gridFaultyContacts);
            //    GridView gridValidContacts = new GridView();
            //    gridValidContacts.ID = "ValidContactsGrid_" + table.TableName;
            //    gridValidContacts.Caption = validContacts.TableName;
            //    gridValidContacts.DataSource = validContacts;
            //    gridValidContacts.DataBind();
            //    ValidContacts.Controls.Add(gridValidContacts);
            //}
            //ImportResult.Visible = true;
        }

        private DataTable filterFromTable(DataTable tbl, string filter)
        {
            DataTable t = new DataTable();
            DataRow[] selectedRows;
            selectedRows = tbl.Select(filter);

            t = tbl.Clone();
            foreach (DataRow row in selectedRows)
            {
                t.ImportRow(row);
            }
            return t;
        }
        private bool allTablesHaveConsistentColumns(ref Dictionary<string, bool> colConsistency)
        {
            bool check = true;
            foreach (KeyValuePair<string, bool> table in colConsistency)
            {
                if (table.Value == false)
                {
                    check = table.Value;
                }
            }
            return check;
        }

        private bool allTablesValidated(ref Dictionary<string, bool> tableValidation)
        {
            bool check = true;
            foreach (KeyValuePair<string, bool> table in tableValidation)
            {
                if (table.Value == false)
                {
                    check = table.Value;
                }
            }
            return check;
        }

        protected void SaveFile_Click(object sender, EventArgs e)
        {
            string fileName = FileUpload1.PostedFile.FileName;
            LocationOfSavedFile.Value = savePath(fileName);
            FileUpload1.SaveAs(LocationOfSavedFile.Value);
            FileName.Text = fileName;
            UploadedFileProperties.Visible = true;
            Wizard1.ActiveStepIndex++;
            //Import.Visible = true;
        }

        //private List<Contact> convertRowsToContacts()
        //{
        //    //todo Write another function which should precede this one....
        //    //....that function must check if all tables in dataset contain Columns with names same as used below in this function....
        //    //....if not, then the column name may be changed to match the one given here.
        //    //....Also, if a column does not exist, it may be added to avoid error when running the below code.
        //    List<Contact> contacts = new List<Contact>();

        //    foreach (DataTable table in ImportedExcel.Tables)
        //    {
        //        foreach (DataRow row in table.Rows)
        //        {
        //            string name = row["Name"].ToString();
        //            string nickName = row["Nick name"].ToString();

        //            Contact.Address add = new Contact.Address();
        //            add.Line1 = row["Line 1"].ToString();
        //            add.Line2 = row["Line 2"].ToString();
        //            add.Line3 = row["Line 3"].ToString();
        //            add.City = row["City"].ToString();
        //            add.PinCode = row["Pin code"].ToString();
        //            add.State = row["State"].ToString();
        //            add.Country = row["Country"].ToString();
        //            List<Contact.Address> addresses = new List<Contact.Address>();
        //            addresses.Add(add);

        //            string rowPhones = row["Phone number"].ToString();
        //            string rowEmails = row["Emails"].ToString();
        //            string rowTags = row["Tag"].ToString();

        //            Contact c = ContactLogic.ConvertToContact(name, nickName, addresses, rowPhones, rowEmails, rowTags);

        //            contacts.Add(c);
        //        }
        //    }
        //    return contacts;
        //}

        //protected void ConvertRowsToContacts_Click(object sender, EventArgs e)
        //{
        //    MaintainDatasetToViewstate();
        //    List<Contact> contacts = convertRowsToContacts();
        //    Repeater1.DataSource = contacts;
        //    Repeater1.DataBind();
        //}

        protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
        {
            MaintainDatasetToViewstate();
        }

        private void MaintainDatasetToViewstate()
        {
                ViewState.Add("ImportedExcel", ImportedExcel);
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            MaintainDatasetToViewstate();
        }
    }
}