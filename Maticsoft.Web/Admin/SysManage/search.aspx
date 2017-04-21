<%@ Page Language="c#" CodeBehind="search.aspx.cs" AutoEventWireup="True" Inherits="Maticsoft.Web.SysManage.search" %>

<%--<%@ Register TagPrefix="uc1" TagName="CheckRight" Src="~/Controls/CheckRight.ascx" %>--%>
<%@ Register TagPrefix="uc1" TagName="CopyRight" Src="~/Controls/copyright.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SysManage,ptSearch %>" />
    </title>
    <link href="/Admin/css/style.css" type="text/css" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="600" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td height="22">
                <div align="right">
                    [ <a href="treelist.aspx">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,btnBackText %>" /></a> ]
                </div>
            </td>
        </tr>
    </table>
    <table width="600" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td bgcolor='<%=Application[Session["Style"]+"xtable_bgcolor"]%>'>
                <table width="100%" border="1" cellpadding="5" cellspacing="0" bordercolorlight='<%=Application[Session["Style"]+"xtable_bordercolorlight"]%>'
                    bordercolordark='<%=Application[Session["Style"]+"xtable_bordercolordark"]%>'>
                    <tr bgcolor="#e4e4e4">
                        <td height="22" bgcolor='<%=Application[Session["Style"]+"xtable_titlebgcolor"]%>'>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:SysManage,lblSearch %>" />
                        </td>
                    </tr>
                    <tr>
                        <td height="22">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="150" height="22">
                                        <div align="right">
                                            <font color='<%=Application[Session["Style"]+"xform_requestcolor"]%>'>*</font>
                                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,fieldFeedback_iID %>" />£º</div>
                                    </td>
                                    <td height="22" align="left">
                                        <asp:TextBox ID="txtID" runat="server" Width="200px" ToolTip="<%$Resources:SysManage,TooltipMenuID%>"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150" height="22">
                                        <div align="right">
                                            <font color='<%=Application[Session["Style"]+"xform_requestcolor"]%>'>*</font>
                                          <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:SysManage,fieldText %>" />£º</div>
                                    </td>
                                    <td height="22" align="left">
                                        <asp:TextBox ID="txtName" runat="server" Width="200px" ToolTip="<%$Resources:SysManage,TooltipTxtName%>"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150" height="15" style="height: 15px">
                                        <div align="right">
                                            <font color='<%=Application[Session["Style"]+"xform_requestcolor"]%>'>*</font>
                                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblParentClass%>" />£º</div>
                                    </td>
                                    <td height="22" align="left">
                                        <asp:DropDownList ID="listTarget" runat="server" Width="200px">
                                            <asp:ListItem Value="0" Selected="True" Text="<%$Resources:Site,lblRootDirectory%>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150" height="22">
                                        <div align="right">
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:SysManage,fieldPermission%>" />£º</div>
                                    </td>
                                    <td height="22" align="left">
                                        <asp:DropDownList ID="listPermission" runat="server" Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150" height="22">
                                        <div align="right">
                                            <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,lblExplain%>" />£º</div>
                                    </td>
                                    <td height="22" align="left">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="300px" ToolTip="<%$Resources:SysManage,TooltiptxtDescription%>"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="22">
                            <div align="center">
                                &nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="<%$Resources:Site,btnQueryText%>" OnClick="btnSearch_Click">
                                </asp:Button>&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:Site,btnCancelText%>" OnClick="btnCancel_Click">
                                </asp:Button>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <uc1:CopyRight ID="CopyRight1" runat="server"></uc1:CopyRight>
  <%--  <uc1:CheckRight ID="CheckRight1" runat="server"></uc1:CheckRight>--%>
    </form>
</body>
</html>
