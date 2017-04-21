<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.UserAttendance.Left" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title></title>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/MasterPage<%=Session["Style"].ToString()%>.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" />
</head>
<body 
    leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
    <form id="Form1" method="post" runat="server">
    <table width="204" style="height:100%;" border="0" cellpadding="0" cellspacing="0">
      
        <tr>
            <td style="height:100%;" valign="top" background='<%=Application[Session["Style"].ToString()+"xleftbj_bgimage"]%>'>
                <asp:TreeView ID="TreeView1" runat="server" ShowLines="true" ShowExpandCollapse="true">
                </asp:TreeView>
            </td>
        </tr>
       
    </table>
   
    </form>
</body>
</html>
