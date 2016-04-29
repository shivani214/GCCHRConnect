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

            <asp:Wizard ID="Wizard1" runat="server" DisplayCancelButton="True" ActiveStepIndex="0" OnCancelButtonClick="Wizard1_CancelButtonClick">
                <LayoutTemplate>
                    <div id="steps" class=" panel btn-group btn-group-justified">
                        <asp:PlaceHolder ID="sideBarPlaceHolder" runat="server" />
                    </div>

                    <asp:PlaceHolder ID="headerPlaceHolder" runat="server" />

                    <div>
                        <asp:PlaceHolder ID="WizardStepPlaceHolder" runat="server" />
                    </div>

                    <div>
                        <asp:PlaceHolder ID="navigationPlaceHolder" runat="server" />
                    </div>
                </LayoutTemplate>

                <HeaderTemplate>
                    <div class="page-header">
                        <h3 class="text-primary">
                            <asp:Literal ID="StepTagLine" runat="server" Text="--add tagline here--" /></h3>
                    </div>
                </HeaderTemplate>

                <SideBarTemplate>
                    <asp:ListView ID="SideBarList" runat="server">
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

                </StepNavigationTemplate>

                <WizardSteps>
                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Guidelines">
                        <div class="page-header">
                            <h3 class="text-primary">
                                <asp:Literal ID="Step1TagLine" runat="server" Text="Guidelines to follow to construct your Excel file" /></h3>
                        </div>
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
                                                    <%--<asp:Label runat="server" CssClass="glyphicon glyphicon-tag" />--%>
                                                    <asp:Label ID="Literal1" runat="server" Text='<%# string.Format("{0} {1}","<span class=\"glyphicon glyphicon-tag\"></span>",Container.DataItem) %>' CssClass="well well-sm" Style="display: inline-block;"></asp:Label>
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

                    <asp:WizardStep ID="WizardStep2" runat="server" Title="File upload">
                        <div class="page-header">
                            <h3 class="text-primary">
                                <asp:Literal ID="Step2TagLine" runat="server" Text="Upload your Excel file for importing contacts" /></h3>
                        </div>
                        <asp:Panel runat="server" ID="fileUploadPanel">
                            <p>
                                <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select or drag'n'drop the Miscrosoft Excel file here" BackColor="#404040" ForeColor="Black" />
                            </p>
                            <p>
                                <asp:Button ID="SaveFile" runat="server" Text="Upload file" OnClick="SaveFile_Click" CssClass="btn btn-default" />
                                <asp:RequiredFieldValidator ID="RequiredSaveFile" runat="server"
                                    ErrorMessage="Please select a file and then click <strong>Upload file</strong>"
                                    ControlToValidate="FileUpload1" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </p>

                            <asp:Panel ID="ErrorAlert" runat="server" CssClass="alert alert-danger" Visible="false" EnableViewState="False">
                                <asp:Literal ID="ErrorMessage" runat="server" />
                            </asp:Panel>
                            <asp:HiddenField ID="LocationOfSavedFile" runat="server" />
                        </asp:Panel>

                    </asp:WizardStep>

                    <asp:WizardStep ID="WizardStep3" Title="Extract">
                        <div class="page-header">
                            <h3 class="text-primary">
                                <asp:Literal ID="Step3TagLine" runat="server" Text="Extract records from uploaded file" /></h3>
                        </div>

                        <asp:UpdatePanel runat="server" ID="upnlImportResult" ChildrenAsTriggers="True" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="UploadSuccess" EnableViewState="false">
                                    <p>
                                        <asp:Literal Text="Your file " runat="server" />
                                        <em>
                                            <asp:Label ID="UploadedFileName" Text="" runat="server" CssClass="text-success" /></em>
                                        <asp:Literal Text=" has been uploaded successfully! Records may now be extracted." runat="server" />
                                    </p>
                                </asp:Panel>
                                <p>
                                    <asp:Button ID="Extract" runat="server" Text="Extract now" OnClick="Extract_Click" CssClass="btn btn-default" />
                                </p>
                                <asp:Panel ID="ExtractionSuccess" runat="server" CssClass="alert alert-success" Visible="false" EnableViewState="false">
                                    <asp:Literal ID="ExtractionSuccessMessage" runat="server" Text="Extraction successful"></asp:Literal>
                                </asp:Panel>
                                <%--<asp:Panel ID="ExtractionFailure" runat="server" CssClass="alert alert-danger" Visible="false" EnableViewState="False">
                                    <asp:Literal ID="ExtractionFailureMessage" runat="server" Text="Extraction failed"></asp:Literal>
                                </asp:Panel>--%>

                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlImportResult" DisplayAfter="500">
                                    <ProgressTemplate>
                                        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/icons/uploading.gif" CssClass="pull-left" />--%>
                                        <div class="progress progress-striped active">
                                            <div class="progress-bar" style="width: 100%"></div>
                                        </div>
                                        <em>
                                            <asp:Literal runat="server" Text="This process may take a while depending upon the number of sheets and rows in each sheet in the excel file. Please do not interrupt the process in order to allow successful import"></asp:Literal></em>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:Panel runat="server" ID="ExtractResult" Visible="false">
                                    <%--<div class="well">--%>
                                    <h4>Extraction summary</h4>
                                    <%--</div>--%>
                                    <asp:GridView ID="SummaryRecordCount" runat="server" AutoGenerateColumns="False" Caption="Number of rows extracted" CssClass="table">
                                        <Columns>
                                            <asp:BoundField DataField="Key" HeaderText="Table"></asp:BoundField>
                                            <asp:BoundField DataField="Value" HeaderText="Number of rows"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <p>
                                    <asp:Button ID="ProceedToTransform" Text="Proceed" runat="server" Visible="false" CssClass="btn btn-default" OnClick="ProceedToTransform_Click" />
                                </p>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ProceedToTransform" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <%--<asp:Panel ID="TransformError" runat="server" CssClass="alert alert-danger" Visible="false">
                            <asp:Literal ID="Literal2" runat="server" Text="Transformation failed"></asp:Literal>
                        </asp:Panel>--%>
                    </asp:WizardStep>

                    <asp:WizardStep ID="WizardStep4" runat="server" Title="Validate">
                        <div class="page-header">
                            <h3 class="text-primary">
                                <asp:Literal ID="Step4TagLine" runat="server" Text="Transform extracted records into contacts and validate" /></h3>
                        </div>
                        <p>
                            <asp:Button ID="Transform" runat="server" Text="Prepare contacts" CssClass="btn btn-block btn-default btn-lg" OnClick="Transform_Click" />
                        </p>
                        <asp:Panel ID="pnlSaveToDatabase" runat="server" class="page-header" Visible="false">
                            <asp:Button ID="SaveToDatabase" Text="Save to database" runat="server" CssClass="btn btn-success btn-block" OnClick="SaveToDatabase_Click" />
                        </asp:Panel>
                        <asp:Panel ID="ValidationError" runat="server" Visible="false">
                            <div class="alert alert-danger">
                                <p>Invalid contacts found. Please make necessary corrections in your Excel file and restart this process.</p>
                            </div>
                            <asp:GridView ID="ValidationSummary" runat="server" CssClass="table table-striped table-hover" EnableViewState="false"></asp:GridView>
                        </asp:Panel>
                        <div id="transformedContactsWrapper" class="row" style="min-width: 620px;">
                            <asp:Repeater ID="TransformedContacts" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                        <div id="contactPanel" class="panel panel-primary" style="min-width: 200px; height: 350px; margin-right: 5px; overflow-y: auto;">
                                            <div id="contactHeader" class="panel-heading">
                                                <asp:Literal ID="Label5" runat="server" Text='<%# Eval("Name.Full") %>'></asp:Literal>
                                            </div>
                                            <div id="contactBody" class="panel-body">
                                                <asp:Literal ID="Nickname" runat="server" Text='<%# string.Format("<strong>{0}:</strong> {1}","Nick name", Eval("NickName")) %>' />
                                                <br />
                                                <asp:Literal ID="Line1" runat="server" Text='<%#string.Format("<strong>{0}:</strong> {1}","Line 1",  Eval("Addresses[0].Line1")) %>' />
                                                <br />
                                                <asp:Literal ID="Line2" runat="server" Text='<%#string.Format("<strong>{0}:</strong> {1}","Line 2",  Eval("Addresses[0].Line2")) %>' />
                                                <br />
                                                <asp:Literal ID="Line3" runat="server" Text='<%#string.Format("<strong>{0}:</strong> {1}","Line 3",  Eval("Addresses[0].Line3")) %>' />
                                                <br />
                                                <asp:Literal ID="City" runat="server" Text='<%#string.Format("<strong>{0}:</strong> {1}","City",  Eval("Addresses[0].City")) %>' />
                                                <br />
                                                <asp:Literal ID="PinCode" runat="server" Text='<%#string.Format("<strong>{0}:</strong> {1}","Pin code",  Eval("Addresses[0].PinCode")) %>' />
                                                <br />
                                                <asp:Literal ID="State" runat="server" Text='<%#string.Format("<strong>{0}:</strong> {1}","State",  Eval("Addresses[0].State")) %>' />
                                                <br />
                                                <asp:Literal ID="Country" runat="server" Text='<%#string.Format("<strong>{0}:</strong> {1}","Country",  Eval("Addresses[0].Country")) %>' />

                                                <br />
                                                <asp:Literal ID="Literal2" runat="server" Text='<%#string.Format("<strong>{0}:</strong>","Mobiles") %>' />
                                                <asp:Repeater ID="Mobiles" runat="server" DataSource='<%#Eval("Mobiles") %>' Visible='<%# (Eval("Mobiles") as List<string>).Count != 0  %>'>
                                                    <ItemTemplate>
                                                        <%# Container.DataItem %>
                                                    </ItemTemplate>
                                                    <SeparatorTemplate>
                                                        <asp:Literal Text=", " runat="server" />
                                                    </SeparatorTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <asp:Literal ID="Literal3" runat="server" Text='<%#string.Format("<strong>{0}:</strong>","Phones") %>' />
                                                <asp:Repeater ID="Phones" runat="server" DataSource='<%#Eval("Phones") %>' Visible='<%# (Eval("Phones") as List<string>).Count != 0  %>'>
                                                    <ItemTemplate>
                                                        <%# Container.DataItem %>
                                                    </ItemTemplate>
                                                    <SeparatorTemplate>
                                                        <asp:Literal Text=", " runat="server" />
                                                    </SeparatorTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <asp:Literal ID="Literal4" runat="server" Text='<%#string.Format("<strong>{0}:</strong>","Emails") %>' />
                                                <asp:Repeater ID="Emails" runat="server" DataSource='<%#Eval("Emails") %>' Visible='<%# (Eval("Emails") as List<string>).Count != 0  %>'>
                                                    <ItemTemplate>
                                                        <asp:HyperLink NavigateUrl="#" runat="server" Text='<%# Container.DataItem %>' CssClass="text-info" />
                                                        <a href="#"><%# Container.DataItem %></a>
                                                    </ItemTemplate>
                                                    <SeparatorTemplate>
                                                        <asp:Literal Text=", " runat="server" />
                                                    </SeparatorTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <asp:Literal ID="Literal5" runat="server" Text='<%#string.Format("<strong>{0}:</strong>","Tags") %>' />
                                                <asp:Repeater ID="Tags" runat="server" DataSource='<%#Eval("Tags") %>' Visible='<%# (Eval("Tags") as List<string>).Count != 0 %>'>
                                                    <ItemTemplate>
                                                        <span class="label label-default"><%# Container.DataItem %></span>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:GridView ID="tempGridView" runat="server"></asp:GridView>
                    </asp:WizardStep>

                    <asp:WizardStep ID="WizardStep5" Title="Save">
                            <div class="page-header">
                                <h3 class="text-primary">
                                    <asp:Literal ID="Step5TagLine" runat="server" Text="Save contacts to database" /></h3>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblTime" runat="server" />
                                    <asp:Timer ID="InsertInDatabase" runat="server" OnTick="InsertInDatabase_Tick" Interval="1000" />
                                    <asp:UpdateProgress runat="server">
                                        <ProgressTemplate>
                                            <div class="progress progress-striped active">
  <div class="progress-bar progress-bar-success" style="width: 100%;"></div>
</div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                                
                            </asp:UpdatePanel>
                    </asp:WizardStep>
                </WizardSteps>

            </asp:Wizard>


        </div>




    </form>
</body>
</html>
