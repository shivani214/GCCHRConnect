﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateContact.aspx.cs" Inherits="GCCHRConnect.ContactsManagement.CreateContact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <link href="../Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" class="container">
        <div class="page-header">
            <h1>Create Contact</h1>
        </div>
        <div class="form-horizontal">
            <div class="row">
                <div class="col-lg-4">
                    <fieldset>

                        <div class="form-group">

                            <asp:Label ID="Label1" runat="server" Text="Title" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="Title" runat="server" CssClass="form-control" placeholder="Title"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="FirstName" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="FirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="MiddleName" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="MiddleName" runat="server" CssClass="form-control" placeholder="Middle Name "></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="LastName" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="LastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label5" runat="server" Text="Phone No." CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="PhoneNo" runat="server" CssClass="form-control" placeholder="Comma seperated"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label6" runat="server" Text="Email" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="Email" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label7" runat="server" Text="Mobile" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="Mobile" runat="server" CssClass="form-control" placeholder="Comma seperated"></asp:TextBox>
                            </div>
                        </div>
                        <p>Address</p>
                        <div class="well bs-component">
                        <div class="form-group">
                            <asp:Label ID="Label8" runat="server" Text="Line1" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="Address" runat="server" CssClass="form-control" placeholder="Line1"></asp:TextBox>
                            </div>
                        </div>
                            
                        <div class="form-group">
                            <asp:Label ID="Label9" runat="server" Text="Line2" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Line2"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label10" runat="server" Text="Line3" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Line3"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label11" runat="server" Text="City" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label12" runat="server" Text="Pincode" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Pincode"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label13" runat="server" Text="State" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" placeholder="State"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label14" runat="server" Text="Country" CssClass="col-lg-3 control-label"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" placeholder="Country"></asp:TextBox>
                            </div>
                        </div>
                            </div>
                        <div class="form-group">
                            <div class="col-lg-9 col-lg-offset-3">
                                <button type="submit" class="btn btn-primary">Submit</button>
                                <button type="reset" class="btn btn-default">Cancel</button>
                            </div>
                        </div>
                        <div class="alert alert-success">
                            <strong>1 Record Saved Succesfully!</strong>
                        </div>
                    </fieldset>
                </div>
                <div class="col-lg-6 col-lg-offset-2">
                    <h5 class="text-danger">Add Tag Here </h5>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        <asp:ListItem>Doctor</asp:ListItem>
                        <asp:ListItem>Relative</asp:ListItem>
                        <asp:ListItem>Friends</asp:ListItem>
                    </asp:CheckBoxList>
                </div>

            </div>

        </div>
    </form>
</body>
</html>
