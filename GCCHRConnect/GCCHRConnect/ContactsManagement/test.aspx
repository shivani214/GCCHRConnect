<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="GCCHRConnect.ContactsManagement.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type='text/javascript'
        src='http://code.jquery.com/jquery-2.0.2.js'></script>
    <link rel="stylesheet" type="text/css"
        href="http://getbootstrap.com/dist/css/bootstrap.css"/>
    <link rel="stylesheet" type="text/css"
        href="http://www.bootstrap-switch.org/dist/css/bootstrap3/bootstrap-switch.css"/>
    <script type='text/javascript'
        src="http://www.bootstrap-switch.org/dist/js/bootstrap-switch.js"></script>
    <script type='text/javascript'>
        $(window).load(function () {
            $("input.switch").bootstrapSwitch();
        });
    </script>
     
</head>
<body>
    <div>
        Doctor:
        <input type="checkbox" class="switch" checked "Docter"/>
     
           </div>
    
    <div>
       Relative:
        <input type="checkbox" class="switch" checked />
    </div>
    <div>
       Friends:
        <input type="checkbox" class="switch" />
        </body>
    </html>