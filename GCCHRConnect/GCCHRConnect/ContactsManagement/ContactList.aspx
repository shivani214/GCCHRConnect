<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="GCCHRConnect.ContactsManagement.ContactList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <link href="../Content/Bootswatch_Cyborg/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <script>
             $(document).ready(function () {
                 $('[data-toggle="tooltip"]').tooltip();
             });
        </script> 
    <form id="form1" runat="server">
        <a href="http://localhost:19008/ContactsManagement/">http://localhost:19008/ContactsManagement/</a>
        <div class="container-fluid">
            <div class="page-header">
                <h1>Contact list</h1>
                <p class="lead">A list of all the contacts</p>
            </div>
            <div class="row">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-11">
                    <asp:ObjectDataSource runat="server" ID="ContactsDataSource" SelectMethod="GetAllRecords" TypeName="GCCHRMachinery.BusinessLogicLayer.ContactService"></asp:ObjectDataSource>
                    <asp:GridView ID="GridViewContacts" runat="server" AutoGenerateColumns="False" DataSourceID="ContactsDataSource" CssClass="table table-hover" DataKeyNames="Id" GridLines="None" ShowHeader="False">
                        <Columns>
                            <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField runat="server" Value='<%# Bind("Id") %>' ID="ContactId"></asp:HiddenField>
                                <asp:Label Text='<%# Eval("Name.Title") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                            <asp:BoundField DataField="Id" Visible="false" />

                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <%# string.Format("<strong>{0}</strong>",Eval("Name.Full")) %>
                                    <div>
                                    <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("Tags") %>'>
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Container.DataItem %>' CssClass="label label-default" runat="server" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                        </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="Name.First" HeaderText="First" />
                        <asp:BoundField DataField="Name.Middle" HeaderText="Middle" />
                        <asp:BoundField DataField="Name.Last" HeaderText="Last" />--%>


                            <%--<asp:TemplateField HeaderText="Tags">
                                <ItemTemplate>
                                    <asp:GridView ID="GridViewTags" runat="server" DataSource='<%# Eval("Tags") %>'></asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <%--<asp:TemplateField HeaderText="Mobiles">
                            <ItemTemplate>
                                <asp:GridView ID="GridViewMobiles" runat="server" DataSource='<%# Eval("Mobiles") %>'></asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Mobiles">
                                <ItemTemplate>
                                    <asp:Repeater ID="RepeaterMobiles" runat="server" DataSource='<%# Eval("Mobiles") %>'>
                                        <ItemTemplate>
                                            <p><%# Container.DataItem %></p>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Phones">
                            <ItemTemplate>
                                <asp:GridView ID="GridViewPhones" runat="server" DataSource='<%# Eval("Phones") %>'></asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Phones">
                                <ItemTemplate>
                                    <asp:Repeater ID="RepeaterPhones" runat="server" DataSource='<%# Eval("Phones") %>'>
                                        <ItemTemplate>
                                            <p><%# Container.DataItem %></p>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Emails">
                            <ItemTemplate>
                                <asp:GridView ID="GridViewEmails" runat="server" DataSource='<%# Eval("Emails") %>'></asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Emails">
                                <ItemTemplate>
                                    <asp:Repeater ID="RepeaterEmails" runat="server" DataSource='<%# Eval("Emails") %>'>
                                        <ItemTemplate>
                                            <p>
                                                <asp:HyperLink NavigateUrl='<%# string.Format("mailto:{0}", Container.DataItem) %>' Text='<%# Container.DataItem %>' CssClass="btn-link" runat="server"></asp:HyperLink>
                                            </p>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Addresses">
                        <ItemTemplate>
                            <asp:GridView ID="GridViewAddresses" runat="server" DataSource='<%# Eval("Addresses") %>'></asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                            <%--<asp:TemplateField HeaderText="Addresses">
                                <ItemTemplate>
                                    <asp:Repeater DataSource='<%# Eval("Addresses") %>' runat="server">
                                        <ItemTemplate>
                                            <div class="panel panel-default" style="display: inline-block;">
                                                <div class="panel-body">
                                                    <p><%# Eval("Line1") %></p>
                                                    <p><%# Eval("Line2") %></p>
                                                    <p><%# Eval("Line3") %></p>
                                                    <p><%# Eval("City") %></p>
                                                    <p><%# Eval("PinCode") %></p>
                                                    <p><%# Eval("State") %></p>
                                                    <p><%# Eval("Country") %></p>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Repeater DataSource='<%# Eval("Addresses") %>' runat="server">
                                        <ItemTemplate>
                                                    <asp:Button Text='<%# Eval("City") %>' CssClass="btn btn-default" runat="server" data-toggle="tooltip" data-placement="bottom" title='<%# Container.DataItem.ToString() %>' data-original-title="Tooltip on bottom" ToolTip='<%# Container.DataItem.ToString() %>' />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
