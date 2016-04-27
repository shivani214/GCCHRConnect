using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelBridge.Helper;
using GCCHRMachinery.Entities;
using UniversalEntities;
using GCCHRMachinery.Utilities;
using GCCHRMachinery.BusinessLogicLayer;
//ContactsManagement
//using ContactsManagement.Objects;
//using ContactsManagement.Logics;

namespace GCCHRConnect.ContactsManagement
{
    public partial class ImportUtility : Page
    {
        DataSet importedExcel;
        const string VIEWSTATE_DATASET = "ExctractedRecords";
        List<string> columns;
        //DataRow rowContact;
        private DataSet AllContactsList
        {
            get
            {
                return importedExcel;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Literal activeStepName = (Literal)Wizard1.FindControl("HeaderContainer").FindControl("ActiveStepName");
            //activeStepName.Text = Wizard1.ActiveStep.Name;
            if (!Page.IsPostBack)
            {
                Wizard1.ActiveStepIndex = 0;
            }
            else
            {
                if (ViewState["ImportedExcel"] != null)
                {
                    importedExcel = (DataSet)ViewState["ImportedExcel"];
                }
            }
        }

        /// <summary>
        /// Get complete path of file, including file name
        /// </summary>
        /// <returns></returns>
        private string savePath(string fileName)
        {
            DirectoryInfo tempDir;
            string path = "~/temp/ExcelFiles";
            if (Directory.Exists(Server.MapPath(path)))
            {
                tempDir = new DirectoryInfo(Server.MapPath(path));
            }
            else
            {
                tempDir = Directory.CreateDirectory(Server.MapPath(path));
            }
            return string.Format("{0}\\{1}", tempDir.FullName, fileName);
            //todo Add code to check for duplicate files (if a file already exists)
        }

        protected void Extract_Click(object sender, EventArgs e)
        {
            using (ExcelBridge.ExcelFile xl = new ExcelBridge.ExcelFile(LocationOfSavedFile.Value))
            {
                importedExcel = new DataSet();
                //Stopwatch watch = Stopwatch.StartNew();
                //watch.Start();
                importedExcel = xl.Import();
                //watch.Stop();
            }
            DataSetHelper datasetOperations = new DataSetHelper(ref importedExcel);
            datasetOperations.RemoveBlankRows();

            AddDatasetToViewstate();

            SummaryRecordCount.DataSource = datasetOperations.GetRecordsCount();
            SummaryRecordCount.DataBind();
            UploadSuccess.Visible = false;
            Extract.Visible = false;
            ExtractionSuccess.Visible = true;
            ExtractResult.Visible = true;
            Transform.Visible = true;
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


        protected void SaveFile_Click(object sender, EventArgs e)
        {
            //Uploading file

            string fileName = FileUpload1.PostedFile.FileName;
            LocationOfSavedFile.Value = savePath(fileName);

            try
            {
                FileUpload1.SaveAs(LocationOfSavedFile.Value);
            }
            catch (IOException x) when (x.Message.Contains("being used by another process"))
            {
                //todo release busy file or save by another name
                throw;
            }

            if (File.Exists(LocationOfSavedFile.Value))
            {
                bool requiredColumnsExist;
                using (ExcelBridge.ExcelFile xl = new ExcelBridge.ExcelFile(LocationOfSavedFile.Value))
                {
                    string[] requiredColumns = new string[2] { "Title", "First name" };
                    requiredColumnsExist = xl.HeadersExist(requiredColumns);
                }
                if (!requiredColumnsExist)
                {
                    ErrorMessage.Text = "Required columns <strong>Title</strong> and/or <strong>First name</strong> missing";
                    ErrorAlert.Visible = true;
                    //todo clear FileUpload1
                }
                else
                {
                    //ErrorAlert.Visible = false;
                    Wizard1.ActiveStepIndex++;
                    UploadedFileName.Text = fileName;
                }
            }
            //Import.Visible = true;
        }

        private void AddDatasetToViewstate()
        {
            ViewState.Add(VIEWSTATE_DATASET, importedExcel);
        }

        protected void Transform_Click(object sender, EventArgs e)
        {
            Transform.Visible = false;
            importedExcel = (DataSet)ViewState[VIEWSTATE_DATASET];
            //todo Clear ViewState

            List<Contact> allContacts = new List<Contact>();
            //Prepare table for validation summary of all contacts
            DataTable contactsValidationSummary = new DataTable();
            contactsValidationSummary.Columns.Add("Sheet");
            contactsValidationSummary.Columns.Add("Row number");
            contactsValidationSummary.Columns.Add("Error encountered");
            bool invalidContactsEncountered = false;

            //Begin procedure
            foreach (DataTable table in importedExcel.Tables)
            {
                #region GetColumnNames
                string[] columnNames = table.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                columns = columnNames.ToList();
                #endregion
                //Transforming values from rows into Contact object
                foreach (DataRow row in table.Rows)
                {
                    string columnName;
                    Contact prepareContact = new Contact();
                    //Serial number
                    columnName = "SN";
                    int serialNumber;
                    if (columns.Contains(columnName))
                    {
                        try
                        {
                            serialNumber = (int)row[columnName];
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        serialNumber = table.Rows.IndexOf(row) + 2;
                    }
                    // Title
                    columnName = "Title";
                    if (columns.Contains(columnName))
                    {
                        prepareContact.Name.Title = (string)row[columnName];
                    }
                    // First name
                    columnName = "First name";
                    if (columns.Contains(columnName))
                    {
                        prepareContact.Name.First = (string)row[columnName];
                    }
                    // Middle name
                    columnName = "Middle name";
                    if (columns.Contains(columnName))
                    {
                        prepareContact.Name.Middle = (string)row[columnName];
                    }
                    // Last name
                    columnName = "Last name";
                    if (columns.Contains(columnName))
                    {
                        prepareContact.Name.Last = (string)row[columnName];
                    }
                    // Nickname
                    columnName = "Nick name";
                    if (columns.Contains(columnName))
                    {
                        prepareContact.NickName = (string)row[columnName];
                    }
                    // Tags
                    columnName = "Tags";
                    if (columns.Contains(columnName))
                    {
                        string raw = (string)row[columnName];
                        prepareContact.Tags = raw.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                    // Emails
                    columnName = "Emails";
                    if (columns.Contains(columnName))
                    {
                        string raw = (string)row[columnName];
                        prepareContact.Emails = raw.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                    // Mobiles
                    columnName = "Mobiles";
                    if (columns.Contains(columnName))
                    {
                        string raw = (string)row[columnName];
                        prepareContact.Mobiles = raw.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                    // Phones
                    columnName = "Phone numbers";
                    if (columns.Contains(columnName))
                    {
                        string raw = (string)row[columnName];
                        prepareContact.Phones = raw.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    }

                    // Address
                    Address address = new Address();
                    //Line1
                    columnName = "Line 1";
                    string[] line1 = new string[] { };
                    if (columns.Contains(columnName))
                    {
                        address.Line1 = (string)row[columnName];
                    }
                    //Line2
                    columnName = "Line 2";
                    if (columns.Contains(columnName))
                    {
                        address.Line2 = (string)row[columnName];
                    }
                    //Line3
                    columnName = "Line 3";
                    if (columns.Contains(columnName))
                    {
                        address.Line3 = (string)row[columnName];
                    }
                    //City
                    columnName = "City";
                    if (columns.Contains(columnName))
                    {
                        address.City = (string)row[columnName];
                    }
                    //Pincode
                    columnName = "Pin code";
                    if (columns.Contains(columnName))
                    {
                        address.PinCode = (string)row[columnName];
                    }
                    //State
                    columnName = "State";
                    if (columns.Contains(columnName))
                    {
                        address.State = (string)row[columnName];
                    }
                    //Country
                    columnName = "Country";
                    if (columns.Contains(columnName))
                    {
                        address.Country = (string)row[columnName];
                    }
                    address.SortAddressLines();
                    prepareContact.Addresses.Add(address);

                    ContactService contactManager = new ContactService();
                    try
                    {
                        contactManager.Validate(prepareContact);
                        allContacts.Add(prepareContact);
                    }
                    catch (ArgumentNullException argNullX)
                    {
                        //todo If any contact invalid
                        invalidContactsEncountered = true;
                        contactsValidationSummary.Rows.Add(table.TableName, serialNumber, argNullX.Message);
                    }
                    catch (ArgumentException argX)
                    {
                        //todo If any contact invalid
                        invalidContactsEncountered = true;
                        contactsValidationSummary.Rows.Add(table.TableName, serialNumber, argX.Message);
                    }
                }
            }
            ValidationError.Visible = invalidContactsEncountered;
            if (invalidContactsEncountered)
            {
                ValidationSummary.DataSource = contactsValidationSummary;
                ValidationSummary.DataBind();
            }
            else
            {
                TransformedContacts.DataSource = allContacts;
                TransformedContacts.DataBind();
                Wizard1.ActiveStepIndex++;
            }

            //#region Debugging
            //tempGridView.DataSource = importedExcel.Tables[0];
            //tempGridView.DataBind();
            //#endregion

        }

        protected void Wizard1_CancelButtonClick(object sender, EventArgs e)
        {
            //todo Where should the user be taken to on cancelling import process
            Response.Redirect("#");
        }
    }
}