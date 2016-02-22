<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="GCCHRConnect.ContactsManagement.ContactList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <link href="../Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
                <a href="http://localhost:19008/ContactsManagement/">http://localhost:19008/ContactsManagement/</a>
            <div class="jumbotron">
                <h1>Contact list</h1>
                <p>A list of all the contacts</p>
                </div>
                <asp:ObjectDataSource runat="server" ID="ContactsDataSource" SelectMethod="GetAllRecords" TypeName="GCCHRMachinery.BusinessLogicLayer.ContactPersonOrganizationService"></asp:ObjectDataSource>
                <asp:GridView ID="GridViewContacts" runat="server" AutoGenerateColumns="False" DataSourceID="ContactsDataSource" CssClass="table table-bordered table-striped table-condensed">
                    <Columns>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" Value='<%# Bind("Id") %>' ID="ContactId"></asp:HiddenField>
                                <asp:Label Text='<%# Eval("Name.Title") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:BoundField DataField="Name.Title" HeaderText="Title" />--%>
                        <asp:BoundField DataField="Name.First" HeaderText="First" />
                        <asp:BoundField DataField="Name.Middle" HeaderText="Middle" />
                        <asp:BoundField DataField="Name.Last" HeaderText="Last" />


                        <asp:TemplateField HeaderText="Tags">
                            <ItemTemplate>
                                <asp:GridView ID="GridViewTags" runat="server" DataSource='<%# Eval("Tags") %>'></asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mobiles">
                            <ItemTemplate>
                                <asp:GridView ID="GridViewMobiles" runat="server" DataSource='<%# Eval("Mobiles") %>'></asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Phones">
                            <ItemTemplate>
                                <asp:GridView ID="GridViewPhones" runat="server" DataSource='<%# Eval("Phones") %>'></asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Emails">
                            <ItemTemplate>
                                <asp:GridView ID="GridViewEmails" runat="server" DataSource='<%# Eval("Emails") %>'></asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Addresses">
                            <ItemTemplate>
                                <asp:GridView ID="GridViewAddresses" runat="server" DataSource='<%# Eval("Addresses") %>'></asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>
            
        </div>
    </form>
</body>
</html>
