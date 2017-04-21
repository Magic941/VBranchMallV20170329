<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Maticsoft.Web.Admin.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" type="text/css" rel="stylesheet">
</head>
<body >
    <form id="Form1" method="post" runat="server">
        
        <br>
        <table height="380" cellspacing="0" cellpadding="0" width="638" align="center" border="0">
            <tr>
                <td valign="top" width="574" height="380" align="center">
                 <img src='<%=Application[Session["Style"]+"xdesktop_bgimage"]%>' />   
                </td>
            </tr>
        </table>        
    </form>
</body>

</html>
