<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportUtility.aspx.cs" Inherits="GCCHRConnect.ContactsManagement.ImportUtility" Trace="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="scripts/jquery-2.2.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery-2.2.1.js" type="text/javascript"></script>
    <%--<script src="scripts/jquery-2.2.1.intellisense.js" type="text/javascript"></script>--%>

    <link href="../Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/bootstrap.min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="300"></asp:ScriptManager>

        <div>
            <div class="jumbotron container-fluid">
                <h1>Import utility
                    <br />
                </h1>
                <h3>
                    <small>This utility helps to import contacts from an excel file and inserts them in the database</small>
                </h3>
            </div>
            <div id="container_ImportUtility" class="container">
                <div class="row">

                    <asp:Wizard ID="Wizard1" runat="server" DisplayCancelButton="True" ActiveStepIndex="2" OnActiveStepChanged="Wizard1_ActiveStepChanged" OnNextButtonClick="Wizard1_NextButtonClick"
                        CssClass="panel">

                        <HeaderStyle CssClass="page-header panel-body"></HeaderStyle>

                        <NavigationButtonStyle CssClass="btn btn-default"></NavigationButtonStyle>
                        <SideBarButtonStyle></SideBarButtonStyle>
                        <SideBarStyle CssClass="col-lg-2 panel-body"></SideBarStyle>
                        <StepStyle CssClass="col-lg-10 panel-body"></StepStyle>

                        <HeaderTemplate>
                            <h3 class="text-success">Import contacts from Microsoft Excel</h3>
                            <h4>
                                <small>
                                    <span><%= Wizard1.ActiveStep.Name %></span>
                                </small>
                            </h4>
                        </HeaderTemplate>
                        <StartNavigationTemplate>
                            <div class="panel-body">
                                <asp:Button runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger" ID="CancelButton"></asp:Button>
                                <asp:Button runat="server" CommandName="MoveNext" Text="Next" CssClass="btn btn-default" ID="StartNextButton"></asp:Button>
                            </div>
                        </StartNavigationTemplate>
                        <StepNavigationTemplate>
                            <div class="panel-body">
                                <asp:Button runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger" ID="CancelButton"></asp:Button>
                            </div>
                            <%--<asp:Button runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Previous" CssClass="btn btn-default" ID="StepPreviousButton"></asp:Button>
                            <asp:Button runat="server" CommandName="MoveNext" Text="Next" CssClass="btn btn-default" ID="StepNextButton"></asp:Button>--%>
                        </StepNavigationTemplate>

                        <WizardSteps>

                            <asp:WizardStep ID="WizardStep1" runat="server" Title="Begin">
                                <p>Before you go ahead, go through this checklist for a smooth import experience</p>
                                <%--bulletedlist--%>
                                <p>Go through following easy steps</p>
                                <asp:BulletedList ID="BulletedList1" runat="server">
                                    <asp:ListItem>The Excel file may contain single or multiple sheets</asp:ListItem>
                                    <asp:ListItem>Each sheet must have the same columns (order of columns need not be same)</asp:ListItem>
                                    <asp:ListItem>The values will be validated eg. mobiles must be numbers only</asp:ListItem>
                                    <asp:ListItem>Any blank rows will be deleted</asp:ListItem>
                                    <asp:ListItem Text="Any tags which are not present in database will be created for you, duplicates will be ignored" />
                                </asp:BulletedList>


                                <p>ToDo: Show a list of tags available in the tags collection so that user may either edit his tags (in excel) to match the ones provided or leave them as it is so they may be added to the Tag collection</p>
                            </asp:WizardStep>

                            <asp:WizardStep ID="FileUpload" runat="server" Title="Upload file to server">
                                <asp:Panel runat="server" ID="fileUploadPanel">
                                    <p>
                                        <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select the Miscrosoft Excel file from which to import contacts" BackColor="#404040" ForeColor="Black" />
                                    </p>
                                    <p>
                                        <asp:Button ID="SaveFile" runat="server" Text="Upload file" OnClick="SaveFile_Click" CssClass="btn btn-default" />
                                            <asp:RequiredFieldValidator ID="RequiredSaveFile" runat="server"
                                                ErrorMessage="Please select a file and then click <strong>Upload file</strong>"
                                                ControlToValidate="FileUpload1" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </p>
                                    <asp:HiddenField ID="LocationOfSavedFile" runat="server" />
                                </asp:Panel>

                            </asp:WizardStep>

                            <asp:WizardStep ID="WizardStep3" Title="Import rows from file">

                                <asp:UpdatePanel runat="server" ID="upnlImportResult" ChildrenAsTriggers="True" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <p>
                                            <asp:Button ID="Import" runat="server" Text="Extract from" OnClick="Import_Click" CssClass="btn btn-default btn-block" />
                                        </p>
                                        <asp:Panel ID="ExtractionSuccess" runat="server" CssClass="alert alert-success" Visible="false" EnableViewState="false">
                                            <asp:Literal ID="ExtractionSuccessMessage" runat="server" Text="Extraction successful"></asp:Literal>
                                        </asp:Panel>
                                        <asp:Panel ID="ExtractionFailure" runat="server" CssClass="alert alert-danger" Visible="false" EnableViewState="False">
                                            <asp:Literal ID="ExtractionFailureMessage" runat="server" Text="Extraction failed"></asp:Literal>
                                        </asp:Panel>

                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlImportResult" DisplayAfter="500">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/icons/uploading.gif" />
                                                <asp:Literal runat="server" Text="This process may take a while depending upon the number of sheets and rows in each sheet in the excel file
                                                Please do not interrupt the process in order to allow successful import"></asp:Literal>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                        <asp:Panel runat="server" ID="ImportResult" Visible="false">
                                            <h3>Validation status</h3>
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <asp:GridView ID="ColumnConsistencySummary" runat="server" AutoGenerateColumns="False" Caption="Column consistency" CssClass="table">
                                                        <Columns>
                                                            <asp:BoundField DataField="Key" HeaderText="Table"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Consistent ?" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ImageUrl="~/icons/tick.png" Visible='<%# Boolean.Parse(Eval("Value").ToString()) ? true : false %>' ID="Tick" Width="25px" />
                                                                    <asp:Image runat="server" ImageUrl="~/icons/cross.png" Visible='<%# bool.Parse(Eval("Value").ToString()) ? false : true %>' ID="Cross" Width="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>

                                                        <RowStyle Height="32px"></RowStyle>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:GridView ID="BlankRowsDeleteSummary" runat="server" AutoGenerateColumns="False" Caption="Deleted rows" CssClass="table">
                                                        <Columns>
                                                            <asp:BoundField DataField="Key" HeaderText="Table"></asp:BoundField>
                                                            <asp:BoundField DataField="Value" HeaderText="Number of rows deleted"></asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle Height="24px"></EditRowStyle>

                                                        <RowStyle Height="32px"></RowStyle>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <asp:GridView ID="RecordCount" runat="server" AutoGenerateColumns="False" Caption="Deleted rows" CssClass="table">
                                                <Columns>
                                                    <asp:BoundField DataField="Key" HeaderText="Table"></asp:BoundField>
                                                    <asp:BoundField DataField="Value" HeaderText="Number of contacts"></asp:BoundField>
                                                </Columns>
                                                <EditRowStyle Height="24px"></EditRowStyle>

                                                <RowStyle Height="32px"></RowStyle>
                                            </asp:GridView>


                                            <%--<asp:GridView ID="ValidationSummary" runat="server" AutoGenerateColumns="False" Caption="Contacts validation">
                                                    <Columns>
                                                        <asp:BoundField DataField="Key" HeaderText="Table"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="Passed ?" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ImageUrl="~/media/icons/tick.png" Visible='<%# Boolean.Parse(Eval("Value").ToString()) ? true : false %>' ID="Tick" Width="25px" />
                                                                <asp:Image runat="server" ImageUrl="~/media/icons/cross.png" Visible='<%# bool.Parse(Eval("Value").ToString()) ? false : true %>' ID="Cross" Width="20px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                    <RowStyle Height="32px"></RowStyle>
                                                </asp:GridView>--%>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:Button ID="Transform" runat="server" Text="Prepare contacts" CssClass="btn btn-block btn-default btn-lg" OnClick="Transform_Click" />
                                <asp:Panel ID="TransformError" runat="server" CssClass="alert alert-danger" Visible="false">
                                            <asp:Literal ID="Literal2" runat="server" Text="Transformation failed"></asp:Literal>
                                        </asp:Panel>
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="TestLabel" runat="server" Text="Label"></asp:Label>
                                        <asp:Button ID="Button1" runat="server" Text="Update time" OnClick="Button1_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            <asp:UpdateProgress runat="server">
                                <ProgressTemplate>
                                    <asp:Image ImageUrl="~/media/icons/uploading (1).gif" runat="server" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                            </asp:WizardStep>

                            <asp:WizardStep runat="server" Title="Convert imported excel rows to contacts">
                                <%--<asp:Button ID="ConvertRowsToContacts" runat="server" Text="Show contacts" />--%>
                                <%--OnClick="ConvertRowsToContacts_Click"--%>
                                <asp:GridView ID="tempGridView" runat="server"></asp:GridView>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label5" runat="server" Text="Converted contacts" Font-Size="X-Large"></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>

                                    <AlternatingItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("Name") %>' ForeColor="SteelBlue"></asp:Label>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>

                                <asp:Button ID="Save" runat="server" Text="Save all to database"
                                    Style="margin: 20px 20% 20px 20%; width: 60%; height: 40px;" />
                            </asp:WizardStep>
                        </WizardSteps>
                    </asp:Wizard>

                </div>
            </div>



        </div>
    </form>
</body>
</html>
