<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.PhotoClass.Add"
    Title="<%$Resources:CMSPhoto,ptAddPrictureClass %>" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<%@ Register TagPrefix="uc1" TagName="PhotoClassDropList" Src="~/Controls/PhotoClassDropList.ascx" %>
<html>
<head>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/MasterPage<%=Session["Style"]%>.css" type="text/css"
        rel="stylesheet" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" charset="utf-8">
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/admin/js/msgbox/css/msgbox.css" rel="stylesheet" charset="utf-8" />
    <script type="text/javascript" src="/admin/js/msgbox/script/msgbox.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="newslistabout"  style="width:500">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptAddPrictureClass %>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMSPhoto,lblAddPrictureClass %>"/>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td height="25" width="30%" align="right">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,fieldUserDescription %>"/>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtClassName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMSPhoto,lblParentsClass %>"/>：
                            </td>
                            <td height="25" width="*" align="left">
                                <uc1:PhotoClassDropList ID="ddlPhotoClass" runat="server" IsNull="True"  />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">
                               <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblOrder %>"/>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtSequence" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdbg" align="center" valign="bottom">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                        OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                        class="adminsubmit_short" OnClientClick="JavaScript:parent.$.colorbox.close();"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
