<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="GCCHRConnect.ContactsManagement.ContactList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="http://localhost:19008/ContactsManagement/">http://localhost:19008/ContactsManagement/</a>

            <asp:GridView ID="GridViewContacts" runat="server" AutoGenerateColumns="False" DataSourceID="ContactsDataSource" OnRowDataBound="GridViewContacts_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Unique Id" />
                    <asp:BoundField DataField="Name.Title" HeaderText="Title" />
                    <asp:BoundField DataField="Name.First" HeaderText="First" />
                    <asp:BoundField DataField="Name.Middle" HeaderText="Middle" />
                    <asp:BoundField DataField="Name.Last" HeaderText="Last" />
                    
                    
                    <asp:TemplateField HeaderText="Tags">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Id") %>' runat="server" />

                            <asp:GridView ID="GridViewTags" runat="server" DataSource='<%# Eval("Tags") %>'></asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mobiles">
                        <ItemTemplate>
                            <asp:GridView ID="GridViewMobiles" runat="server"></asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Phones">
                        <ItemTemplate>
                            <asp:GridView ID="GridViewPhones" runat="server"></asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Emails">
                        <ItemTemplate>
                            <asp:GridView ID="GridViewEmails" runat="server"></asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Addresses">
                        <ItemTemplate>
                            <asp:GridView ID="GridViewAddresses" runat="server"></asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>

                    

                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource runat="server" ID="ContactsDataSource" SelectMethod="GetAllRecords" TypeName="GCCHRMachinery.BusinessLogicLayer.ContactPersonOrganizationService"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
