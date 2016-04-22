<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportUtility.aspx.cs" Inherits="GCCHRConnect.ContactsManagement.ImportUtility" Trace="False" %>

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

        <div class="page-header text-center">
            <h1 class="text-success">Import contacts from Microsoft Excel file
                <br />
                <small>This utility helps to import contacts from an excel file and inserts them in the database</small>
            </h1>
        </div>
        <div id="container_ImportUtility" class="container">

            <asp:Wizard ID="Wizard1" runat="server" DisplayCancelButton="True" ActiveStepIndex="0">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="headerPlaceHolder" runat="server" />
                    <div id="steps" class=" panel btn-group btn-group-justified">
                        <asp:PlaceHolder ID="sideBarPlaceHolder" runat="server" />
                    </div>
                    <div>
                        <asp:PlaceHolder ID="WizardStepPlaceHolder" runat="server" />
                    </div>
                    <div>
                        <asp:PlaceHolder ID="navigationPlaceHolder" runat="server" />
                    </div>
                </LayoutTemplate>

                <HeaderTemplate>
                </HeaderTemplate>

                <SideBarTemplate>
                    <asp:ListView ID="SideBarList" runat="server" RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <asp:LinkButton ID="SideBarButton" runat="server" CssClass="btn btn-default" />
                        </ItemTemplate>
                        <SelectedItemTemplate>
                            <asp:LinkButton ID="SideBarButton" runat="server" CssClass="btn btn-success" />
                        </SelectedItemTemplate>
                    </asp:ListView>
                </SideBarTemplate>
                <SideBarStyle></SideBarStyle>
                <SideBarButtonStyle></SideBarButtonStyle>

                <StepStyle CssClass="col-lg-10 panel-body"></StepStyle>

                <StartNavigationTemplate>
                    <div class="panel-body well well-sm">
                        <div class="pull-right">
                            <asp:Button runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger" ID="CancelButton"></asp:Button>
                            <asp:Button runat="server" CommandName="MoveNext" Text="Next" CssClass="btn btn-default" ID="StartNextButton"></asp:Button>
                        </div>
                    </div>
                </StartNavigationTemplate>
                <StepNavigationTemplate>
                    <div class="panel-body well well-sm">
                        <div class="pull-right">
                            <asp:Button runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger" ID="CancelButton"></asp:Button>
                        </div>
                    </div>


                    <%--<asp:Button runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Previous" CssClass="btn btn-default" ID="StepPreviousButton"></asp:Button>
                            <asp:Button runat="server" CommandName="MoveNext" Text="Next" CssClass="btn btn-default" ID="StepNextButton"></asp:Button>--%>
                </StepNavigationTemplate>

                <WizardSteps>
                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Guidelines">
                        <div class="panel panel-default" id="fileStructure">
                            <div class="panel-heading">
                                <h5>File structure</h5>
                            </div>
                            <div class="panel-body">
                                <asp:BulletedList ID="BulletedList2" runat="server">
                                    <asp:ListItem>The Excel file may contain single or multiple sheets</asp:ListItem>
                                    <asp:ListItem>Row 1 on each sheet: must not be blank and must contain headers</asp:ListItem>
                                    <asp:ListItem>Records must begin from Row 2</asp:ListItem>
                                    <asp:ListItem>There should be no blank rows from beginning to end of records</asp:ListItem>
                                    <asp:ListItem>Headers must begin from Column A and there should be no blank columns from beginning to end of headers</asp:ListItem>
                                </asp:BulletedList>
                            </div>
                        </div>

                        <div class="panel panel-default" id="columns">
                            <div class="panel-heading">
                                <h5>Columns</h5>
                            </div>
                            <div class="panel-body">
                                <asp:BulletedList ID="BulletedList1" runat="server">
                                    <asp:ListItem class="text-danger">The required columns (headers) must be present in each sheet. Application will proceed if they are not present.</asp:ListItem>
                                    <asp:ListItem class="text-info">Optional columns may or may not be present. Values are extracted from these columns if they have been provided.</asp:ListItem>
                                    <asp:ListItem>The order of columns in various sheets need not be same.</asp:ListItem>
                                    <asp:ListItem>The names of the columns (headers) must be exact as given below.</asp:ListItem>
                                    <asp:ListItem>Any other columns may be present in the file or any sheet but no values will be extracted from them.</asp:ListItem>
                                </asp:BulletedList>
                                <%--<div class="row" id="columnsDescription">
                                            <div class="panel">Panel-1</div>
                                            <div class="panel">Panel-2</div>
                                        </div>--%>
                                <div class="list-group">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <h4 class="list-group-item-heading text-danger">Required columns</h4>
                                            <ul class="list-unstyled">
                                                <li class="list-group-item-text">SN<span class="text-muted"><em> (for Serial number, must be in Column A, not required but recommended)</em></span></li>
                                                <li class="list-group-item-text">Title</li>
                                                <li class="list-group-item-text">First name</li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4">
                                            <h4 class="list-group-item-heading text-info">Optional columns</h4>
                                            <ul class="list-unstyled">
                                                <li class="list-group-item-text">Middle name</li>
                                                <li class="list-group-item-text">Last name</li>
                                                <li class="list-group-item-text">Line 1</li>
                                                <li class="list-group-item-text">Line 2</li>
                                                <li class="list-group-item-text">Line 3</li>
                                                <li class="list-group-item-text">City</li>
                                                <li class="list-group-item-text">Pin code</li>
                                                <li class="list-group-item-text">State</li>
                                                <li class="list-group-item-text">Country</li>
                                                <li class="list-group-item-text">Mobiles</li>
                                                <li class="list-group-item-text">Phones</li>
                                                <li class="list-group-item-text">Emails</li>
                                                <li class="list-group-item-text">Tags</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default" id="rows">
                            <div class="panel-heading">
                                <h5>Rows</h5>
                            </div>
                            <div class="panel-body">
                                <ul>
                                    <li>The method of writing values for each header is given below.</li>
                                    <li>If a header supports multiple values, the values must be separated by <strong>;</strong></li>
                                </ul>


                                <asp:Table ID="Table1" runat="server" CssClass="table">
                                    <asp:TableHeaderRow>
                                        <asp:TableHeaderCell>Header</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Value</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Multi values support</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>SN</asp:TableHeaderCell>
                                        <asp:TableCell>Incrementing number. May begin from 1 on each sheet or continue from previous sheet. Helps to pin-point validation errors.</asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="text-danger">Title</asp:TableHeaderCell>
                                        <asp:TableCell>Required</asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="text-danger">First name</asp:TableHeaderCell>
                                        <asp:TableCell>Required</asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Middle name</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Last name</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Line 1</asp:TableHeaderCell>
                                        <asp:TableCell>1<sup>st</sup> line of address</asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Line 2</asp:TableHeaderCell>
                                        <asp:TableCell>2<sup>nd</sup> line of address</asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Line 3</asp:TableHeaderCell>
                                        <asp:TableCell>3<sup>rd</sup> line of address</asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>City</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Pin code</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>State</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Country</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Mobiles</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell><span class="glyphicon glyphicon-ok text-success"></span><span class="text-muted"><em> (separated by ;)</em></span></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Phones</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell><span class="glyphicon glyphicon-ok text-success"></span><span class="text-muted"><em> (separated by ;)</em></span></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Emails</asp:TableHeaderCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell><span class="glyphicon glyphicon-ok text-success"></span><span class="text-muted"><em> (separated by ;)</em></span></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableHeaderCell>Tags</asp:TableHeaderCell>
                                        <asp:TableCell>
                                            <p>
                                                You may use any existing tags from the ones given below. If any new tags are found, they will be added to the database.
                                            </p>

                                            <asp:Repeater ID="Repeater2" runat="server" DataSourceID="dsTags">
                                                <ItemTemplate>
                                                    <asp:Label ID="Literal1" runat="server" Text='<%# Container.DataItem %>' CssClass="well well-sm" Style="display: inline-block;"></asp:Label>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:ObjectDataSource runat="server" ID="dsTags" SelectMethod="GetAllTagNamesOnly" TypeName="GCCHRMachinery.BusinessLogicLayer.TagService"></asp:ObjectDataSource>

                                        </asp:TableCell>
                                        <asp:TableCell><span class="glyphicon glyphicon-ok text-success"></span><span class="text-muted"><em> (separated by ;)</em></span></asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </div>
                        </div>
                    </asp:WizardStep>

                    <asp:WizardStep ID="FileUpload" runat="server" Title="Upload file to server">
                        <asp:Panel runat="server" ID="fileUploadPanel">
                            <h4>Select the Excel file to be uploaded</h4>
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




    </form>
</body>
</html>
