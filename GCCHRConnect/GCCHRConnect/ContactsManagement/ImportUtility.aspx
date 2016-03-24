<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportUtility.aspx.cs" Inherits="ContactsVisual.ImportUtility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register Src="~/uc_title.ascx" TagPrefix="uc1" TagName="uc_title" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-2.1.4.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="Scripts/jquery-2.1.4.intellisense.js" type="text/javascript"></script>


</head>
<body style="margin: 0;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="300"></asp:ScriptManager>

        <uc1:uc_title runat="server" ID="uc_title" />
        <div>
            <div id="container_ImportUtility">
                <asp:Label ID="Label1" runat="server"
                    Text="Import utility"
                    Font-Names="Batang" Font-Size="XX-Large" ForeColor="#0059DF"></asp:Label>
                <br />

                <div>
                    <asp:Wizard ID="Wizard1" runat="server"
                        BorderColor="#B5C7DE" BorderWidth="1px"
                        Font-Names="Verdana" Font-Size="0.8em"
                        DisplayCancelButton="True" Width="100%" ActiveStepIndex="3" OnActiveStepChanged="Wizard1_ActiveStepChanged" OnNextButtonClick="Wizard1_NextButtonClick">

                        <HeaderStyle
                            HorizontalAlign="Center"
                            BackColor="#284E98"
                            BorderColor="#EFF3FB" BorderWidth="2px" BorderStyle="Solid"
                            Font-Bold="True" Font-Size="0.9em" ForeColor="White"></HeaderStyle>

                        <NavigationButtonStyle
                            BackColor="White"
                            BorderColor="#507CD1" BorderWidth="1px" BorderStyle="Solid"
                            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98"></NavigationButtonStyle>

                        <SideBarButtonStyle
                            BackColor="#507CD1"
                            Font-Names="Verdana" ForeColor="White"></SideBarButtonStyle>

                        <SideBarStyle
                            VerticalAlign="Top"
                            BackColor="#507CD1"
                            Font-Size="0.9em"></SideBarStyle>

                        <StepStyle
                            Font-Size="0.8em"
                            ForeColor="#333333"></StepStyle>

                        <WizardSteps>

                            <asp:WizardStep ID="WizardStep1" runat="server" Title="Begin">
                                <asp:Label ID="Label3" runat="server" Text="Import contacts from a Microsoft Excel file" Font-Size="Large"></asp:Label>
                                <p>This utility helps to import contacts from an excel file and inserts them in the database</p>
                                <p>Before you go ahead verify this checklist for smooth import</p>
                                <%--bulletedlist--%>
                                <p>Go through following easy steps</p>
                                <asp:BulletedList ID="BulletedList1" runat="server">
                                    <asp:ListItem>Upload file to server</asp:ListItem>
                                    <asp:ListItem>Import rows from file</asp:ListItem>
                                    <asp:ListItem>Convert imported excel rows to contacts</asp:ListItem>
                                    <asp:ListItem>Save to database</asp:ListItem>
                                    <asp:ListItem>View summary</asp:ListItem>
                                </asp:BulletedList>
                                <p>ToDo: Show a list of tags available in the tags collection so that user may either edit his tags (in excel) to match the ones provided or leave them as it is so they may be added to the Tag collection</p>
                                <p>Another way is that the list of tags from Tag colleciton may also be shown later before converting contacts to objects.
                                    Simultaneously we may show a list of tags imported from excel. Then user may be provided option to replace his tag by the one in Tag collection
                                </p>
                            </asp:WizardStep>

                            <asp:WizardStep ID="WizardStep2" runat="server" Title="Upload file to server">
                                <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select the Miscrosoft Excel file from which to import contacts" BackColor="#E5E5E5" />

                                <asp:Button ID="SaveFile" runat="server" Text="Upload file" OnClick="SaveFile_Click" />

                                <asp:HiddenField ID="LocationOfSavedFile" runat="server" />

                            </asp:WizardStep>

                            <asp:WizardStep ID="WizardStep3" Title="Import rows from file">
                                <asp:Panel ID="UploadedFileProperties" runat="server" Font-Italic="True" Font-Size="Medium">
                                    <asp:Label ID="Label2" runat="server" Text="File name: "></asp:Label>
                                    <asp:Label ID="FileName" runat="server" Text=""></asp:Label>
                                </asp:Panel>

                                <asp:UpdatePanel runat="server" ID="upnlImportResult" ChildrenAsTriggers="True" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="Import" runat="server" Text="Import" OnClick="Import_Click" />

                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlImportResult" DisplayAfter="500">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/media/icons/uploading.gif" />
                                                <p>This process may take a while depending upon the number of sheets and rows in each sheet in the excel file</p>
                                                <p>Please do not interrupt the process or click any button to allow successful import</p>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                        <asp:Panel runat="server" ID="ImportResult">
                                            <div id="container_ValidationStatus">

                                                <div>
                                                    <asp:Label ID="Literal1" runat="server" Text="Validation status" Font-Names="Batang" Font-Size="XX-Large" ForeColor="Blue"></asp:Label>
                                                </div>

                                                <div>
                                                    <asp:GridView ID="ColumnConsistencySummary" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" Caption="Column consistency"
                                                        Style="float: left; margin-right: 10px;" BorderColor="#66CCFF" BorderWidth="1px">
                                                        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="Key" HeaderText="Table"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Consistent ?" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ImageUrl="~/media/icons/tick.png" Visible='<%# Boolean.Parse(Eval("Value").ToString()) ? true : false %>' ID="Tick" Width="25px" />
                                                                    <asp:Image runat="server" ImageUrl="~/media/icons/cross.png" Visible='<%# bool.Parse(Eval("Value").ToString()) ? false : true %>' ID="Cross" Width="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF"></EditRowStyle>

                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                                        <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

                                                        <RowStyle BackColor="#EFF3FB" Height="32px"></RowStyle>

                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                                        <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                                                        <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                                                        <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                                                    </asp:GridView>

                                                    <asp:GridView ID="BlankRowsDeleteSummary" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" Caption="Deleted rows"
                                                        Style="float: left; margin-right: 10px;" BorderColor="#66CCFF" BorderWidth="1px">
                                                        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="Key" HeaderText="Table"></asp:BoundField>
                                                            <asp:BoundField DataField="Value" HeaderText="Number of rows deleted"></asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" Height="24px"></EditRowStyle>

                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                                        <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

                                                        <RowStyle BackColor="#EFF3FB" Height="32px"></RowStyle>

                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                                        <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                                                        <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                                                        <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                                                    </asp:GridView>

                                                    <asp:GridView ID="ValidationSummary" runat="server" AutoGenerateColumns="False" Caption="Contacts validation"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="#66CCFF" BorderWidth="1px">
                                                        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="Key" HeaderText="Table"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Passed ?" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ImageUrl="~/media/icons/tick.png" Visible='<%# Boolean.Parse(Eval("Value").ToString()) ? true : false %>' ID="Tick" Width="25px" />
                                                                    <asp:Image runat="server" ImageUrl="~/media/icons/cross.png" Visible='<%# bool.Parse(Eval("Value").ToString()) ? false : true %>' ID="Cross" Width="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF"></EditRowStyle>

                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                                        <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

                                                        <RowStyle BackColor="#EFF3FB" Height="32px"></RowStyle>

                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                                        <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                                                        <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                                                        <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                            <asp:Label ID="Label4" runat="server" Text="Summary" Font-Size="XX-Large" ForeColor="Blue" Font-Names="Batang"></asp:Label>

                                            <asp:Panel ID="toggle_FaultyContacts" runat="server" BackColor="#FF8080" Style="cursor: pointer;" Font-Size="X-Large">
                                                <asp:Label ID="label_FaultyContacts" runat="server" Text="Faulty contacts of various tables"></asp:Label>
                                                <asp:Label ID="displayStatus_FaultyContacts" runat="server" Text="" Style="float: right;"></asp:Label>
                                                <asp:Image ID="imageDisplayStatus_FaultyContacts" runat="server" Height="20px" ImageUrl="~/media/icons/Swapster-Arrow_down.png" />
                                            </asp:Panel>
                                            <asp:Panel ID="FaultyContacts" runat="server" BackColor="#FFA6A6"></asp:Panel>
                                            <ajx:CollapsiblePanelExtender
                                                ID="FaultyContacts_Collapsible" runat="server"
                                                TargetControlID="FaultyContacts"
                                                ScrollContents="True"
                                                ExpandControlID="toggle_FaultyContacts"
                                                CollapseControlID="toggle_FaultyContacts"
                                                TextLabelID="displayStatus_FaultyContacts"
                                                ExpandedText="Hide" CollapsedText="Show"
                                                ExpandedImage="~/media/icons/Swapster-Arrow_down.png"
                                                CollapsedImage="~/media/icons/Swapster-Arrow_right.png"
                                                ImageControlID="imageDisplayStatus_FaultyContacts"></ajx:CollapsiblePanelExtender>


                                            <asp:Panel ID="toggle_ValidContacts" runat="server" BackColor="#42FFA0" Style="cursor: pointer;" Font-Size="X-Large">
                                                <asp:Label ID="label_ValidContacts" runat="server" Text="Valid contacts"></asp:Label>
                                                <asp:Label ID="displayStatus_ValidContacts" runat="server" Text=""></asp:Label>
                                                <asp:Image ID="imageDisplayStatus_ValidContacts" runat="server" Height="20px" ImageUrl="~/media/icons/Swapster-Arrow_right.png" />
                                            </asp:Panel>

                                            <asp:Panel ID="ValidContacts" runat="server" BackColor="#99FFCC"></asp:Panel>
                                            <ajx:CollapsiblePanelExtender
                                                ID="ValidContacts_Collapsible" runat="server"
                                                TargetControlID="ValidContacts"
                                                ExpandedSize="500"
                                                Collapsed="True"
                                                ScrollContents="True"
                                                CollapseControlID="toggle_ValidContacts"
                                                ExpandControlID="toggle_ValidContacts"
                                                TextLabelID="displayStatus_ValidContacts"
                                                ExpandedText="Hide"
                                                CollapsedText="Show"
                                                CollapsedImage="~/media/icons/Swapster-Arrow_right.png"
                                                ExpandedImage="~/media/icons/Swapster-Arrow_down.png"
                                                ImageControlID="imageDisplayStatus_ValidContacts"></ajx:CollapsiblePanelExtender>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

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
                                <asp:Button ID="ConvertRowsToContacts" runat="server" Text="Show contacts" OnClick="ConvertRowsToContacts_Click" />

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
