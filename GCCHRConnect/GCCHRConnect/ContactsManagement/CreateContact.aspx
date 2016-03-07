<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateContact.aspx.cs" Inherits="GCCHRConnect.ContactsManagement.CreateContact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <link href="../Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <div class="row">
            <div class="col-lg-4">
                <fieldset>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Title" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="Title" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="FirstName" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="FirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="MiddleName" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="MiddleName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="LastName" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="LastName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" Text="Phone No." CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="PhoneNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" Text="Email" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Mobile" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="Mobile" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label8" runat="server" Text="Address" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="Address" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-lg-10">
            </div>
        </div>
    </form>
</body>
</html>
