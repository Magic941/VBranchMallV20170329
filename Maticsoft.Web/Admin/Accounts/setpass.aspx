<%@ Page Language="c#" CodeBehind="SetPass.aspx.cs" AutoEventWireup="True" Inherits="Maticsoft.Web.Accounts.SetPass" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:SysManage,ptSetPass%>"/></title>
    <link href="/Admin/../style.css" type="text/css" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <div align="center">
        <table cellspacing="0" cellpadding="5" width="600" align="center" border="0">
            <tr>
                <td bgcolor="#f5f9ff">
                    <table cellspacing="0" bordercolordark="#d3d8e0" cellpadding="5" width="100%" bordercolorlight="#4f7fc9"
                        border="1">
                        <tr>
                            <td bgcolor="#e3efff" height="22" align="center">
                            </td>
                        </tr>
                        <tr>
                            <td height="22">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td width="30%" height="22">
                                            <div align="right">
                                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Site,fieldUserName %>" />:</div>
                                        </td>
                                        <td height="22">
                                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                                ErrorMessage="<%$Resources:Site,ErrorUserNameNotNull%>"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" height="22">
                                            <div align="right">
                                                <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,fieldPassword %>" /></div>
                                        </td>
                                        <td height="22">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" height="22">
                                            <div align="right">
                                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,fieldConfirmationPassword %>" />:</div>
                                        </td>
                                        <td height="22">
                                            <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<%$Resources:Site,ErrorPasswprd%>"
                                                ControlToCompare="txtPassword" ControlToValidate="txtPassword1"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" height="22">
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="22">
                                <div align="center">
                                    <asp:Button ID="btnUpdate" runat="server" Text="<%$Resources:SysManage,btnUpdateText%>" OnClick="btnUpdate_Click">
                                    </asp:Button><font face="ËÎÌו">&nbsp;</font>
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:Site,btnCancelText%>" OnClick="btnCancel_Click">
                                    </asp:Button></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
